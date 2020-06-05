namespace O9K.Evader.Abilities.Heroes.Pudge.Rot
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.pudge_rot)]
    internal class RotBase : EvaderBaseAbility, IEvadable
    {
        public RotBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new RotEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}