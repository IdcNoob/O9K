namespace O9K.Core.Entities.Abilities.Heroes.OgreMagi
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.ogre_magi_bloodlust)]
    public class Bloodlust : RangedAbility, IBuff
    {
        public Bloodlust(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_ogre_magi_bloodlust";

        public bool BuffsAlly { get; } = true;

        public bool BuffsOwner { get; } = true;
    }
}