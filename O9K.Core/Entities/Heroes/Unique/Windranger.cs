namespace O9K.Core.Entities.Heroes.Unique
{
    using System;

    using Ensage;

    using Helpers;

    using Logger;

    using Managers.Entity;

    using Metadata;

    using Units;

    [HeroId(HeroId.npc_dota_hero_windrunner)]
    public class Windranger : Hero9, IDisposable
    {
        private readonly float focusFireAttackSpeed;

        public Windranger(Hero baseHero)
            : base(baseHero)
        {
            this.focusFireAttackSpeed = new SpecialData(AbilityId.windrunner_focusfire, "bonus_attack_speed").GetValue(1);

            if (this.IsControllable)
            {
                Player.OnExecuteOrder += this.OnExecuteOrder;
            }
        }

        public bool FocusFireActive { get; private set; }

        public Unit9 FocusFireTarget { get; private set; }

        public void Dispose()
        {
            Player.OnExecuteOrder -= this.OnExecuteOrder;
            Unit.OnModifierAdded -= this.OnModifierAdded;
            Unit.OnModifierRemoved -= this.OnModifierRemoved;
        }

        internal override float GetAttackSpeed(Unit9 target = null)
        {
            var attackSpeed = base.GetAttackSpeed(target);
            if (target == null || !this.FocusFireActive || this.FocusFireTarget?.Handle == target.Handle)
            {
                return attackSpeed;
            }

            return attackSpeed - this.focusFireAttackSpeed;
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (args.OrderId != OrderId.AbilityTarget || args.Ability.Id != AbilityId.windrunner_focusfire || !args.Process)
                {
                    return;
                }

                this.FocusFireTarget = EntityManager9.GetUnit(args.Target.Handle);
                if (this.FocusFireTarget == null || this.FocusFireTarget.IsLinkensProtected || this.FocusFireTarget.IsSpellShieldProtected)
                {
                    return;
                }

                Unit.OnModifierAdded += this.OnModifierAdded;
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
                if (sender.Handle != this.Handle || args.Modifier.Name != "modifier_windrunner_focusfire")
                {
                    return;
                }

                this.FocusFireActive = true;

                Unit.OnModifierAdded -= this.OnModifierAdded;
                Unit.OnModifierRemoved += this.OnModifierRemoved;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Handle != this.Handle || args.Modifier.Name != "modifier_windrunner_focusfire")
                {
                    return;
                }

                this.FocusFireActive = false;

                Unit.OnModifierRemoved -= this.OnModifierRemoved;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}