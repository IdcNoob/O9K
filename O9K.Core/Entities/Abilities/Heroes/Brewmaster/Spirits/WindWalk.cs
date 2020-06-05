namespace O9K.Core.Entities.Abilities.Heroes.Brewmaster.Spirits
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.brewmaster_storm_wind_walk)]
    public class WindWalk : ActiveAbility, IBuff
    {
        public WindWalk(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_brewmaster_storm_wind_walk";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;

        public override bool IsInvisibility { get; } = true;
    }
}