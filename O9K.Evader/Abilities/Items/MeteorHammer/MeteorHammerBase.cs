namespace O9K.Evader.Abilities.Items.MeteorHammer
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_meteor_hammer)]
    internal class MeteorHammerBase : EvaderBaseAbility, IEvadable
    {
        public MeteorHammerBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new MeteorHammerEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}