namespace O9K.Hud.Modules.Particles.Units
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Heroes;
    using Core.Entities.Units;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    using MainMenu;

    using SharpDX;

    internal class HeroDeny //: IHudModule
    {
        private readonly HashSet<AbilityId> denyAbilities = new HashSet<AbilityId>
        {
            AbilityId.queenofpain_shadow_strike,
            AbilityId.doom_bringer_doom,
            AbilityId.venomancer_venomous_gale,
        };

        private readonly HashSet<string> denyModifiers = new HashSet<string>
        {
            "modifier_queenofpain_shadow_strike",
            "modifier_doom_bringer_doom",
            "modifier_venomancer_venomous_gale",
        };

        private readonly MenuSwitcher show;

        private readonly Dictionary<Unit9, ParticleEffect> units = new Dictionary<Unit9, ParticleEffect>();

        private bool added;

        private IUpdateHandler handler;

        private Owner owner;

        [ImportingConstructor]
        public HeroDeny(IHudMenu hudMenu)
        {
            //todo delete?

            this.show = hudMenu.ParticlesMenu.Add(
                new MenuSwitcher("Hero deny").SetTooltip("Highlight ally hero when he can be denied with one hit"));
            this.show.AddTranslation(Lang.Ru, "Денай героя");
            this.show.AddTooltipTranslation(Lang.Ru, "Подсветить союзника, когда он может быть убит одним ударом");
            this.show.AddTranslation(Lang.Cn, "反补英雄");
            this.show.AddTooltipTranslation(Lang.Cn, "突出一個盟友，如果他可以被一擊殺死");
        }

        public void Activate()
        {
            this.owner = EntityManager9.Owner;
            this.handler = UpdateManager.Subscribe(this.OnUpdate, 100, false);
            this.show.ValueChange += this.ShowOnValueChanging;
        }

        public void Dispose()
        {
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            Unit.OnModifierAdded -= this.OnModifierAdded;
            Unit.OnModifierRemoved -= this.OnModifierRemoved;
            UpdateManager.Unsubscribe(this.handler);
            this.added = false;

            foreach (var effect in this.units.Values.Where(x => x?.IsValid == true))
            {
                effect.Dispose();
            }

            this.units.Clear();
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (ability.Owner.Team == this.owner.Team || !this.denyAbilities.Contains(ability.Id))
                {
                    return;
                }

                if (this.added)
                {
                    return;
                }

                EntityManager9.AbilityAdded -= this.OnAbilityAdded;
                Unit.OnModifierAdded += this.OnModifierAdded;
                Unit.OnModifierRemoved += this.OnModifierRemoved;
                this.added = true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Team != this.owner.Team || args.Modifier.IsHidden || !this.denyModifiers.Contains(args.Modifier.Name))
                {
                    return;
                }

                var unit = EntityManager9.GetUnit(sender.Handle);
                if (unit == null || unit.IsMyHero || unit.IsIllusion || !unit.IsHero)
                {
                    return;
                }

                this.units[unit] = null;
                this.handler.IsEnabled = true;
            }
            catch (Exception e)
            {
                Logger.Error(e, sender);
            }
        }

        private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Team != this.owner.Team || args.Modifier.IsHidden || !this.denyModifiers.Contains(args.Modifier.Name))
                {
                    return;
                }

                var unit = EntityManager9.GetUnit(sender.Handle);
                if (unit == null || !this.units.TryGetValue(unit, out var effect))
                {
                    return;
                }

                effect?.Dispose();
                this.units.Remove(unit);
            }
            catch (Exception e)
            {
                Logger.Error(e, sender);
            }
        }

        private void OnUpdate()
        {
            try
            {
                var hero = this.owner.Hero;

                foreach (var pair in this.units.ToList())
                {
                    var unit = pair.Key;
                    var effect = pair.Value;

                    if (!unit.IsValid || !unit.IsAlive)
                    {
                        if (effect?.IsValid == true)
                        {
                            effect.Dispose();
                        }

                        this.units.Remove(unit);
                        continue;
                    }

                    if (unit.HealthPercentage > 25
                        || hero.GetAttackDamage(unit) - (unit.HealthRegeneration * hero.SecondsPerAttack) < unit.Health)
                    {
                        if (effect?.IsValid == true)
                        {
                            effect.Dispose();
                        }

                        continue;
                    }

                    if (effect?.IsValid == true)
                    {
                        continue;
                    }

                    effect = new ParticleEffect(
                        "materials/ensage_ui/particles/illusions_mod_v2.vpcf",
                        unit.BaseUnit,
                        ParticleAttachment.CenterFollow);

                    effect.SetControlPoint(1, new Vector3(255));
                    effect.SetControlPoint(2, new Vector3(0, 255, 128));

                    this.units[unit] = effect;
                }

                if (this.units.Count == 0)
                {
                    this.handler.IsEnabled = false;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void ShowOnValueChanging(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                EntityManager9.AbilityAdded += this.OnAbilityAdded;
            }
            else
            {
                EntityManager9.AbilityAdded -= this.OnAbilityAdded;
                Unit.OnModifierAdded -= this.OnModifierAdded;
                Unit.OnModifierRemoved -= this.OnModifierRemoved;
                this.handler.IsEnabled = false;
                this.added = false;
                foreach (var effect in this.units.Values.Where(x => x?.IsValid == true))
                {
                    effect.Dispose();
                }

                this.units.Clear();
            }
        }
    }
}