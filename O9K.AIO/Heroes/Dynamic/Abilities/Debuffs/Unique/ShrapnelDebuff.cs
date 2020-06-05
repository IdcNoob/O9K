namespace O9K.AIO.Heroes.Dynamic.Abilities.Debuffs.Unique
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;

    [AbilityId(AbilityId.sniper_shrapnel)]
    internal class ShrapnelDebuff : OldDebuffAbility
    {
        public ShrapnelDebuff(IDebuff ability)
            : base(ability)
        {
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
            this.AbilitySleeper.Sleep(this.Ability.Handle, 2);

            return true;
        }
    }
}