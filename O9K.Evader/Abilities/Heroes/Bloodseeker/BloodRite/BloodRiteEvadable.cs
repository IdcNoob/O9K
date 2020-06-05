namespace O9K.Evader.Abilities.Heroes.Bloodseeker.BloodRite
{
    using Base;
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class BloodRiteEvadable : AreaOfEffectEvadable, IModifierObstacle, IModifierCounter
    {
        public BloodRiteEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.BladeMail);

            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.Add(Abilities.MantaStyle);
        }

        public bool AllyModifierObstacle { get; } = false;

        public override bool CanBeDodged { get; } = false;

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public void AddModifierObstacle(Modifier modifier, Unit sender)
        {
            var obstacle = new AreaOfEffectModifierObstacle(this, sender.Position, modifier);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public override void PhaseCancel()
        {
        }

        public override void PhaseStart()
        {
        }
    }
}