namespace O9K.Evader.Abilities.Heroes.QueenOfPain.Blink
{
    using Base;
    using Base.Evadable;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.queenofpain_blink)]
    internal class BlinkBase : EvaderBaseAbility, IEvadable, IUsable<BlinkAbility>
    {
        public BlinkBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BlinkEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public BlinkAbility GetUsableAbility()
        {
            return new BlinkAbility(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}