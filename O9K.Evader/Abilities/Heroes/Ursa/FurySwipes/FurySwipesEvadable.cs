namespace O9K.Evader.Abilities.Heroes.Ursa.FurySwipes
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class FurySwipesEvadable : ModifierCounterEvadable
    {
        public FurySwipesEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierDisables.UnionWith(Abilities.EnemyStrongPurge);
            this.ModifierDisables.UnionWith(Abilities.PhysDisable);

            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
            this.ModifierCounters.Add(Abilities.BladeMail);
        }

        public override bool ModifierAllyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierStackAllyObstacle(this, modifier, modifierOwner, 4);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}