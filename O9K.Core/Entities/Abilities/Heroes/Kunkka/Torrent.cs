namespace O9K.Core.Entities.Abilities.Heroes.Kunkka
{
    using System.Collections.Generic;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    using Prediction.Data;

    using SharpDX;

    [AbilityId(AbilityId.kunkka_torrent)]
    public class Torrent : CircleAbility, IDisable
    {
        public Torrent(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "delay");
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "torrent_damage");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float GetCastDelay(Unit9 unit)
        {
            return this.GetCastDelay();
        }

        public override float GetCastDelay(Vector3 position)
        {
            return this.GetCastDelay();
        }

        public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
        {
            var input = new PredictionInput9
            {
                Caster = this.Owner,
                Target = target,
                CollisionTypes = this.CollisionTypes,
                Delay = this.CastPoint + this.ActivationDelay + InputLag,
                Speed = this.Speed,
                CastRange = this.CastRange,
                Range = this.Range,
                Radius = this.Radius,
                SkillShotType = this.SkillShotType,
                RequiresToTurn = false
            };

            if (aoeTargets != null)
            {
                input.AreaOfEffect = this.HasAreaOfEffect;
                input.AreaOfEffectTargets = aoeTargets;
            }

            return input;
        }
    }
}