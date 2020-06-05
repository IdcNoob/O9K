namespace O9K.Evader.Abilities.Units.AncientThunderhide.Slam
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.big_thunder_lizard_slam)]
    internal class SlamBase : EvaderBaseAbility, IEvadable
    {
        public SlamBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SlamEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}