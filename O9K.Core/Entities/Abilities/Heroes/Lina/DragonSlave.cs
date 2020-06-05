namespace O9K.Core.Entities.Abilities.Heroes.Lina
{
    using System.Collections.Generic;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    using Prediction.Data;

    [AbilityId(AbilityId.lina_dragon_slave)]
    public class DragonSlave : LineAbility, INuke
    {
        public DragonSlave(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "dragon_slave_width_initial");
            this.SpeedData = new SpecialData(baseAbility, "dragon_slave_speed");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                // disable casting on target
                return base.AbilityBehavior & ~AbilityBehavior.UnitTarget;
            }
        }

        public override float Speed
        {
            get
            {
                return base.Speed * 1.2f;
            }
        }

        public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
        {
            var input = base.GetPredictionInput(target, aoeTargets);
            input.Range -= 200;
            input.CastRange -= 200;

            return input;
        }
    }
}