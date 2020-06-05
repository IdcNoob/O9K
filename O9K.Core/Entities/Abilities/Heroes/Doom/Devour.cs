namespace O9K.Core.Entities.Abilities.Heroes.Doom
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.doom_bringer_devour)]
    public class Devour : RangedAbility
    {
        public Devour(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override bool TargetsEnemy { get; } = false;
    }
}