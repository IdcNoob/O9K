namespace O9K.AIO.Heroes.DarkWillow.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Modes.Combo;

    using TargetManager;

    using BaseShadowRealm = Core.Entities.Abilities.Heroes.DarkWillow.ShadowRealm;

    internal class ShadowRealm : NukeAbility
    {
        private readonly BaseShadowRealm realm;

        public ShadowRealm(ActiveAbility ability)
            : base(ability)
        {
            this.realm = (BaseShadowRealm)ability;
        }

        public bool Casted
        {
            get
            {
                return this.realm.Casted;
            }
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            if (this.Owner.Distance(targetManager.Target) > 900)
            {
                return false;
            }

            return true;
        }

        public bool ShouldAttack(Unit9 target)
        {
            if (this.Owner.IsReflectingDamage && this.Owner.HealthPercentage < 50)
            {
                return false;
            }

            if (this.Owner.IsMagicImmune && !this.Ability.PiercesMagicImmunity(target))
            {
                return false;
            }

            if (this.realm.GetDamage(target) > target.Health)
            {
                return true;
            }

            if (this.Owner.Distance(target) < 700 || target.IsStunned || target.IsRooted || target.IsHexed)
            {
                return this.realm.DamageMaxed;
            }

            if (this.realm.RealmTime < 1)
            {
                return false;
            }

            return true;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;

            if (!target.IsVisible)
            {
                return false;
            }

            return true;
        }
    }
}