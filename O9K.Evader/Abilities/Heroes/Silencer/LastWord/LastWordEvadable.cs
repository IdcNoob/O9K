namespace O9K.Evader.Abilities.Heroes.Silencer.LastWord
{
    using System.Collections.Generic;

    using Base;
    using Base.Evadable;
    using Base.Usable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class LastWordEvadable : TargetableEvadable, IModifierCounter
    {
        private readonly HashSet<AbilityId> modifierCountersIgnoreTime = new HashSet<AbilityId>();

        public LastWordEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Counterspell);

            this.ModifierCounters.Add(Abilities.Mischief);
            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.Add(Abilities.AttributeShift);
            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.UnionWith(Abilities.Shield);
            this.ModifierCounters.Add(Abilities.EulsScepterOfDivinity);
            this.ModifierCounters.UnionWith(Abilities.MagicShield);

            this.modifierCountersIgnoreTime.UnionWith(Abilities.AllyPurge);
            this.modifierCountersIgnoreTime.Add(Abilities.MantaStyle);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            if (modifier.Name == "modifier_silence")
            {
                //todo add silence modifier
                return;
            }

            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public override bool IgnoreRemainingTime(IObstacle obstacle, UsableAbility usableAbility)
        {
            return this.modifierCountersIgnoreTime.Contains(usableAbility.Ability.Id);
        }
    }
}