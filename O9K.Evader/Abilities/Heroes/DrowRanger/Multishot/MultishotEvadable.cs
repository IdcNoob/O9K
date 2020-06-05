namespace O9K.Evader.Abilities.Heroes.DrowRanger.Multishot
{
    using Base.Evadable;
    using Base.Evadable.Components;
    using Base.Usable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Heroes.DrowRanger;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal sealed class MultishotEvadable : EvadableAbility, IModifierObstacle
    {
        public MultishotEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Multishot = (Multishot)ability;

            this.Disables.UnionWith(Abilities.Disable);
            this.Disables.UnionWith(Abilities.ChannelDisable);
            this.Disables.UnionWith(Abilities.Invulnerability);

            this.Counters.Add(Abilities.Bulwark);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.StrongPhysShield);
            this.Counters.Add(Abilities.BladeMail);
            this.Counters.UnionWith(Abilities.Heal);

            this.Counters.ExceptWith(Abilities.MagicImmunity);
            this.Counters.Remove(Abilities.HeavenlyGrace);
            this.Counters.Remove(Abilities.Bulldoze);
            this.Counters.Remove(Abilities.DarkPact);
            this.Counters.Remove(Abilities.ShadowDance);
            this.Counters.Remove(Abilities.Windrun);
        }

        public bool AllyModifierObstacle { get; } = false;

        public override bool CanBeDodged { get; } = false;

        public Multishot Multishot { get; }

        public void AddModifierObstacle(Modifier modifier, Unit sender)
        {
            var remainingTime = modifier.RemainingTime;
            if (remainingTime < this.Multishot.ChannelTime * 0.7f)
            {
                return;
            }

            var obstacle = new MultishotObstacle(this, this.Owner.Position, modifier)
            {
                EndCastTime = Game.RawGameTime,
                EndObstacleTime = Game.RawGameTime + remainingTime
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

        protected override void AddObstacle()
        {
        }
    }
}