namespace O9K.Evader.Abilities.Heroes.CentaurWarrunner.HoofStomp
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.centaur_hoof_stomp)]
    internal class HoofStompBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public HoofStompBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new HoofStompEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}