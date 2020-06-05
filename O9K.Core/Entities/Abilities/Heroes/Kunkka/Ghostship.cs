namespace O9K.Core.Entities.Abilities.Heroes.Kunkka
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.kunkka_ghostship)]
    public class Ghostship : CircleAbility, IHasDamageAmplify, IHarass
    {
        private readonly SpecialData amplifierData;

        private readonly SpecialData ghostshipDistance;

        public Ghostship(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "tooltip_delay");
            //this.SpeedData = new SpecialData(baseAbility, "ghostship_speed");
            this.RadiusData = new SpecialData(baseAbility, "ghostship_width");
            this.ghostshipDistance = new SpecialData(baseAbility, "ghostship_distance");
            this.amplifierData = new SpecialData(baseAbility, "ghostship_absorb");
        }

        public override float ActivationDelay
        {
            get
            {
                var delay = base.ActivationDelay;

                if (this.Owner.HasAghanimsScepter)
                {
                    delay /= 2f;
                }

                return delay;
            }
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_kunkka_ghost_ship_damage_absorb" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public float GhostshipDistance
        {
            get
            {
                var distance = this.ghostshipDistance.GetValue(this.Level);
                if (this.Owner.HasAghanimsScepter)
                {
                    distance /= 2;
                }

                return distance;
            }
        }

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return this.amplifierData.GetValue(this.Level) / -100;
        }

        //public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
        //{
        //    var input = base.GetPredictionInput(target, aoeTargets);
        //    input.Delay += this.GhostshipDistance / this.Speed;
        //    input.Speed = 0;

        //    return input;
        //}
    }
}