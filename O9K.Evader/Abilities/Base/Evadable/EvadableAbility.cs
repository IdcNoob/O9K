namespace O9K.Evader.Abilities.Base.Evadable
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Managers.Menu.Items;

    using Ensage;

    using Evader.EvadeModes;

    using Metadata;

    using Pathfinder.Obstacles;

    using PlaySharp.Toolkit.Helper.Annotations;

    using Usable;
    using Usable.BlinkAbility;
    using Usable.CounterAbility;
    using Usable.DisableAbility;

    internal abstract class EvadableAbility
    {
        protected EvadableAbility(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
        {
            this.Ability = ability;
            this.ActiveAbility = ability as ActiveAbility;
            this.Pathfinder = pathfinder;
            this.Menu = menu;
            this.Owner = ability.Owner;
        }

        public Ability9 Ability { get; }

        public ActiveAbility ActiveAbility { get; }

        public virtual HashSet<AbilityId> Blinks { get; } = new HashSet<AbilityId>();

        public virtual bool CanBeDodged { get; } = true;

        public virtual HashSet<AbilityId> Counters { get; } = new HashSet<AbilityId>();

        public MenuSlider DamageIgnore { get; set; }

        public virtual HashSet<AbilityId> Disables { get; } = new HashSet<AbilityId>();

        public MenuSwitcher Enabled { get; set; }

        public float EndCastTime { get; protected set; }

        public MenuSlider LevelIgnore { get; set; }

        public virtual HashSet<AbilityId> ModifierBlinks { get; } = new HashSet<AbilityId>();

        public MenuSwitcher ModifierCounterEnabled { get; set; }

        public virtual HashSet<AbilityId> ModifierCounters { get; } = new HashSet<AbilityId>();

        public virtual HashSet<AbilityId> ModifierDisables { get; } = new HashSet<AbilityId>();

        public Unit9 Owner { get; }

        public IPathfinder Pathfinder { get; }

        public List<EvadeMode> Priority { get; set; } = new List<EvadeMode>();

        public HashSet<AbilityId> ProactiveBlinks { get; } = new HashSet<AbilityId>();

        public HashSet<AbilityId> ProactiveCounters { get; } = new HashSet<AbilityId>();

        public HashSet<AbilityId> ProactiveDisables { get; } = new HashSet<AbilityId>();

        public float StartCastTime { get; protected set; }

        public MenuSlider TimeIgnore { get; set; }

        public virtual float TimeSinceCasted
        {
            get
            {
                return Game.RawGameTime - this.EndCastTime;
            }
        }

        public MenuSwitcher UseCustomPriority { get; set; }

        protected virtual IEnumerable<AbilityId> AllBlinks
        {
            get
            {
                return this.Blinks.Concat(this.ProactiveBlinks);
            }
        }

        protected virtual IEnumerable<AbilityId> AllCounters
        {
            get
            {
                return this.Counters.Concat(this.ProactiveCounters);
            }
        }

        protected virtual IEnumerable<AbilityId> AllDisables
        {
            get
            {
                return this.Disables.Concat(this.ProactiveDisables);
            }
        }

        protected virtual IEnumerable<AbilityId> AllModifierBlinks
        {
            get
            {
                return this.ModifierBlinks;
            }
        }

        protected virtual IEnumerable<AbilityId> AllModifierCounters
        {
            get
            {
                return this.ModifierCounters;
            }
        }

        protected virtual IEnumerable<AbilityId> AllModifierDisables
        {
            get
            {
                return this.ModifierDisables;
            }
        }

        protected IMainMenu Menu { get; }

        public virtual bool IgnoreRemainingTime(IObstacle obstacle, [CanBeNull] UsableAbility usableAbility)
        {
            return obstacle.IsModifierObstacle || obstacle.IsProactiveObstacle;
        }

        public bool IsCounteredBy(UsableAbility ability)
        {
            switch (ability)
            {
                case CounterAbility _:
                    return this.AllCounters.Contains(ability.Ability.Id);
                case BlinkAbility _:
                    return this.AllBlinks.Contains(ability.Ability.Id);
                case DisableAbility _:
                    return this.AllDisables.Contains(ability.Ability.Id);
            }

            return false;
        }

        public bool IsModifierCounteredBy(UsableAbility ability)
        {
            if (!(this is IModifierCounter))
            {
                return false;
            }

            switch (ability)
            {
                case CounterAbility _:
                    return this.AllModifierCounters.Contains(ability.Ability.Id);
                case BlinkAbility _:
                    return this.AllModifierBlinks.Contains(ability.Ability.Id);
                case DisableAbility _:
                    return this.AllModifierDisables.Contains(ability.Ability.Id);
            }

            return false;
        }

        public bool IsObstacleIgnored(Unit9 unit, IObstacle obstacle)
        {
            if (this.LevelIgnore > 0 && this.Ability.Level <= this.LevelIgnore)
            {
                return true;
            }

            if (this.TimeIgnore > 0 && Game.GameTime / 60 > this.TimeIgnore)
            {
                return true;
            }

            if (this.DamageIgnore > 0)
            {
                var damagePercentage = (obstacle.GetDamage(unit) / unit.Health) * 100;
                if (damagePercentage < this.DamageIgnore)
                {
                    return true;
                }
            }

            return false;
        }

        public virtual void PhaseCancel()
        {
            this.EndCastTime = 0;
            this.StartCastTime = 0;

            this.Pathfinder.CancelObstacle(this.Ability.Handle);
        }

        public virtual void PhaseStart()
        {
            this.StartCastTime = Game.RawGameTime - (Game.Ping / 2000);
            this.EndCastTime = this.StartCastTime + this.ActiveAbility.CastPoint;

            this.AddObstacle();
        }

        protected abstract void AddObstacle();
    }
}