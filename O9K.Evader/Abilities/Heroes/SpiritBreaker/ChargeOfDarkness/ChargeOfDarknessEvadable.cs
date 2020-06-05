namespace O9K.Evader.Abilities.Heroes.SpiritBreaker.ChargeOfDarkness
{
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;

    using Ensage;

    using Metadata;

    internal sealed class ChargeOfDarknessEvadable : EvadableAbility, IModifierObstacle
    {
        public ChargeOfDarknessEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.SimpleStun);
            this.Disables.UnionWith(Abilities.Root);
            this.Disables.UnionWith(Abilities.Invulnerability);
            this.Disables.UnionWith(Abilities.PhysDisable);

            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.HurricanePike);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.StrongPhysShield);
            this.Counters.UnionWith(Abilities.Invisibility);
            this.Counters.Add(Abilities.BladeMail);
            this.Counters.Add(Abilities.LotusOrb);
        }

        public bool AllyModifierObstacle { get; } = true;

        public void AddModifierObstacle(Modifier modifier, Unit sender)
        {
            var obstacle = new ChargeOfDarknessObstacle(this, modifier, sender);
            this.Pathfinder.AddObstacle(obstacle);
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