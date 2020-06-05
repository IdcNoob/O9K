namespace O9K.Evader.Abilities.Heroes.Dazzle.ShadowWave
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class ShadowWaveEvadable : LinearAreaOfEffectEvadable
    {
        public ShadowWaveEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo create obstacle only near units ?

            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.Bulwark);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.PhysShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
        }
    }
}