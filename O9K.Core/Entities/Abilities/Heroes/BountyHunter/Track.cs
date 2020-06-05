namespace O9K.Core.Entities.Abilities.Heroes.BountyHunter
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.bounty_hunter_track)]
    public class Track : RangedAbility, IDebuff
    {
        public Track(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string DebuffModifierName { get; } = "modifier_bounty_hunter_track";
    }
}