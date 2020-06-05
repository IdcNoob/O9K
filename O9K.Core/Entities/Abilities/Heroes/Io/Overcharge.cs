namespace O9K.Core.Entities.Abilities.Heroes.Io
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.wisp_overcharge)]
    public class Overcharge : ActiveAbility
    {
        public Overcharge(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override bool TargetsEnemy { get; } = false;
    }
}