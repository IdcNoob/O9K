namespace O9K.Core.Entities.Abilities.Units.Necronomicon.Archer
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.necronomicon_archer_purge)]
    public class Purge : RangedAbility, IDebuff
    {
        public Purge(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string DebuffModifierName { get; } = "modifier_necronomicon_archer_purge";
    }
}