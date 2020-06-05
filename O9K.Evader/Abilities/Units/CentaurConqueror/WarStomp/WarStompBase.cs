namespace O9K.Evader.Abilities.Units.CentaurConqueror.WarStomp
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.centaur_khan_war_stomp)]
    internal class WarStompBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public WarStompBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new WarStompEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}