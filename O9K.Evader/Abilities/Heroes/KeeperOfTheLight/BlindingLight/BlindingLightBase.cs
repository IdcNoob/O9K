namespace O9K.Evader.Abilities.Heroes.KeeperOfTheLight.BlindingLight
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.keeper_of_the_light_blinding_light)]
    internal class BlindingLightBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public BlindingLightBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BlindingLightEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new BlindingLightUsable(this.Ability, this.Menu);
        }
    }
}