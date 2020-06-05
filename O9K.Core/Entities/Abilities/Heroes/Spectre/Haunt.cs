namespace O9K.Core.Entities.Abilities.Heroes.Spectre
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.spectre_haunt)]
    public class Haunt : AreaOfEffectAbility
    {
        public Haunt(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override float Radius { get; } = 9999999;
    }
}