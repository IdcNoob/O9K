namespace O9K.Evader.Abilities.Heroes.Clockwerk.BatteryAssault
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class BatteryAssaultEvadable : ModifierCounterEvadable
    {
        public BatteryAssaultEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.UnionWith(Abilities.Shield);
            this.ModifierCounters.UnionWith(Abilities.MagicShield);
            this.ModifierCounters.UnionWith(Abilities.SlowHeal);
            this.ModifierCounters.Add(Abilities.BladeMail);

            this.ModifierCounters.Remove(Abilities.Nightmare);
        }

        public override bool ModifierEnemyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, this.ActiveAbility.Radius);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}