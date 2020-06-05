namespace O9K.Evader.Pathfinder.Obstacles.Modifiers
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Units;

    using Ensage;

    using O9K.Evader.Abilities.Base;
    using O9K.Evader.Abilities.Base.Evadable;

    using SharpDX;

    using Types;

    internal abstract class ModifierObstacle : IObstacle, IExpirable
    {
        protected readonly Modifier Modifier;

        private readonly Dictionary<Unit9, int> damages = new Dictionary<Unit9, int>();

        protected ModifierObstacle(IModifierCounter ability, Modifier modifier, Unit9 modifierOwner)
        {
            this.EvadableAbility = (EvadableAbility)ability;
            this.Modifier = modifier;
            this.ModifierOwner = modifierOwner;
            this.Counters = ability.ModifierCounters.ToArray();
            this.Disables = ability.ModifierDisables.ToArray();
            this.Blinks = ability.ModifierBlinks.ToArray();
            this.IgnoreModifierRemainingTime = modifier.Duration < 0;
            this.Delay = Game.Ping / 2000f;
        }

        public AbilityId[] Blinks { get; set; }

        public bool CanBeDodged { get; } = false;

        public Unit9 Caster { get; protected set; }

        public AbilityId[] Counters { get; set; }

        public AbilityId[] Disables { get; set; }

        public EvadableAbility EvadableAbility { get; }

        public uint Id { get; set; }

        public bool IgnoreModifierRemainingTime { get; set; }

        public bool IsExpired
        {
            get
            {
                return !this.Modifier.IsValid;
            }
        }

        public bool IsModifierObstacle { get; } = true;

        public bool IsProactiveObstacle { get; } = false;

        public Vector3 Position
        {
            get
            {
                return this.Modifier.Owner.Position;
            }
        }

        protected float Delay { get; }

        protected Unit9 ModifierOwner { get; }

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

        public virtual float GetDisableTime(Unit9 enemy)
        {
            if (!this.Modifier.IsValid)
            {
                return 0;
            }

            if (this.IgnoreModifierRemainingTime)
            {
                //todo change ?
                // modifier ignored time after counter, change to counter ability duration ?
                return 5;
            }

            return this.Modifier.RemainingTime - this.Delay;
        }

        public virtual float GetEvadeTime(Unit9 ally, bool blink)
        {
            if (!this.Modifier.IsValid)
            {
                return 0;
            }

            if (this.IgnoreModifierRemainingTime)
            {
                //todo change ?
                // modifier ignored time after counter, change to counter ability duration ?
                return 5;
            }

            return this.Modifier.RemainingTime - this.Delay;
        }

        public abstract bool IsIntersecting(Unit9 unit, bool checkPrediction);
    }
}