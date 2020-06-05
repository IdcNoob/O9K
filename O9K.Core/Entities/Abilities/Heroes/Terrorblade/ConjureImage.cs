namespace O9K.Core.Entities.Abilities.Heroes.Terrorblade
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.terrorblade_conjure_image)]
    public class ConjureImage : ActiveAbility, IBuff
    {
        public ConjureImage(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = string.Empty;

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}