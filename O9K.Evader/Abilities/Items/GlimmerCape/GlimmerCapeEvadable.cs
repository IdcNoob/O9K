namespace O9K.Evader.Abilities.Items.GlimmerCape
{
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class GlimmerCapeEvadable : ModifierCounterEvadable, IModifierObstacle
    {
        public GlimmerCapeEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierDisables.UnionWith(Abilities.EnemyPurge);
        }

        public bool AllyModifierObstacle { get; } = false;

        public override bool ModifierEnemyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, 600);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public void AddModifierObstacle(Modifier modifier, Unit sender)
        {
            var modifierOwner = EntityManager9.GetUnit(sender.Handle);
            if (modifierOwner?.IsAlly(this.Owner) != true)
            {
                return;
            }

            this.AddModifier(modifier, modifierOwner);
        }
    }
}