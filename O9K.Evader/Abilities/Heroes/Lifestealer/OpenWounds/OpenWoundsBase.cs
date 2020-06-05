namespace O9K.Evader.Abilities.Heroes.Lifestealer.OpenWounds
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.life_stealer_open_wounds)]
    internal class OpenWoundsBase : EvaderBaseAbility, IEvadable
    {
        public OpenWoundsBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new OpenWoundsEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}