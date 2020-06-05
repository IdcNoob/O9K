namespace O9K.Farm.Utils
{
    using System;
    using System.Collections.Generic;

    using Ensage;

    using Units.Base;

    internal class ControlEffect : IDisposable
    {
        private readonly List<ParticleEffect> effects = new List<ParticleEffect>();

        public ControlEffect(FarmUnit unit)
        {
            //this.effects.Add(
            //    new ParticleEffect(
            //        "particles/econ/events/ti7/ti7_hero_effect_light_aura.vpcf",
            //        unit.Unit.BaseUnit,
            //        ParticleAttachment.AbsOriginFollow));

            //this.effects.Add(
            //    new ParticleEffect(
            //        "particles/econ/events/ti7/ti7_hero_effect_aegis_back.vpcf",
            //        unit.Unit.BaseUnit,
            //        ParticleAttachment.AbsOriginFollow));

            //this.effects.Add(
            //    new ParticleEffect(
            //        "particles/econ/events/ti7/ti7_hero_effect_aegis_top.vpcf",
            //        unit.Unit.BaseUnit,
            //        ParticleAttachment.AbsOriginFollow));

            //this.effects.Add(
            //    new ParticleEffect(
            //        "particles/econ/events/ti8/ti8_hero_effect_base_detail.vpcf",
            //        unit.Unit.BaseUnit,
            //        ParticleAttachment.AbsOriginFollow));
        }

        public void Dispose()
        {
            //foreach (var particleEffect in this.effects)
            //{
            //    particleEffect.Dispose();
            //}
        }
    }
}