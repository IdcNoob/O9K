namespace O9K.Evader.Abilities.Heroes.SkywrathMage.MysticFlare
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.skywrath_mage_mystic_flare)]
    internal class MysticFlareBase : EvaderBaseAbility, IEvadable
    {
        public MysticFlareBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new MysticFlareEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}