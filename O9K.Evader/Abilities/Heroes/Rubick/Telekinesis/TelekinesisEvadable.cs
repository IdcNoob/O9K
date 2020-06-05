namespace O9K.Evader.Abilities.Heroes.Rubick.Telekinesis
{
    using System.Collections.Generic;
    using System.Linq;

    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class TelekinesisEvadable : TargetableEvadable, IModifierCounter
    {
        private readonly HashSet<AbilityId> landBlinks = new HashSet<AbilityId>();

        private readonly HashSet<AbilityId> landCounters = new HashSet<AbilityId>();

        public TelekinesisEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.LinkensSphere);
            this.Counters.Add(Abilities.DarkPact);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.LotusOrb);
            this.Counters.UnionWith(Abilities.MagicShield);

            this.landBlinks.UnionWith(Abilities.Blink);

            this.landCounters.Add(Abilities.SleightOfFist);
            this.landCounters.Add(Abilities.BallLightning);
            this.landCounters.Add(Abilities.MantaStyle);
            this.landCounters.Add(Abilities.AttributeShift);
            this.landCounters.UnionWith(Abilities.StrongShield);
            this.landCounters.UnionWith(Abilities.Invulnerability);
            this.landCounters.UnionWith(Abilities.MagicShield);
            this.landCounters.Add(Abilities.Armlet);
            this.landCounters.UnionWith(Abilities.SlowHeal);
            this.landCounters.Add(Abilities.BladeMail);

            this.ModifierCounters.UnionWith(Abilities.AllyStrongPurge);
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.UnionWith(Abilities.MagicShield);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        protected override IEnumerable<AbilityId> AllModifierBlinks
        {
            get
            {
                return this.ModifierBlinks.Concat(this.landBlinks);
            }
        }

        protected override IEnumerable<AbilityId> AllModifierCounters
        {
            get
            {
                return this.ModifierCounters.Concat(this.landCounters);
            }
        }

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            if (modifier.Name == "modifier_rubick_telekinesis")
            {
                var obstacle = new AreaOfEffectModifierObstacle(this, modifierOwner.Position, modifier)
                {
                    Counters = this.landCounters.ToArray(),
                    Blinks = this.landBlinks.ToArray()
                };

                this.Pathfinder.AddObstacle(obstacle);
            }
            else
            {
                var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
                this.Pathfinder.AddObstacle(obstacle);
            }
        }
    }
}