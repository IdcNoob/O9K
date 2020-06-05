namespace O9K.ItemManager.Modules.AbilityLeveling.AbilityBuild
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    using Core.Entities.Heroes;
    using Core.Logger;
    using Core.Managers.Menu.Items;

    using Ensage;

    using Names;

    internal class AbilityBuilder
    {
        private readonly MenuSelector<BuildType> abilitiesType;

        private readonly List<uint> dotabuffAbilityLevels = new List<uint>();

        private readonly MenuSwitcher learnAbilities;

        private readonly MenuSwitcher learnTalents;

        private readonly Owner owner;

        private readonly MenuSelector<BuildType> talentsType;

        private NameManager nameManager;

        public AbilityBuilder(
            Owner owner,
            MenuSwitcher learnAbilities,
            MenuSelector<BuildType> abilitiesType,
            MenuSwitcher learnTalents,
            MenuSelector<BuildType> talentsType)
        {
            this.owner = owner;
            this.learnAbilities = learnAbilities;
            this.abilitiesType = abilitiesType;
            this.learnTalents = learnTalents;
            this.talentsType = talentsType;
        }

        public event EventHandler<EventArgs> BuildReady;

        public List<LearnableAbility> Abilities { get; } = new List<LearnableAbility>();

        public List<LearnableAbility> Talents { get; } = new List<LearnableAbility>();

        public void GetBuild()
        {
            this.nameManager = new NameManager(this.owner);

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var client = new WebClient())
            {
                client.Headers.Add(
                    "user-agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36");
                var uri = new Uri("https://www.dotabuff.com/heroes/" + this.nameManager.HeroDotaBuffName + "/builds");

                client.DownloadStringCompleted += this.OnDownloadStringCompleted;
                client.DownloadStringAsync(uri);
            }
        }

        public Ability GetLearnableAbility(int abilityPoints)
        {
            if (abilityPoints <= 0)
            {
                return null;
            }

            var canLearnTalent = this.IsTalentLearnable();
            //if (canLearnTalent && this.owner.HeroId == HeroId.npc_dota_hero_meepo && this.owner.Hero.Level == 10)
            //{
            //    canLearnTalent = false;
            //}

            if (this.learnTalents && canLearnTalent)
            {
                foreach (var talentGroup in this.Talents.Where(x => x.LearnLevel <= this.owner.Hero.Level).GroupBy(x => x.LearnLevel))
                {
                    if (talentGroup.Any(x => x.CurrentLevel > 0))
                    {
                        continue;
                    }

                    return this.talentsType.Selected == BuildType.WinRate
                               ? talentGroup.OrderByDescending(x => x.WinRate).Select(x => x.Ability).FirstOrDefault()
                               : talentGroup.OrderByDescending(x => x.PickRate).Select(x => x.Ability).FirstOrDefault();
                }
            }

            if (this.learnAbilities && (!canLearnTalent || abilityPoints > 1))
            {
                var learnableAbilities = this.Abilities.Where(x => this.IsLearnable(x));

                return this.abilitiesType.Selected == BuildType.WinRate
                           ? learnableAbilities.OrderByDescending(x => x.WinRate)
                               .ThenByDescending(x => x.PickRate)
                               .Select(x => x.Ability)
                               .FirstOrDefault()
                           : learnableAbilities.OrderByDescending(x => x.PickRate).Select(x => x.Ability).FirstOrDefault();
            }

            return null;
        }

        public bool IsLearnable(LearnableAbility ability, uint? heroLevel = null, uint? spentPoints = null, uint? abilityLevel = null)
        {
            if (abilityLevel == null)
            {
                abilityLevel = ability.CurrentLevel;
            }

            if (abilityLevel >= ability.MaxLevel)
            {
                return false;
            }

            if (heroLevel == null)
            {
                heroLevel = this.owner.Hero.Level;
            }

            if (spentPoints == null)
            {
                spentPoints = this.GetSpentPoints();
            }

            if (ability.LearnLevel != spentPoints)
            {
                return false;
            }

            if (ability.IsUltimate)
            {
                if (this.owner.HeroId == HeroId.npc_dota_hero_meepo)
                {
                    if (heroLevel < (abilityLevel * 7) + 4)
                    {
                        return false;
                    }
                }
                else if (heroLevel < (abilityLevel + 1) * 6)
                {
                    return false;
                }
            }
            else if (heroLevel <= abilityLevel * 2)
            {
                return false;
            }

            return true;
        }

        private void AddAbility(string abilityValue)
        {
            var name = WebUtility.HtmlDecode(Regex.Match(abilityValue, @"<img alt=""(.+?)""").Groups[1].Value);
            var dotaName = this.nameManager.MyAbilityNames.FirstOrDefault(x => x.Value == name).Key;
            if (string.IsNullOrEmpty(dotaName))
            {
                return;
            }

            var ability = this.owner.Hero.BaseSpellbook.Spells.FirstOrDefault(x => x.Name == dotaName);
            if (ability == null)
            {
                return;
            }

            var abilityLevel = 1u;
            var levelData = Regex.Match(abilityValue, @"<td.+?</td>");

            while (levelData.Success)
            {
                var learnable = new LearnableAbility(ability, name, abilityLevel);

                var winRate = Regex.Match(levelData.Value, @"<span (.+?)>(.?\d{1,3}\.\d{1,2})%");
                if (winRate.Success)
                {
                    learnable.WinRate = float.Parse(winRate.Groups[2].Value, CultureInfo.InvariantCulture);
                }

                var pickRate = Regex.Match(levelData.Value, @"<div class=""toptext"">(\d{1,3}\.\d{1,2})%");
                if (pickRate.Success)
                {
                    learnable.PickRate = float.Parse(pickRate.Groups[1].Value, CultureInfo.InvariantCulture);
                }

                this.Abilities.Add(learnable);

                if (!this.dotabuffAbilityLevels.Contains(++abilityLevel))
                {
                    this.Abilities.Add(new LearnableAbility(ability, name, abilityLevel++));
                }

                levelData = levelData.NextMatch();
            }
        }

        private void AddTalent(string talentValue, uint level)
        {
            var name = WebUtility.HtmlDecode(
                Regex.Match(talentValue, @"<span class=""talent-name-inner"">(.+?)</span>", RegexOptions.CultureInvariant).Groups[1].Value);

            float.TryParse(
                Regex.Match(talentValue, @">Win Rate: (\d{1,3}\.\d{1,2})%").Groups[1].Value,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var winRate);

            float.TryParse(
                Regex.Match(talentValue, @">Pick Rate: (\d{1,3}\.\d{1,2})%").Groups[1].Value,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var pickRate);

            var dotaName = this.nameManager.MyAbilityNames.FirstOrDefault(x => x.Value == name).Key;
            if (string.IsNullOrEmpty(dotaName))
            {
                return;
            }

            var ability = this.owner.Hero.BaseSpellbook.Spells.FirstOrDefault(x => x.Name == dotaName);
            if (ability == null)
            {
                return;
            }

            var learnable = new LearnableAbility(ability, name, level)
            {
                PickRate = pickRate,
                WinRate = winRate
            };

            this.Talents.Add(learnable);
        }

        private uint GetSpentPoints()
        {
            var spentPoints = (uint)this.Abilities.Select(x => x.Ability).Distinct().Sum(x => x.Level) + 1;

            //if (this.owner.HeroId == HeroId.npc_dota_hero_meepo)
            //{
            //    if (spentPoints >= 15)
            //    {
            //        spentPoints++;
            //    }
            //}
            //else
            //{
            if (spentPoints >= 10)
            {
                spentPoints++;
            }

            if (spentPoints >= 15)
            {
                spentPoints++;
            }

            if (spentPoints >= 17)
            {
                spentPoints++;
            }
            //}

            return spentPoints;
        }

        private bool IsTalentLearnable()
        {
            return Math.Max((int)this.owner.Hero.Level - 5, 1) / 5 != this.Talents.Count(x => x.CurrentLevel > 0);
        }

        private void OnDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs args)
        {
            try
            {
                var html = args.Result;

                var talentBuild = Regex.Match(html, @"<article class=""show-hero-talents"">.+?</article>");
                if (talentBuild.Success)
                {
                    var group = Regex.Match(talentBuild.Value, @"<tr class=""talent-data-row"">.+?</tr>");

                    while (group.Success)
                    {
                        var level = uint.Parse(
                            Regex.Match(group.Value, @"<td class=""talent-level""><div>(\d{1,2})</div></td>").Groups[1].Value,
                            CultureInfo.InvariantCulture);
                        var talent = Regex.Match(group.Value, @"<td class=""talent-cell"">.+?</td>");

                        while (talent.Success)
                        {
                            this.AddTalent(talent.Value, level);
                            talent = talent.NextMatch();
                        }

                        group = group.NextMatch();
                    }
                }

                var abilityBuild = Regex.Match(
                    html,
                    @"<header>Point Choices By Level</header><article class=""hero_builds_ability_table"">.+?</article>");
                if (abilityBuild.Success)
                {
                    var ability = Regex.Match(abilityBuild.Value, @"<tr>.+?</tr>");
                    if (ability.Success)
                    {
                        var level = Regex.Match(ability.Value, @"<th class=""cell-centered"">(\d{1,2})</th>");
                        while (level.Success)
                        {
                            this.dotabuffAbilityLevels.Add(uint.Parse(level.Groups[1].Value, CultureInfo.InvariantCulture));
                            level = level.NextMatch();
                        }
                    }

                    ability = ability.NextMatch();

                    while (ability.Success)
                    {
                        this.AddAbility(ability.Value);
                        ability = ability.NextMatch();
                    }

                    if (this.owner.HeroId == HeroId.npc_dota_hero_meepo)
                    {
                        this.Abilities.RemoveAll(x => x.LearnLevel == 15);
                    }
                    else
                    {
                        this.Abilities.RemoveAll(x => x.LearnLevel == 10 || x.LearnLevel == 15 || x.LearnLevel == 17);
                    }
                }

                this.BuildReady?.Invoke(null, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Logger.Error(e, this.nameManager.HeroName);
            }
        }
    }
}