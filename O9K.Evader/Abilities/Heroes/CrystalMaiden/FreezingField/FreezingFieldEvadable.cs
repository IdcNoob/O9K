namespace O9K.Evader.Abilities.Heroes.CrystalMaiden.FreezingField
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class FreezingFieldEvadable : ModifierCounterEvadable
    {
        public FreezingFieldEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierDisables.UnionWith(Abilities.Disable);
            this.ModifierDisables.UnionWith(Abilities.ChannelDisable);
            this.ModifierDisables.UnionWith(Abilities.Invulnerability);
            this.ModifierDisables.Add(Abilities.NetherSwap);
            this.ModifierDisables.UnionWith(Abilities.StrongDisable);
            this.ModifierDisables.Add(Abilities.ReapersScythe);

            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.UnionWith(Abilities.StrongMagicShield);
            this.ModifierCounters.UnionWith(Abilities.Shield);
            this.ModifierCounters.Add(Abilities.BladeMail);
        }

        public override bool ModifierAllyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}