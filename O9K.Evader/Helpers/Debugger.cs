namespace O9K.Evader.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Abilities.Base;

    using Core.Entities.Heroes;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu.EventArgs;

    using Ensage;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer;

    using Evader.EvadeModes;

    using Metadata;

    using Pathfinder;
    using Pathfinder.Obstacles.Types;

    using Settings;

    using SharpDX;

    using Color = System.Drawing.Color;

    [Export(typeof(IDebugger))]
    internal class Debugger : IEvaderService, IDebugger
    {
        private const float TextSize = 18f;

        private readonly IAbilityManager abilityManager;

        private readonly IActionManager actionManager;

        private readonly IContext9 context;

        private readonly List<EvadeResult> evadeResults = new List<EvadeResult>();

        private readonly Pathfinder pathfinder;

        private readonly DebugMenu settings;

        private Owner owner;

        [ImportingConstructor]
        public Debugger(
            IContext9 context,
            IPathfinder pathfinder,
            IMainMenu menu,
            IAbilityManager abilityManager,
            IActionManager actionManager)
        {
            this.pathfinder = (Pathfinder)pathfinder;
            this.settings = menu.Debug;
            this.context = context;
            this.abilityManager = abilityManager;
            this.actionManager = actionManager;
        }

        public LoadOrder LoadOrder { get; } = LoadOrder.Debugger;

        public void Activate()
        {
            this.owner = EntityManager9.Owner;

            this.settings.DrawAbilities.ValueChange += this.DrawAbilitiesOnValueChanged;
            this.settings.DrawEvadeResult.ValueChange += this.DrawEvadeResultOnValueChanged;
            this.settings.DrawObstacleMap.ValueChange += this.DrawObstacleMapOnValueChanged;
            this.settings.DrawIntersections.ValueChange += this.DrawIntersectionsOnValueChanged;
            this.settings.DrawUsableAbilities.ValueChange += this.DrawUsableAbilitiesOnValueChanged;
            this.settings.DrawEvadableAbilities.ValueChange += this.DrawEvadableAbilitiesOnValueChanged;
        }

        public void AddEvadeResult(EvadeResult evadeResult)
        {
            if (!this.settings.DrawEvadeResult.IsEnabled)
            {
                return;
            }

            if (evadeResult == null || evadeResult.State == EvadeResult.EvadeState.TooEarly
                                    || evadeResult.State == EvadeResult.EvadeState.Ignore)
            {
                return;
            }

            if (this.evadeResults.Contains(evadeResult))
            {
                return;
            }

            if (this.evadeResults.Count >= 7)
            {
                this.evadeResults.Remove(this.evadeResults.Last());
            }

            this.evadeResults.Add(evadeResult);
            UpdateManager.BeginInvoke(() => this.evadeResults.Remove(evadeResult), 7500);
        }

        public void Dispose()
        {
            this.context.Renderer.Draw -= this.DrawEvadableAbilities;
            this.context.Renderer.Draw -= this.DrawIntersections;
            this.context.Renderer.Draw -= this.DrawUsableAbilities;
            Drawing.OnDraw -= this.DrawAbilityObstacles;
            Drawing.OnDraw -= this.DrawMap;
        }

        private void DrawAbilitiesOnValueChanged(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                if (e.OldValue)
                {
                    //to prevent mini freeze on 1st particle creation on usage
                    var fix = new AbilityObstacleDrawer();
                    fix.DrawArcRectangle(Vector3.Zero, Vector3.Zero + 100, 100, 200);
                    fix.DrawCircle(Vector3.Zero, 100);
                }

                Drawing.OnDraw += this.DrawAbilityObstacles;
            }
            else
            {
                Drawing.OnDraw -= this.DrawAbilityObstacles;
            }
        }

        private void DrawAbilityObstacles(EventArgs args)
        {
            try
            {
                foreach (var obstacle in this.pathfinder.Obstacles.ToList())
                {
                    if (obstacle is IDrawable drawable)
                    {
                        drawable.Draw();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Warn(e.ToString());
            }
        }

        private void DrawEvadableAbilities(IRenderer renderer)
        {
            try
            {
                foreach (var unit in this.abilityManager.EvadableAbilities.Select(x => x.Ability.Owner)
                    .Where(x => x.IsAlive && x.IsVisible)
                    .Distinct()
                    .ToList())
                {
                    var position = Drawing.WorldToScreen(unit.Position);
                    if (position.IsZero)
                    {
                        continue;
                    }

                    position -= new Vector2(-50, 110);

                    foreach (var ability in this.abilityManager.EvadableAbilities.Where(x => x.Owner.Equals(unit))
                        .OrderBy(x => x.Ability.BaseAbility.AbilitySlot)
                        .ToList())
                    {
                        var text = ability.Ability.DisplayName;
                        if (ability is IModifierCounter modifier)
                        {
                            text += " (Modifier " + (modifier.ModifierEnemyCounter ? "enemy" : "ally") + ")";
                        }

                        renderer.DrawText(
                            (position += new Vector2(0, 20)) + new Vector2(20, 0),
                            text,
                            ability.Ability.BaseAbility.IsInAbilityPhase ? Color.LawnGreen :
                            ability.Ability.CanBeCasted() ? Color.White : Color.Gray,
                            TextSize,
                            "Arial");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Warn(e.ToString());
            }
        }

        private void DrawEvadableAbilitiesOnValueChanged(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.context.Renderer.Draw += this.DrawEvadableAbilities;
            }
            else
            {
                this.context.Renderer.Draw -= this.DrawEvadableAbilities;
            }
        }

        private void DrawEvadeResultOnValueChanged(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.context.Renderer.Draw += this.ShowEvadeResult;
            }
            else
            {
                this.context.Renderer.Draw -= this.ShowEvadeResult;
            }
        }

        private void DrawIntersections(IRenderer renderer)
        {
            try
            {
                foreach (var unit in EntityManager9.Units.Where(x => x.IsHero && x.IsAlive && x.IsAlly(this.owner.Team)).ToList())
                {
                    var obstacles = this.pathfinder.GetIntersectingObstacles(unit).ToList();
                    if (obstacles.Count == 0)
                    {
                        continue;
                    }

                    var position = Drawing.WorldToScreen(unit.Position);
                    if (position.IsZero)
                    {
                        continue;
                    }

                    foreach (var obstacle in obstacles)
                    {
                        renderer.DrawText(
                            (position += new Vector2(0, 20)) + new Vector2(-120, 0),
                            obstacle.EvadableAbility.Ability.DisplayName + " (" + obstacle.Id + ") "
                            + obstacle.GetEvadeTime(unit, false).ToString("n2"),
                            this.actionManager.IsObstacleIgnored(unit, obstacle) ? Color.Gray : Color.White,
                            TextSize,
                            "Arial");
                    }
                }
            }
            catch
            {
                //ignored
            }
        }

        private void DrawIntersectionsOnValueChanged(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.context.Renderer.Draw += this.DrawIntersections;
            }
            else
            {
                this.context.Renderer.Draw -= this.DrawIntersections;
            }
        }

        private void DrawMap(EventArgs args)
        {
            try
            {
#pragma warning disable 618

                var center = Game.MousePosition;
                const int CellCount = 40;
                for (var i = 0; i < CellCount; ++i)
                {
                    for (var j = 0; j < CellCount; ++j)
                    {
                        Vector2 p;
                        p.X = (this.pathfinder.NavMesh.CellSize * (i - (CellCount / 2))) + center.X;
                        p.Y = (this.pathfinder.NavMesh.CellSize * (j - (CellCount / 2))) + center.Y;
                        SharpDX.Color color;

                        var isFlying = this.owner.Hero.MoveCapability == MoveCapability.Fly
                                       || (this.owner.Hero.UnitState & UnitState.Flying) != 0;
                        var flag = this.pathfinder.NavMesh.GetCellFlags(p);
                        if (!isFlying && (flag & NavMeshCellFlags.Walkable) != 0)
                        {
                            color = (flag & NavMeshCellFlags.Tree) != 0 ? SharpDX.Color.Purple : SharpDX.Color.Green;
                            if ((flag & NavMeshCellFlags.GridFlagObstacle) != 0)
                            {
                                color = SharpDX.Color.Pink;
                            }
                        }
                        else if (isFlying && (flag & NavMeshCellFlags.MovementBlocker) == 0)
                        {
                            color = SharpDX.Color.Green;
                        }
                        else
                        {
                            color = SharpDX.Color.Red;
                        }

                        Drawing.DrawRect(new Vector2(i * 10, 50 + ((CellCount - j - 1) * 10)), new Vector2(9), color, false);
                    }
                }

                this.pathfinder.NavMesh.GetCellPosition(this.owner.Hero.Position - center, out var heroX, out var heroY);
                heroX += CellCount / 2;
                heroY += CellCount / 2;

                if (heroX >= 0 && heroX < CellCount && heroY >= 0 && heroY < CellCount)
                {
                    Drawing.DrawRect(
                        new Vector2(heroX * 10, 50 + ((CellCount - heroY - 1) * 10)),
                        new Vector2(9),
                        SharpDX.Color.Blue,
                        false);
                }

                //this.pathfinder.NavMesh.GetCellPosition(Game.MousePosition - center, out var mouseX, out var mouseY);
                //mouseX += CellCount / 2;
                //mouseY += CellCount / 2;

                //if (mouseX >= 0 && mouseX < CellCount && mouseY >= 0 && mouseY < CellCount)
                //{
                //    Drawing.DrawRect(
                //        new Vector2(mouseX * 10, 50 + ((CellCount - mouseY - 1) * 10)),
                //        new Vector2(9),
                //        SharpDX.Color.White,
                //        false);
                //}

#pragma warning restore 618
            }
            catch (Exception e)
            {
                Logger.Warn(e.ToString());
            }
        }

        private void DrawObstacleMapOnValueChanged(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                Drawing.OnDraw += this.DrawMap;
            }
            else
            {
                Drawing.OnDraw -= this.DrawMap;
            }
        }

        private void DrawUsableAbilities(IRenderer renderer)
        {
            try
            {
                var units = this.abilityManager.UsableBlinkAbilities.Select(x => x.Ability.Owner)
                    .Concat(this.abilityManager.UsableCounterAbilities.Select(x => x.Ability.Owner))
                    .Concat(this.abilityManager.UsableDisableAbilities.Select(x => x.Ability.Owner))
                    .Where(x => x.IsAlive)
                    .Distinct()
                    .ToList();

                foreach (var unit in units)
                {
                    var position = Drawing.WorldToScreen(unit.Position);
                    if (position.IsZero)
                    {
                        continue;
                    }

                    position -= new Vector2(200, 110);

                    if (this.actionManager.IsInputBlocked(unit))
                    {
                        renderer.DrawText(position + new Vector2(150, 20), "Blocked", Color.Red, TextSize, "Arial");
                    }

                    var blinks = this.abilityManager.UsableBlinkAbilities.Where(x => x.Ability.Owner.Equals(unit))
                        .OrderBy(x => x.Ability.BaseAbility.AbilitySlot)
                        .ToList();

                    if (blinks.Count > 0)
                    {
                        renderer.DrawText((position += new Vector2(0, 20)) + new Vector2(10, 0), "Blinks:", Color.White, TextSize, "Arial");

                        foreach (var ability in blinks)
                        {
                            renderer.DrawText(
                                (position += new Vector2(0, 20)) + new Vector2(20, 0),
                                ability.Ability.DisplayName,
                                ability.Ability.BaseAbility.IsInAbilityPhase ? Color.LawnGreen :
                                ability.Ability.CanBeCasted() ? Color.White : Color.Gray,
                                TextSize,
                                "Arial");
                        }
                    }

                    var counters = this.abilityManager.UsableCounterAbilities.Where(x => x.Ability.Owner.Equals(unit))
                        .OrderBy(x => x.Ability.BaseAbility.AbilitySlot)
                        .ToList();

                    if (counters.Count > 0)
                    {
                        renderer.DrawText(
                            (position += new Vector2(0, 20)) + new Vector2(10, 0),
                            "Counters:",
                            Color.White,
                            TextSize,
                            "Arial");

                        foreach (var ability in counters)
                        {
                            renderer.DrawText(
                                (position += new Vector2(0, 20)) + new Vector2(20, 0),
                                ability.Ability.DisplayName,
                                ability.Ability.BaseAbility.IsInAbilityPhase ? Color.LawnGreen :
                                ability.Ability.CanBeCasted() ? Color.White : Color.Gray,
                                TextSize,
                                "Arial");
                        }
                    }

                    var disables = this.abilityManager.UsableDisableAbilities.Where(x => x.Ability.Owner.Equals(unit))
                        .OrderBy(x => x.Ability.BaseAbility.AbilitySlot)
                        .ToList();

                    if (disables.Count > 0)
                    {
                        renderer.DrawText(
                            (position += new Vector2(0, 20)) + new Vector2(10, 0),
                            "Disables:",
                            Color.White,
                            TextSize,
                            "Arial");

                        foreach (var ability in disables)
                        {
                            renderer.DrawText(
                                (position += new Vector2(0, 20)) + new Vector2(20, 0),
                                ability.Ability.DisplayName,
                                ability.Ability.BaseAbility.IsInAbilityPhase ? Color.LawnGreen :
                                ability.Ability.CanBeCasted() ? Color.White : Color.Gray,
                                TextSize,
                                "Arial");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Warn(e.ToString());
            }
        }

        private void DrawUsableAbilitiesOnValueChanged(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.context.Renderer.Draw += this.DrawUsableAbilities;
            }
            else
            {
                this.context.Renderer.Draw -= this.DrawUsableAbilities;
            }
        }

        private void ShowEvadeResult(IRenderer renderer)
        {
            try
            {
                var position = new Vector2(Hud.Info.ScreenSize.X, 23);

                for (var i = this.evadeResults.Count - 1; i >= 0; i--)
                {
                    var result = this.evadeResults[i];
                    var text = string.Empty;

                    if (result.State == EvadeResult.EvadeState.Failed)
                    {
                        text = result.Ally + " can't evade " + result.EnemyAbility + (result.IsModifier ? " (modifier)" : "");
                    }
                    else
                    {
                        switch (result.Mode)
                        {
                            case EvadeMode.Dodge:
                            {
                                if (result.AllyAbility == null)
                                {
                                    text = result.Ally + " dodging " + result.EnemyAbility;
                                }
                                else
                                {
                                    text = result.Ally + " using " + result.AllyAbility + " to dodge " + result.EnemyAbility;
                                }

                                break;
                            }
                            case EvadeMode.Counter:
                            {
                                text = result.AbilityOwner + " using " + result.AllyAbility + " (" + (result.IsModifier ? "modifier " : "")
                                       + "counter) on " + result.Ally + " vs " + result.EnemyAbility;
                                break;
                            }
                            case EvadeMode.Blink:
                            {
                                text = result.AbilityOwner + " using " + result.AllyAbility + " (" + (result.IsModifier ? "modifier " : "")
                                       + "blink) vs " + result.EnemyAbility;
                                break;
                            }
                            case EvadeMode.Disable:
                            {
                                text = result.AbilityOwner + " using " + result.AllyAbility + " (" + (result.IsModifier ? "modifier " : "")
                                       + "disable) on " + result.Enemy + " vs " + result.EnemyAbility;
                                break;
                            }
                            case EvadeMode.GoldSpend:
                            {
                                text = result.Ally + " trying to spend gold vs " + result.EnemyAbility;
                                break;
                            }
                        }
                    }

                    var size = renderer.MeasureText(text, TextSize + 2);
                    position += new Vector2(0, TextSize + 2);

                    renderer.DrawLine(
                        position - new Vector2(size.X + 20, ((TextSize + 2) / -2) - 3),
                        position - new Vector2(0, ((TextSize + 2) / -2) - 3),
                        Color.FromArgb(220, 0, 0, 0),
                        TextSize + 2);

                    renderer.DrawText(
                        position - new Vector2(size.X + 10, 0),
                        text,
                        result.State == EvadeResult.EvadeState.Failed ? Color.Red : Color.Green,
                        TextSize + 2);
                }
            }
            catch
            {
                //ignore
            }
        }
    }
}