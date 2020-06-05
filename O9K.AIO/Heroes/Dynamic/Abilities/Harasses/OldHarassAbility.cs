namespace O9K.AIO.Heroes.Dynamic.Abilities.Harasses
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    internal class OldHarassAbility : OldUsableAbility
    {
        private readonly bool isOrb;

        public OldHarassAbility(IHarass ability)
            : base(ability)
        {
            //todo move orb to own group ?

            this.Harass = ability;
            this.isOrb = ability is OrbAbility;
        }

        public IHarass Harass { get; }

        public override bool ShouldCast(Unit9 target)
        {
            if (this.isOrb)
            {
                return false;
            }

            if (target.IsInvulnerable)
            {
                return false;
            }

            if (target.IsRooted && !this.Ability.UnitTargetCast && target.GetImmobilityDuration() <= 0)
            {
                return false;
            }

            if (this.Ability.BreaksLinkens && target.IsBlockingAbilities)
            {
                return false;
            }

            if (this.Harass is ToggleAbility toggle && toggle.Enabled)
            {
                return false;
            }

            return true;
        }

        public override bool Use(Unit9 target)
        {
            if (!this.Ability.UseAbility(target, EntityManager9.EnemyHeroes, HitChance.Low))
            {
                return false;
            }

            this.OrbwalkSleeper.Sleep(this.Ability.Owner.Handle, this.Ability.GetCastDelay(target));
            this.AbilitySleeper.Sleep(this.Ability.Handle, this.Ability.GetHitTime(target) + 0.5f);

            return true;
        }
    }
}