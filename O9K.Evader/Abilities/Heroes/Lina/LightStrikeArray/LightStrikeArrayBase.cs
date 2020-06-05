namespace O9K.Evader.Abilities.Heroes.Lina.LightStrikeArray
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.lina_light_strike_array)]
    internal class LightStrikeArrayBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public LightStrikeArrayBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new LightStrikeArrayEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}