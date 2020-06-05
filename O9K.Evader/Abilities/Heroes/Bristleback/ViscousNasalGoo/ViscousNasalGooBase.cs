namespace O9K.Evader.Abilities.Heroes.Bristleback.ViscousNasalGoo
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.bristleback_viscous_nasal_goo)]
    internal class ViscousNasalGooBase : EvaderBaseAbility, IEvadable
    {
        public ViscousNasalGooBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ViscousNasalGooEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}