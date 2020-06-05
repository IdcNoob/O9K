namespace O9K.Evader.Abilities.Heroes.Warlock.ShadowWord
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class ShadowWordEvadable : TargetableEvadable, IModifierCounter
    {
        public ShadowWordEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Counterspell);

            this.ModifierCounters.UnionWith(Abilities.AllyPurge);

            this.ModifierDisables.UnionWith(Abilities.EnemyPurge);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = true;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            if (modifier.IsDebuff)
            {
                if (!modifierOwner.IsAlly())
                {
                    return;
                }

                var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner)
                {
                    Disables = new AbilityId[0]
                };

                this.Pathfinder.AddObstacle(obstacle);
            }
            else
            {
                if (modifierOwner.IsAlly())
                {
                    return;
                }

                var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, 800)
                {
                    Counters = new AbilityId[0]
                };

                this.Pathfinder.AddObstacle(obstacle);
            }
        }
    }
}