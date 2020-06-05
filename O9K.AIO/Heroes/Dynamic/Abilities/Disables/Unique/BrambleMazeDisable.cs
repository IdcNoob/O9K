namespace O9K.AIO.Heroes.Dynamic.Abilities.Disables.Unique
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Extensions;

    using Ensage;

    [AbilityId(AbilityId.dark_willow_bramble_maze)]
    internal class BrambleMazeDisable : OldDisableAbility
    {
        public BrambleMazeDisable(IDisable ability)
            : base(ability)
        {
        }

        public override bool Use(Unit9 target)
        {
            var input = this.Ability.GetPredictionInput(target);
            var output = this.Ability.GetPredictionOutput(input);

            if (!target.IsMoving && this.Owner.Distance(target) < this.Ability.CastRange + 140)
            {
                if (!this.Ability.UseAbility(output.CastPosition.Extend2D(this.Owner.Position, 150)))
                {
                    return false;
                }

                this.OrbwalkSleeper.Sleep(this.Ability.Owner.Handle, this.Ability.GetCastDelay(target));
                this.AbilitySleeper.Sleep(this.Ability.Handle, 2);

                return true;
            }

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