namespace O9K.Evader.Abilities.Heroes.SkywrathMage.MysticFlare
{
    using Base.Evadable;
    using Base.Evadable.Components;
    using Base.Usable;

    using Core.Entities.Abilities.Base;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal sealed class MysticFlareEvadable : AreaOfEffectEvadable, IModifierObstacle
    {
        public MysticFlareEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.Add(Abilities.ForceBoots);
            this.Blinks.Add(Abilities.ForceStaff);
            this.Blinks.Add(Abilities.HurricanePike);

            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.HurricanePike);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.Spoink);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.Add(Abilities.BladeMail);

            this.Counters.Remove(Abilities.DarkPact);
            this.Counters.Remove(Abilities.ShadowDance);
            this.Counters.Remove(Abilities.ShadowRealm);
        }

        public bool AllyModifierObstacle { get; } = false;

        public override bool CanBeDodged { get; } = false;

        public void AddModifierObstacle(Modifier modifier, Unit sender)
        {
            var obstacle = new MysticFlareObstacle(this, sender.Position, modifier);
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