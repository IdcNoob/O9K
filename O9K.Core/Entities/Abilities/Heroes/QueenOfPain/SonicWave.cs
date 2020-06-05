namespace O9K.Core.Entities.Abilities.Heroes.QueenOfPain
{
    using System;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Extensions;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    using SharpDX;

    [AbilityId(AbilityId.queenofpain_sonic_wave)]
    public class SonicWave : ConeAbility, INuke
    {
        private readonly SpecialData scepterDamageData;

        public SonicWave(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "starting_aoe");
            this.EndRadiusData = new SpecialData(baseAbility, "final_aoe");
            this.RangeData = new SpecialData(baseAbility, "distance");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.scepterDamageData = new SpecialData(baseAbility, "damage_scepter");
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            if (this.Owner.HasAghanimsScepter)
            {
                return new Damage
                {
                    [this.DamageType] = this.scepterDamageData.GetValue(this.Level)
                };
            }

            return base.GetRawDamage(unit, remainingHealth);
        }

        public override bool UseAbility(Vector3 position, bool queue = false, bool bypass = false)
        {
            //todo fix prediction range and delete 

            position = this.Owner.Position.Extend2D(position, Math.Min(this.CastRange, this.Owner.Distance(position)));

            var result = this.BaseAbility.UseAbility(position, queue, bypass);
            if (result)
            {
                this.ActionSleeper.Sleep(0.1f);
            }

            return result;
        }
    }
}