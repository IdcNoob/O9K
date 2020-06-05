namespace O9K.Core.Entities.Abilities.Heroes.DarkSeer
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.dark_seer_ion_shell)]
    public class IonShell : RangedAbility, IBuff
    {
        public IonShell(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string BuffModifierName { get; } = "modifier_dark_seer_ion_shell";

        public bool BuffsAlly { get; } = true;

        public bool BuffsOwner { get; } = true;
    }
}