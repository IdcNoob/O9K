namespace O9K.Evader.Abilities.Heroes.Venomancer.VenomousGale
{
    using Base;
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.LinearProjectile;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class VenomousGaleEvadable : LinearProjectileEvadable, IModifierCounter, IParticle
    {
        public VenomousGaleEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.PhaseShift);
            this.Counters.Add(Abilities.Doppelganger);
            this.Counters.Add(Abilities.DarkPact);
            this.Counters.Add(Abilities.Spoink);

            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.Add(Abilities.PressTheAttack);
            this.ModifierCounters.UnionWith(Abilities.MagicShield);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public void AddParticle(ParticleEffect particle, string name)
        {
            if (!this.Owner.IsVisible)
            {
                return;
            }

            var time = Game.RawGameTime - (Game.Ping / 2000);

            var obstacle = new LinearProjectileObstacle(this, this.Ability.Owner.Position)
            {
                EndCastTime = time,
                EndObstacleTime = time + this.Ability.ActivationDelay + (this.RangedAbility.Range / this.RangedAbility.Speed)
            };

            this.Pathfinder.AddObstacle(obstacle);
        }

        protected override void AddObstacle()
        {
        }
    }
}