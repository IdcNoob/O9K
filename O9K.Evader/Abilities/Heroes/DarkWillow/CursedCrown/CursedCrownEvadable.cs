namespace O9K.Evader.Abilities.Heroes.DarkWillow.CursedCrown
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

    internal sealed class CursedCrownEvadable : TargetableEvadable, IModifierCounter
    {
        private readonly HashSet<AbilityId> modifierCountersIgnoreTime = new HashSet<AbilityId>();

        public CursedCrownEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo evade modifier stun aoe

            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.LinkensSphere);

            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.Add(Abilities.AttributeShift);
            this.ModifierCounters.UnionWith(Abilities.StrongShield);
            this.ModifierCounters.UnionWith(Abilities.MagicShield);

            this.modifierCountersIgnoreTime.UnionWith(Abilities.AllyPurge);
            this.modifierCountersIgnoreTime.Add(Abilities.MantaStyle);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            if (modifier.Name == "modifier_stunned")
            {
                //todo add
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