namespace O9K.Core.Entities.Abilities.Heroes.Snapfire
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.snapfire_lil_shredder)]
    public class LilShredder : ActiveAbility, IBuff

    {
        public LilShredder(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_snapfire_lil_shredder_buff";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}