namespace O9K.Evader.Abilities.Heroes.FacelessVoid.Chronosphere
{
    using Base;
    using Base.Evadable;
    using Base.Evadable.Components;
    using Base.Usable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class ChronosphereEvadable : LinearAreaOfEffectEvadable, IModifierCounter, IModifierObstacle
    {
        public ChronosphereEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.Add(Abilities.GlobalSilence);
            this.Disables.UnionWith(Abilities.Disable);
            this.Disables.UnionWith(Abilities.Invulnerability);
            this.Disables.UnionWith(Abilities.StrongDisable);
            this.Disables.UnionWith(Abilities.PhysDisable);

            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.Add(Abilities.Supernova);
            this.Counters.Add(Abilities.HurricanePike);
            this.Counters.UnionWith(Abilities.StrongPhysShield);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.Add(Abilities.BladeMail);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.Spoink);
            this.Counters.Add(Abilities.Bulwark);

            this.Counters.Remove(Abilities.DarkPact);

            this.ModifierDisables.UnionWith(Abilities.Stun);
            this.ModifierDisables.UnionWith(Abilities.Invulnerability);
            this.ModifierDisables.UnionWith(Abilities.PhysDisable);

            this.ModifierCounters.Add(Abilities.SongOfTheSiren);
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.Add(Abilities.InkSwell);
            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
        }

        public bool AllyModifierObstacle { get; } = false;

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner)
            {
                IgnoreModifierRemainingTime = true
            };

            this.Pathfinder.AddObstacle(obstacle);
        }

        public void AddModifierObstacle(Modifier modifier, Unit sender)
        {
            var obstacle = new ChronosphereObstacle(this, sender.Position, modifier);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public override bool IgnoreRemainingTime(IObstacle obstacle, UsableAbility usableAbility)
        {
            return obstacle.IsModifierObstacle || obstacle is ChronosphereObstacle;
        }
    }
}