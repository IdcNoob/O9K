namespace O9K.AIO.ShieldBreaker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    using Heroes.Base;

    using Modes.Base;
    using Modes.Combo;

    using TargetManager;

    using UnitManager;

    internal class ShieldBreaker : BaseMode
    {
        private readonly Dictionary<MenuHoldKey, ComboModeMenu> comboMenus = new Dictionary<MenuHoldKey, ComboModeMenu>();

        private readonly MultiSleeper linkensSleeper = new MultiSleeper();

        private readonly MultiSleeper orbwalkerSleeper;

        private readonly ShieldBreakerMenu shieldBreakerMenu;

        private readonly TargetManager targetManager;

        private readonly IUpdateHandler updateHandler;

        private ComboModeMenu comboModeMenu;

        public ShieldBreaker(BaseHero baseHero)
            : base(baseHero)
        {
            this.targetManager = baseHero.TargetManager;
            this.orbwalkerSleeper = baseHero.OrbwalkSleeper;
            this.shieldBreakerMenu = new ShieldBreakerMenu(baseHero.Menu.RootMenu);

            this.updateHandler = UpdateManager.Subscribe(this.OnUpdate, 0, false);
        }

        public UnitManager UnitManager { get; set; }

        public void AddComboMenu(IEnumerable<ComboModeMenu> menus)
        {
            foreach (var comboMenu in menus)
            {
                this.comboMenus.Add(comboMenu.Key, comboMenu);
            }
        }

        public void Disable()
        {
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            this.updateHandler.IsEnabled = false;

            foreach (var comboMenu in this.comboMenus)
            {
                comboMenu.Key.ValueChange -= this.KeyOnValueChanged;
            }
        }

        public override void Dispose()
        {
            UpdateManager.Unsubscribe(this.updateHandler);
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            foreach (var comboMenu in this.comboMenus)
            {
                comboMenu.Key.ValueChange -= this.KeyOnValueChanged;
            }
        }

        public void Enable()
        {
            EntityManager9.AbilityAdded += this.OnAbilityAdded;

            foreach (var comboMenu in this.comboMenus)
            {
                comboMenu.Key.ValueChange += this.KeyOnValueChanged;
            }
        }

        private void KeyOnValueChanged(object sender, KeyEventArgs e)
        {
            this.updateHandler.IsEnabled = e.NewValue;
            this.comboModeMenu = this.comboMenus[(MenuHoldKey)sender];
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (!ability.IsControllable || ability.IsFake || !ability.Owner.IsAlly(this.Owner.Team) || !ability.Owner.IsMyControllable
                    || !(ability is ActiveAbility active))
                {
                    return;
                }

                if (!active.UnitTargetCast || !active.TargetsEnemy || !active.BreaksLinkens)
                {
                    return;
                }

                this.shieldBreakerMenu.AddBreakerAbility(ability);
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
                if (!this.targetManager.HasValidTarget)
                {
                    return;
                }

                if (!this.comboModeMenu.IgnoreInvisibility && this.Owner.Hero.IsInvisible)
                {
                    return;
                }

                if (this.targetManager.TargetSleeper.IsSleeping)
                {
                    return;
                }

                var target = this.targetManager.Target;

                foreach (var unit in this.UnitManager.ControllableUnits)
                {
                    var abilities = unit.Owner.Abilities
                        .Where(
                            x => (this.shieldBreakerMenu.IsLinkensBreakerEnabled(x.Name)
                                  || this.shieldBreakerMenu.IsSpellShieldBreakerEnabled(x.Name)) && x.CanBeCasted())
                        .OfType<ActiveAbility>()
                        .OrderBy(x => x.CastPoint);

                    foreach (var ability in abilities)
                    {
                        if (!ability.CanHit(target) || !this.ShouldUseBreaker(ability, unit.Owner, target))
                        {
                            continue;
                        }

                        if (ability.UseAbility(target))
                        {
                            this.linkensSleeper.Sleep(unit.Handle, ability.GetHitTime(target) + 5.5f);
                            this.orbwalkerSleeper.Sleep(unit.Handle, ability.GetCastDelay(target));
                            this.targetManager.TargetSleeper.Sleep(ability.GetHitTime(target) + 0.1f);
                            return;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private bool ShouldUseBreaker(IActiveAbility ability, Unit9 owner, Unit9 target)
        {
            if (!ability.UnitTargetCast || this.linkensSleeper.IsSleeping(target.Handle))
            {
                return false;
            }

            if (target.IsUntargetable || !target.IsVisible || target.IsInvulnerable)
            {
                return false;
            }

            if (owner.Abilities.OfType<IDisable>().Any(x => x.NoTargetCast && x.CanBeCasted() && x.CanHit(target)))
            {
                return false;
            }

            if ((target.IsLinkensProtected && target.IsLotusProtected) || target.IsSpellShieldProtected)
            {
                return this.shieldBreakerMenu.IsSpellShieldBreakerEnabled(ability.Name);
            }

            if (target.IsLinkensProtected && this.shieldBreakerMenu.IsLinkensBreakerEnabled(ability.Name))
            {
                return true;
            }

            return false;
        }
    }
}