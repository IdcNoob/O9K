namespace O9K.Evader.Abilities.Heroes.Snapfire.MortimerKisses
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class MortimerKissesEvadable : ModifierCounterEvadable
    {
        public MortimerKissesEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierDisables.UnionWith(Abilities.Disable);

            this.ModifierCounters.UnionWith(Abilities.StrongMagicShield);
            this.ModifierCounters.UnionWith(Abilities.MagicShield);
            this.ModifierCounters.UnionWith(Abilities.SlowHeal);
            this.ModifierCounters.Add(Abilities.BladeMail);

            this.ModifierCounters.Remove(Abilities.Bristleback);
            this.ModifierCounters.Remove(Abilities.FatesEdict);
        }

        public override bool ModifierAllyCounter { get; } = true;

        public override bool ModifierEnemyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            if (modifier.IsDebuff)
            {
                var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner)
                {
                    Disables = new AbilityId[0]
                };

                this.Pathfinder.AddObstacle(obstacle);
            }
            else
            {
                var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, 1000)
                {
                    Counters = new AbilityId[0]
                };

                this.Pathfinder.AddObstacle(obstacle);
            }
        }
    }
}