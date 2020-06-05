namespace O9K.Evader.Abilities.Units.OgreFrostmage.IceArmor
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.ogre_magi_frost_armor)]
    internal class IceArmorBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public IceArmorBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new IceArmorEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}