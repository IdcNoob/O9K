namespace O9K.Evader.Abilities.Heroes.Huskar.InnerFire
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class InnerFireEvadable : AreaOfEffectEvadable, IModifierCounter
    {
        public InnerFireEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.PhaseShift);
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.MantaStyle);

            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.Add(Abilities.PressTheAttack);
            this.ModifierCounters.Add(Abilities.Enrage);
            this.ModifierCounters.Add(Abilities.ChemicalRage);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}