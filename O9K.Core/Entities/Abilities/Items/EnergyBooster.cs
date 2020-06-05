namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_energy_booster)]
    public class EnergyBooster : PassiveAbility
    {
        public EnergyBooster(Ability ability)
            : base(ability)
        {
        }
    }
}