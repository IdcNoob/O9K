namespace O9K.Core.Entities.Abilities.Heroes.Spectre
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.spectre_reality)]
    public class Reality : RangedAbility
    {
        public Reality(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override bool CanHitSpellImmuneEnemy { get; } = true;

        public override float CastRange { get; } = 9999999;
    }
}