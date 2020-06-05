namespace O9K.Evader.Abilities.Heroes.AncientApparition.ColdFeet
{
    using System.Collections.Generic;

    using Base.Evadable;
    using Base.Usable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal sealed class ColdFeetEvadable : ModifierCounterEvadable
    {
        private readonly HashSet<AbilityId> modifierCountersIgnoreTime = new HashSet<AbilityId>();

        public ColdFeetEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.UnionWith(Abilities.StrongShield);
            this.ModifierCounters.UnionWith(Abilities.MagicShield);

            this.modifierCountersIgnoreTime.UnionWith(Abilities.AllyPurge);
            this.modifierCountersIgnoreTime.Add(Abilities.MantaStyle);
        }

        public override bool ModifierAllyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            if (modifier.Name == "modifier_ancientapparition_coldfeet_freeze")
            {
                //todo add
                return;
            }

            var modifierObstacle = new ColdFeetEvadableModifier(this, modifier, modifierOwner, this.Ability.Duration);
            this.Pathfinder.AddObstacle(modifierObstacle);
        }

        public override bool IgnoreRemainingTime(IObstacle obstacle, UsableAbility usableAbility)
        {
            return this.modifierCountersIgnoreTime.Contains(usableAbility.Ability.Id);
        }
    }
}