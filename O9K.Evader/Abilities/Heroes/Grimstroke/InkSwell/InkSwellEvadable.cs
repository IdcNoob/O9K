namespace O9K.Evader.Abilities.Heroes.Grimstroke.InkSwell
{
    using System.Linq;

    using Base.Evadable;
    using Base.Usable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class InkSwellEvadable : ModifierCounterEvadable
    {
        public InkSwellEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.Add(Abilities.HurricanePike);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.Add(Abilities.BladeMail);

            this.ModifierCounters.UnionWith(Abilities.AllyStrongPurge);
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.UnionWith(Abilities.StrongMagicShield);
        }

        public override bool ModifierAllyCounter { get; } = true;

        public override bool ModifierEnemyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            if (modifierOwner.IsAlly(this.Owner))
            {
                if (modifier.Name == "modifier_grimstroke_spirit_walk_buff")
                {
                    var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, this.ActiveAbility.Radius)
                    {
                        Counters = this.Counters.ToArray(),
                        Blinks = this.Blinks.ToArray()
                    };

                    this.Pathfinder.AddObstacle(obstacle);
                }
            }
            else
            {
                if (modifier.Name == "modifier_stunned")
                {
                    var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
                    this.Pathfinder.AddObstacle(obstacle);
                }
            }
        }

        public override bool IgnoreRemainingTime(IObstacle obstacle, UsableAbility usableAbility)
        {
            return obstacle is ModifierAllyObstacle;
        }
    }
}