namespace O9K.Evader.Abilities.Heroes.EmberSpirit.ActivateFireRemnant
{
    using Base;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.ember_spirit_activate_fire_remnant)]
    internal class ActivateFireRemnantBase : EvaderBaseAbility, IUsable<BlinkAbility>
    {
        public ActivateFireRemnantBase(Ability9 ability)
            : base(ability)
        {
        }

        public BlinkAbility GetUsableAbility()
        {
            return new ActivateFireRemnantUsable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}