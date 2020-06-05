namespace O9K.Evader.Abilities.Heroes.Grimstroke.PhantomsEmbrace
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.grimstroke_ink_creature)]
    internal class PhantomsEmbraceBase : EvaderBaseAbility, IEvadable
    {
        public PhantomsEmbraceBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PhantomsEmbraceEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}