namespace O9K.Evader.Abilities.Heroes.AncientApparition.IceBlast
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.ancient_apparition_ice_blast)]
    internal class IceBlastBase : EvaderBaseAbility, IEvadable
    {
        public IceBlastBase(Ability9 ability)
            : base(ability)
        {
            //todo dmg calc
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new IceBlastEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}