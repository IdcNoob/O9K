namespace O9K.Evader.Abilities.Heroes.WitchDoctor.DeathWard
{
    using Base.Evadable;
    using Base.Evadable.Components;
    using Base.Usable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Heroes.WitchDoctor;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal sealed class DeathWardEvadable : AreaOfEffectEvadable, IModifierObstacle
    {
        private readonly DeathWard deathWard;

        public DeathWardEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.deathWard = (DeathWard)ability;

            this.Disables.UnionWith(Abilities.Disable);
            this.Disables.UnionWith(Abilities.ChannelDisable);
            this.Disables.UnionWith(Abilities.Invulnerability);
            this.Disables.Add(Abilities.NetherSwap);
            this.Disables.UnionWith(Abilities.StrongDisable);
            this.Disables.Add(Abilities.ReapersScythe);

            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongPhysShield);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.Add(Abilities.Supernova);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.Add(Abilities.Armlet);

            this.Counters.ExceptWith(Abilities.MagicImmunity);
            this.Counters.Remove(Abilities.DarkPact);
            this.Counters.Remove(Abilities.StaticLink);
            this.Counters.Remove(Abilities.WhirlingAxes);
            this.Counters.Remove(Abilities.SpikedCarapace);
            this.Counters.Remove(Abilities.Bristleback);
        }

        public bool AllyModifierObstacle { get; } = false;

        public override bool CanBeDodged { get; } = false;

        public void AddModifierObstacle(Modifier modifier, Unit sender)
        {
            var time = Game.RawGameTime - (Game.Ping / 2000f);
            var obstacle = new DeathWardObstacle(this, sender.Position, modifier)
            {
                EndCastTime = time + this.deathWard.ChannelTime,
                EndObstacleTime = time + this.deathWard.ChannelTime
            };

            this.Pathfinder.AddObstacle(obstacle);
        }

        public override bool IgnoreRemainingTime(IObstacle obstacle, UsableAbility usableAbility)
        {
            return true;
        }

        public override void PhaseCancel()
        {
        }

        public override void PhaseStart()
        {
        }
    }
}