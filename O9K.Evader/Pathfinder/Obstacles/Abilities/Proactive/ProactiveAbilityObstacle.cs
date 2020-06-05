namespace O9K.Evader.Pathfinder.Obstacles.Abilities.Proactive
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Units;

    using Ensage;

    using O9K.Evader.Abilities.Base.Evadable;
    using O9K.Evader.Abilities.Base.Evadable.Components;

    using SharpDX;

    internal class ProactiveAbilityObstacle : IObstacle
    {
        private readonly Dictionary<Unit9, int> damages = new Dictionary<Unit9, int>();

        public ProactiveAbilityObstacle(IProactiveCounter ability)
        {
            this.EvadableAbility = (EvadableAbility)ability;
            this.Caster = this.EvadableAbility.Owner;
            this.Blinks = ability.ProactiveBlinks.ToArray();
            this.Counters = ability.ProactiveCounters.ToArray();
            this.Disables = ability.ProactiveDisables.ToArray();
        }

        public AbilityId[] Blinks { get; set; }

        public bool CanBeDodged { get; } = false;

        public Unit9 Caster { get; }

        public AbilityId[] Counters { get; set; }

        public AbilityId[] Disables { get; set; }

        public EvadableAbility EvadableAbility { get; }

        public uint Id { get; set; }

        public bool IsModifierObstacle { get; } = false;

        public bool IsProactiveObstacle { get; } = true;

        public Vector3 Position { get; } = Vector3.Zero;

        public virtual IEnumerable<AbilityId> GetBlinks(Unit9 ally)
        {
            return this.Blinks;
        }

        public virtual IEnumerable<AbilityId> GetCounters(Unit9 ally)
        {
            return this.Counters;
        }

        public int GetDamage(Unit9 ally)
        {
            if (!this.damages.TryGetValue(ally, out var damage))
            {
                damage = this.EvadableAbility.Ability.GetDamage(ally);
                this.damages[ally] = damage;
            }

            return damage;
        }

        public virtual IEnumerable<AbilityId> GetDisables(Unit9 ally)
        {
            return this.Disables;
        }

        public float GetDisableTime(Unit9 enemy)
        {
            //return enemy.GetTurnTime(MathUtil.DegreesToRadians(enemy.BaseUnit.RotationDifference));
            return 0.15f;
        }

        public float GetEvadeTime(Unit9 ally, bool blink)
        {
            return 0.15f;
        }

        public virtual bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            if (!unit.IsMyHero)
            {
                return false;
            }

            var ability = this.EvadableAbility.Ability;

            if (this.Caster.Distance(unit) > ability.Range + 100)
            {
                return false;
            }

            if (!ability.CanBeCasted())
            {
                return false;
            }

            if (unit.IsMagicImmune && !ability.PiercesMagicImmunity(unit))
            {
                return false;
            }

            return this.Caster.GetAngle(unit, true) < 0.25f;
        }
    }
}