namespace O9K.ItemManager.Modules.AbilityLeveling.AbilityBuild
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Menu.Items;

    using Ensage;

    using SharpDX;

#pragma warning disable 618
    internal class BuildDrawer : IDisposable
    {
        private readonly MenuSelector<BuildType> abilitiesType;

        private readonly AbilityBuilder abilityBuilder;

        private readonly MenuSelector<BuildType> talentsType;

        public BuildDrawer(AbilityBuilder abilityBuilder, MenuSelector<BuildType> abilitiesType, MenuSelector<BuildType> talentsType)
        {
            this.abilityBuilder = abilityBuilder;

            this.abilitiesType = abilitiesType;
            this.talentsType = talentsType;

            Drawing.OnDraw += this.OnDraw;
        }

        public void Dispose()
        {
            Drawing.OnDraw -= this.OnDraw;
        }

        private void DrawAbilities()
        {
            var abilities = this.abilityBuilder.Abilities;
            var maxLevel = abilities.Max(x => x.LearnLevel);

            var build = new Dictionary<uint, LearnableAbility>();

            for (var i = 1u; i < maxLevel; i++)
            {
                var currentLevelAbilities = abilities.Where(
                        x => x.LearnLevel == i && this.abilityBuilder.IsLearnable(
                                 x,
                                 i,
                                 i,
                                 (uint)build.Count(z => z.Value.Ability == x.Ability)))
                    .ToList();

                if (currentLevelAbilities.Count == 0)
                {
                    continue;
                }

                var ability = this.abilitiesType.Selected == BuildType.WinRate
                                  ? currentLevelAbilities.OrderByDescending(x => x.WinRate).ThenByDescending(x => x.PickRate).First()
                                  : currentLevelAbilities.OrderByDescending(x => x.PickRate).First();

                build.Add(i, ability);
            }

            var ratio = Hud.Info.ScreenRatio;
            var xStart = Hud.Info.ScreenSize.X * 0.35f;
            var yStart = Hud.Info.ScreenSize.Y * 0.55f;
            var uniqueAbilities = build.Values.GroupBy(x => x.Ability).Select(x => x.First()).OrderBy(x => x.Ability.AbilitySlot).ToList();
            var positions = new Dictionary<Ability, float>();

            Drawing.DrawRect(
                new Vector2(xStart - 2, yStart),
                new Vector2(((build.Count + 1) * 48 * ratio) + 2, uniqueAbilities.Count * 40 * ratio),
                new Color(75, 75, 75, 175),
                false);

            for (var i = 0; i < uniqueAbilities.Count; i++)
            {
                //Drawing.DrawRect(
                //    new Vector2(xStart, yStart + (i * 40 * ratio)),
                //    new Vector2(45 * ratio, 40 * ratio),
                //    uniqueAbilities[i].Texture);
                positions.Add(uniqueAbilities[i].Ability, yStart + (i * 40 * ratio));
                Drawing.DrawRect(
                    new Vector2(xStart - 2, (yStart - 2) + (i * 40 * ratio)),
                    new Vector2((build.Count + 1) * 48 * ratio, 2),
                    Color.Silver);
            }

            Drawing.DrawRect(
                new Vector2(xStart - 2, (yStart - 2) + (uniqueAbilities.Count * 40 * ratio)),
                new Vector2((build.Count + 1) * 48 * ratio, 2),
                Color.Silver);

            for (var i = 1u; i <= build.Count; i++)
            {
                var size = Drawing.MeasureText(i.ToString(), "Arial", new Vector2(35 * ratio), FontFlags.None);

                Drawing.DrawRect(
                    new Vector2((xStart - 2) + (i * 48 * ratio), yStart - 2),
                    new Vector2(2, uniqueAbilities.Count * 40 * ratio),
                    Color.Silver);

                if (!build.TryGetValue(i, out var buildAbility))
                {
                    continue;
                }

                Drawing.DrawText(
                    i.ToString(),
                    "Arial",
                    new Vector2(
                        xStart + (45 * ratio) + ((i - 1) * 48 * ratio) + (((48 * ratio) - size.X) / 2),
                        positions[buildAbility.Ability]),
                    new Vector2(35 * ratio),
                    Color.White,
                    FontFlags.None);
            }

            Drawing.DrawRect(
                new Vector2((xStart - 2) + ((build.Count + 1) * 48 * ratio), yStart - 2),
                new Vector2(2, (uniqueAbilities.Count * 40 * ratio) + 2),
                Color.Silver);

            Drawing.DrawRect(new Vector2(xStart - 2, yStart - 2), new Vector2(2, (uniqueAbilities.Count * 40 * ratio) + 2), Color.Silver);
        }

        private void DrawTalents()
        {
            var ratio = Hud.Info.ScreenRatio;
            var xStart = Hud.Info.ScreenSize.X * 0.6f;
            var yStart = Hud.Info.ScreenSize.Y * 0.3f;

            var groups = this.abilityBuilder.Talents.GroupBy(x => x.LearnLevel).ToList();
            var talents = new Dictionary<uint, string>();

            var nameSize = new Vector2();
            foreach (var group in groups)
            {
                var ability = this.talentsType.Selected == BuildType.WinRate
                                  ? group.OrderByDescending(x => x.WinRate).First()
                                  : group.OrderByDescending(x => x.PickRate).First();

                var measure = Drawing.MeasureText(ability.DisplayName, "Arial", new Vector2(35 * ratio), FontFlags.None);
                if (measure.X > nameSize.X)
                {
                    nameSize = measure;
                }

                talents.Add(group.Key, ability.DisplayName);
            }

            var levelSize = Drawing.MeasureText(talents.ElementAt(0).Key.ToString(), "Arial", new Vector2(35 * ratio), FontFlags.None);

            Drawing.DrawRect(
                new Vector2(xStart, yStart),
                new Vector2(nameSize.X + levelSize.X + 24, talents.Count * 40 * ratio),
                new Color(75, 75, 75, 175),
                false);

            for (var i = 0; i < talents.Count; i++)
            {
                Drawing.DrawText(
                    talents.ElementAt(i).Key.ToString(),
                    "Arial",
                    new Vector2(xStart + 2, yStart + (i * 40 * ratio)),
                    new Vector2(35 * ratio),
                    Color.White,
                    FontFlags.None);

                var size = Drawing.MeasureText(talents.ElementAt(i).Key.ToString(), "Arial", new Vector2(35 * ratio), FontFlags.None);

                Drawing.DrawText(
                    talents.ElementAt(i).Value,
                    "Arial",
                    new Vector2(xStart + size.X + 10 + 2, yStart + (i * 40 * ratio)),
                    new Vector2(35 * ratio),
                    Color.White,
                    FontFlags.None);

                Drawing.DrawRect(
                    new Vector2(xStart - 2, (yStart - 2) + (i * 40 * ratio)),
                    new Vector2(nameSize.X + levelSize.X + 24, 2),
                    Color.Silver);
            }

            Drawing.DrawRect(new Vector2(xStart - 2, yStart - 2), new Vector2(2, (talents.Count * 40 * ratio) + 2), Color.Silver);

            Drawing.DrawRect(
                new Vector2((xStart - 2) + levelSize.X + 8, yStart - 2),
                new Vector2(2, (talents.Count * 40 * ratio) + 2),
                Color.Silver);

            Drawing.DrawRect(
                new Vector2((xStart - 2) + nameSize.X + levelSize.X + 24, yStart - 2),
                new Vector2(2, (talents.Count * 40 * ratio) + 2),
                Color.Silver);

            Drawing.DrawRect(
                new Vector2(xStart - 2, (yStart - 2) + (talents.Count * 40 * ratio)),
                new Vector2(nameSize.X + levelSize.X + 24, 2),
                Color.Silver);
        }

        private void OnDraw(EventArgs args)
        {
            try
            {
                this.DrawAbilities();
                this.DrawTalents();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                Drawing.OnDraw -= this.OnDraw;
            }
        }
    }
}