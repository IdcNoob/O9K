namespace O9K.Evader.Abilities.Heroes.Terrorblade.TerrorWave
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.terrorblade_metamorphosis)]
    internal class MetamorphosisBase : EvaderBaseAbility, IEvadable
    {
        public MetamorphosisBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new MetamorphosisEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}