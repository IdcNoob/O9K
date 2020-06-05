namespace O9K.Evader.Abilities.Heroes.VoidSpirit.AetherRemnant
{
    using Base;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.void_spirit_aether_remnant)]
    internal class AetherRemnantBase : EvaderBaseAbility, IUsable<DisableAbility>
    {
        public AetherRemnantBase(Ability9 ability)
            : base(ability)
        {
            //todo obstacle + modifier
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}