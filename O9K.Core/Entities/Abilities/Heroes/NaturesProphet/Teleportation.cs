namespace O9K.Core.Entities.Abilities.Heroes.NaturesProphet
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.furion_teleportation)]
    public class Teleportation : RangedAbility
    {
        public Teleportation(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override float CastRange { get; } = 9999999;
    }
}