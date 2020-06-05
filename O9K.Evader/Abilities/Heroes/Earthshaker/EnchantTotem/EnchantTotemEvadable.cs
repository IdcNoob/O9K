namespace O9K.Evader.Abilities.Heroes.Earthshaker.EnchantTotem
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class EnchantTotemEvadable : AreaOfEffectEvadable, IModifierCounter
    {
        public EnchantTotemEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.Add(Abilities.Spoink);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.HurricanePike);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.Add(Abilities.Armlet);

            this.ModifierDisables.UnionWith(Abilities.EnemyPurge);
        }

        public bool ModifierAllyCounter { get; } = false;

        public bool ModifierEnemyCounter { get; } = true;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, this.Owner.GetAttackRange() + 100);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}