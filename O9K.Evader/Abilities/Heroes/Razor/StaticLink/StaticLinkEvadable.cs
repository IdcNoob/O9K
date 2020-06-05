namespace O9K.Evader.Abilities.Heroes.Razor.StaticLink
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class StaticLinkEvadable : TargetableEvadable //,IModifierCounter
    {
        public StaticLinkEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo modifier blink ?
            //todo 2 obstacles on start and on 100 stacks

            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.LotusOrb);

            //ModifierDisables.UnionWith(Abilities.PhysDisable);
            //ModifierDisables.UnionWith(Abilities.Disable);

            //ModifierCounters.Add(Abilities.HurricanePike);
            //ModifierCounters.UnionWith(Abilities.StrongPhysShield);
        }

        //public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        //{
        //    var obstacle = new ModifierEnemyStackObstacle(this, modifier, modifierOwner, modifierOwner.AttackRange + 100, 100);
        //    Pathfinder.AddObstacle(obstacle);
        //}

        //

        // 

        //public bool ModifierEnemyCounter { get; } = true;

        //public bool ModifierAllyCounter { get; } = false;
    }
}