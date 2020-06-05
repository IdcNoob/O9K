namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes.Unique
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;

    [AbilityId(AbilityId.skywrath_mage_mystic_flare)]
    internal class MysticFlareNukeAbility : OldNukeAbility
    {
        public MysticFlareNukeAbility(INuke ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(Unit9 target)
        {
            if (!base.ShouldCast(target))
            {
                return false;
            }

            if (target.IsStunned || target.IsRooted || target.IsHexed)
            {
                return target.GetImmobilityDuration() > 0;
            }

            if (target.HasModifier("modifier_skywrath_mage_concussive_shot_slow"))
            {
                return true;
            }

            if (target.Speed < 250)
            {
                return true;
            }

            return false;
        }

        public override bool Use(Unit9 target)
        {
            var input = this.Ability.GetPredictionInput(target);
            input.Delay += 0.5f;
            var output = this.Ability.GetPredictionOutput(input);

            if (!this.Ability.UseAbility(output.CastPosition))
            {
                return false;
            }

            this.OrbwalkSleeper.Sleep(this.Ability.Owner.Handle, this.Ability.GetCastDelay(target));
            this.AbilitySleeper.Sleep(this.Ability.Handle, this.Ability.GetHitTime(target) + 0.5f);

            return true;
        }
    }
}