namespace O9K.Core.Entities.Abilities.Heroes.Lycan
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.lycan_wolf_bite)]
    public class WolfBite : RangedAbility, IBuff
    {
        public WolfBite(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_lycan_shapeshift";

        public bool BuffsAlly { get; } = true;

        public bool BuffsOwner { get; } = false;
    }
}