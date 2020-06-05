namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes.Unique
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Extensions;

    using Ensage;

    [AbilityId(AbilityId.tusk_ice_shards)]
    internal class IceShardsNuke : OldNukeAbility
    {
        public IceShardsNuke(INuke ability)
            : base(ability)
        {
        }

        public override bool Use(Unit9 target)
        {
            var input = this.Ability.GetPredictionInput(target);
            input.Delay += 0.5f;
            var output = this.Ability.GetPredictionOutput(input);

            var castPosition = output.CastPosition.Extend2D(this.Owner.Position, -200);

            if (this.Owner.Distance(castPosition) > this.Ability.CastRange)
            {
                return false;
            }

            if (!this.Ability.UseAbility(castPosition))
            {
                return false;
            }

            this.OrbwalkSleeper.Sleep(this.Ability.Owner.Handle, this.Ability.GetCastDelay(target));
            this.AbilitySleeper.Sleep(this.Ability.Handle, this.Ability.GetHitTime(target) + 0.5f);

            return true;
        }
    }
}