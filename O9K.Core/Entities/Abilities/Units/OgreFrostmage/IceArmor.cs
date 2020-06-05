namespace O9K.Core.Entities.Abilities.Units.OgreFrostmage
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.ogre_magi_frost_armor)]
    public class IceArmor : RangedAbility, IBuff
    {
        public IceArmor(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_ogre_magi_frost_armor";

        public bool BuffsAlly { get; } = true;

        public bool BuffsOwner { get; } = true;
    }
}