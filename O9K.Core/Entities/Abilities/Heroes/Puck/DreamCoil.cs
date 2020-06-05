namespace O9K.Core.Entities.Abilities.Heroes.Puck
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.puck_dream_coil)]
    public class DreamCoil : CircleAbility, IDisable
    {
        public DreamCoil(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "coil_radius");
            this.DamageData = new SpecialData(baseAbility, "coil_initial_damage");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override bool CanHitSpellImmuneEnemy
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return true;
                }

                return false;
            }
        }
    }
}