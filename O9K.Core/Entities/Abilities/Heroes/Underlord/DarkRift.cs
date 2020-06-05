namespace O9K.Core.Entities.Abilities.Heroes.Underlord
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.abyssal_underlord_dark_rift)]
    public class DarkRift : RangedAbility
    {
        public DarkRift(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override float CastRange { get; } = 9999999;
    }
}