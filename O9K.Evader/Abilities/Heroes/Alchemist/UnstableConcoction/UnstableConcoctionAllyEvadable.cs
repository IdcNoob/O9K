namespace O9K.Evader.Abilities.Heroes.Alchemist.UnstableConcoction
{
    using Base.Evadable;
    using Base.Evadable.Components;
    using Base.Usable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal sealed class UnstableConcoctionAllyEvadable : ModifierCounterEvadable, IModifierObstacle
    {
        public UnstableConcoctionAllyEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierCounters.Add(Abilities.MantaStyle);
        }

        public bool AllyModifierObstacle { get; } = true;

        public override bool ModifierAllyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new UnstableConcoctionAllyObstacle(this, this.Ability, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public void AddModifierObstacle(Modifier modifier, Unit sender)
        {
            var modifierOwner = EntityManager9.GetUnit(sender.Handle);
            if (modifierOwner == null)
            {
                return;
            }

            this.AddModifier(modifier, modifierOwner);
        }

        public override bool IgnoreRemainingTime(IObstacle obstacle, UsableAbility usableAbility)
        {
            return false;
        }
    }
}