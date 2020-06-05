namespace O9K.Hud.Modules.Units.Ranges
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage.SDK.Helpers;

    using MainMenu;

    internal class Ranges : IHudModule
    {
        private readonly IContext9 context;

        private readonly Menu menu;

        private readonly List<RangeUnit> rangeUnits = new List<RangeUnit>();

        [ImportingConstructor]
        public Ranges(IContext9 context, IHudMenu hudMenu)
        {
            this.context = context;
            this.menu = hudMenu.UnitsMenu.Add(new Menu("Ranges"));
            this.menu.AddTranslation(Lang.Ru, "Радиус способностей");
            this.menu.AddTranslation(Lang.Cn, "范围");
        }

        public void Activate()
        {
            this.context.Renderer.TextureManager.LoadFromDota("o9k.attack", @"panorama\images\hud\reborn\ping_icon_attack_psd.vtex_c");
            this.context.Renderer.TextureManager.LoadFromDota("o9k.exp_plus", @"panorama\images\hud\reborn\levelup_plus_fill_psd.vtex_c");

            EntityManager9.UnitAdded += this.OnUnitAdded;
            EntityManager9.UnitRemoved += this.OnUnitRemoved;
            EntityManager9.AbilityAdded += this.OnAbilityAdded;
            EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
            UpdateManager.Subscribe(this.OnUpdate, 3000);
        }

        public void Dispose()
        {
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
            UpdateManager.Unsubscribe(this.OnUpdate);
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (ability.IsTalent || ability.IsStolen)
                {
                    return;
                }

                var rangeUnit = this.rangeUnits.Find(x => x.Handle == ability.Owner.Handle);
                if (rangeUnit == null)
                {
                    return;
                }

                rangeUnit.AddAbility(ability);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAbilityRemoved(Ability9 ability)
        {
            try
            {
                if (ability.IsTalent || ability.IsStolen)
                {
                    return;
                }

                var rangeUnit = this.rangeUnits.Find(x => x.Handle == ability.Owner.Handle);
                if (rangeUnit == null)
                {
                    return;
                }

                rangeUnit.RemoveAbility(ability);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitAdded(Unit9 unit)
        {
            if (!unit.IsHero || unit.IsIllusion)
            {
                return;
            }

            this.rangeUnits.Add(new RangeUnit(unit, this.menu));
        }

        private void OnUnitRemoved(Unit9 unit)
        {
            if (!unit.IsHero || unit.IsIllusion)
            {
                return;
            }

            var rangeUnit = this.rangeUnits.Find(x => x.Handle == unit.Handle);
            if (rangeUnit == null)
            {
                return;
            }

            rangeUnit.Dispose();
            this.rangeUnits.Remove(rangeUnit);
        }

        private void OnUpdate()
        {
            try
            {
                foreach (var unit in this.rangeUnits)
                {
                    unit.UpdateRanges();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}