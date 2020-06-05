namespace O9K.Core.Entities.Abilities.Heroes.PhantomLancer
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.phantom_lancer_spirit_lance)]
    public class SpiritLance : RangedAbility, INuke
    {
        public SpiritLance(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "lance_damage");
            this.SpeedData = new SpecialData(baseAbility, "lance_speed");
        }
    }
}