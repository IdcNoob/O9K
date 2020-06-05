namespace O9K.Farm.Core.Marker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer;

    using Menu;

    using O9K.Core.Entities.Abilities.Base;
    using O9K.Core.Entities.Abilities.Base.Types;
    using O9K.Core.Entities.Heroes;
    using O9K.Core.Logger;
    using O9K.Core.Managers.Context;
    using O9K.Core.Managers.Entity;
    using O9K.Core.Managers.Menu.EventArgs;
    using O9K.Core.Managers.Renderer.Utils;

    using SharpDX;

    using Units.Base;

    using Color = System.Drawing.Color;

    internal class LastHitMarker : IDisposable
    {
        private readonly IContext9 context;

        private readonly HashSet<AbilityId> includeAbilities = new HashSet<AbilityId>
        {
            AbilityId.shredder_timber_chain
        };

        private readonly MarkerMenu menu;

        private readonly Owner owner;

        private readonly UnitManager unitManager;

        private Dictionary<FarmUnit, DamageData> unitDamage = new Dictionary<FarmUnit, DamageData>();

        public LastHitMarker(IContext9 context, UnitManager unitManager, MenuManager menuManager)
        {
            this.context = context;
            this.unitManager = unitManager;
            this.menu = menuManager.MarkerMenu;
            this.owner = EntityManager9.Owner;

            this.menu.Enabled.ValueChange += this.OnValueChange;
        }

        public void Dispose()
        {
            this.menu.Enabled.ValueChange -= this.OnValueChange;
            UpdateManager.Unsubscribe(this.OnUpdate);
            Unit.OnModifierAdded -= this.OnModifierChange;
            Unit.OnModifierRemoved -= this.OnModifierChange;
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            this.context.Renderer.Draw -= this.OnDraw;
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (!ability.Owner.IsMyHero || ability.IsItem || (!(ability is INuke) && !this.includeAbilities.Contains(ability.Id)))
                {
                    return;
                }

                this.menu.AddAbility(ability);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                foreach (var unitPair in this.unitDamage)
                {
                    var unit = unitPair.Key;
                    if (!unit.IsValid)
                    {
                        continue;
                    }

                    var hpBarPosition = unit.Unit.HealthBarPosition;
                    if (hpBarPosition.IsZero)
                    {
                        continue;
                    }

                    var damage = unitPair.Value;
                    var hpBarSize = unit.Unit.HealthBarSize;
                    var attackDamage = damage.AutoAttackDamage;
                    var health = unit.Unit.Health;

                    if (this.menu.AttacksEnabled)
                    {
                        var attackDamagePct = attackDamage / unit.Unit.MaximumHealth;

                        Color color;
                        if (attackDamage > health)
                        {
                            color = unit.IsAlly ? Color.LawnGreen : Color.OrangeRed;
                        }
                        else
                        {
                            color = unit.IsAlly ? Color.DarkGreen : Color.DarkRed;
                        }

                        var bar = new Rectangle9(
                            hpBarPosition + new Vector2(this.menu.AttacksX, this.menu.AttacksY),
                            hpBarSize + new Vector2(this.menu.AttacksSizeX, this.menu.AttacksSizeY));

                        bar.Width *= Math.Min(attackDamagePct, unit.Unit.HealthPercentageBase);
                        renderer.DrawFilledRectangle(bar, color);
                    }

                    if (this.menu.AbilitiesEnabled)
                    {
                        var fullDamage = damage.AbilityDamage.Sum(x => x.Value);
                        if (fullDamage + attackDamage < health)
                        {
                            continue;
                        }

                        if (fullDamage > health)
                        {
                            var totalDamage = 0;
                            var drawAbilities = new List<string>();

                            foreach (var damagePair in damage.AbilityDamage)
                            {
                                totalDamage += damagePair.Value;
                                drawAbilities.Add(damagePair.Key.Name);

                                if (totalDamage > health)
                                {
                                    break;
                                }
                            }

                            var count = drawAbilities.Count;
                            if (count > 0)
                            {
                                var barSize = new Vector2(this.menu.AbilitiesSize * count, this.menu.AbilitiesSize);
                                var startPosition = hpBarPosition + new Vector2((hpBarSize.X / 2f) - (barSize.X / 2f), -35)
                                                                  + new Vector2(this.menu.AbilitiesX, this.menu.AbilitiesY);

                                renderer.DrawRectangle(
                                    new RectangleF(startPosition.X, startPosition.Y, barSize.X, barSize.Y),
                                    Color.Green,
                                    5);

                                foreach (var ability in drawAbilities)
                                {
                                    renderer.DrawTexture(ability, startPosition, new Vector2(barSize.Y, barSize.Y));
                                    startPosition += new Vector2(barSize.Y, 0);
                                }
                            }
                        }
                        else
                        {
                            var count = damage.AbilityDamage.Count;
                            if (count > 0)
                            {
                                var barSize = new Vector2(this.menu.AbilitiesSize * count, this.menu.AbilitiesSize);
                                var startPosition = hpBarPosition + new Vector2((hpBarSize.X / 2f) - (barSize.X / 2f), -35)
                                                                  + new Vector2(this.menu.AbilitiesX, this.menu.AbilitiesY);

                                renderer.DrawRectangle(
                                    new RectangleF(startPosition.X, startPosition.Y, barSize.X, barSize.Y),
                                    Color.Orange,
                                    5);

                                foreach (var ability in damage.AbilityDamage.Select(x => x.Key))
                                {
                                    renderer.DrawTexture(ability.Name, startPosition, new Vector2(barSize.Y, barSize.Y));
                                    startPosition += new Vector2(barSize.Y, 0);
                                }
                            }
                        }
                    }
                }
            }
            catch (InvalidOperationException)
            {
                // ignore
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnModifierChange(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender?.Handle != this.owner.HeroHandle)
                {
                    return;
                }

                this.OnUpdate();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUpdate()
        {
            try
            {
                var myHero = this.unitManager.Units.FirstOrDefault(x => x.IsMyHero);
                if (myHero == null)
                {
                    return;
                }

                if (this.menu.AbilitiesEnabled)
                {
                    var myAbilities = this.owner.Hero.Abilities.Where(x => this.menu.Abilities.IsEnabled(x.Name))
                        .OrderBy(x => this.menu.Abilities.GetPriority(x.Name))
                        .OfType<ActiveAbility>()
                        .Where(x => x.CanBeCasted())
                        .ToArray();

                    this.unitDamage = this.unitManager.Units.Where(x => !x.IsHero && x.Unit.Distance(myHero.Unit) < 1000)
                        .ToDictionary(x => x, x => new DamageData(myHero, myAbilities, x));
                }
                else
                {
                    this.unitDamage = this.unitManager.Units.Where(x => !x.IsHero && x.Unit.Distance(myHero.Unit) < 1000)
                        .ToDictionary(x => x, x => new DamageData(myHero, x));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                UpdateManager.Subscribe(this.OnUpdate, 500);
                Unit.OnModifierAdded += this.OnModifierChange;
                Unit.OnModifierRemoved += this.OnModifierChange;
                EntityManager9.AbilityAdded += this.OnAbilityAdded;
                this.context.Renderer.Draw += this.OnDraw;
            }
            else
            {
                UpdateManager.Unsubscribe(this.OnUpdate);
                Unit.OnModifierAdded -= this.OnModifierChange;
                Unit.OnModifierRemoved -= this.OnModifierChange;
                EntityManager9.AbilityAdded -= this.OnAbilityAdded;
                this.context.Renderer.Draw -= this.OnDraw;
            }
        }
    }
}