namespace O9K.Evader.Pathfinder.Obstacles.Abilities
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Units;

    using Ensage;
    using Ensage.SDK.Geometry;

    using Helpers;

    using Metadata;

    using O9K.Evader.Abilities.Base.Evadable;

    using SharpDX;

    using Types;

    internal abstract class AbilityObstacle : IObstacle, IDrawable, IExpirable
    {
        protected readonly AbilityObstacleDrawer Drawer = new AbilityObstacleDrawer();

        private readonly Dictionary<Unit9, int> damages = new Dictionary<Unit9, int>();

        protected AbilityObstacle(EvadableAbility ability)
        {
            this.EvadableAbility = ability;
            this.Pathfinder = ability.Pathfinder;
            this.Caster = ability.Owner;
            this.Blinks = ability.Blinks.ToArray();
            this.Counters = ability.Counters.ToArray();
            this.Disables = ability.Disables.ToArray();
            this.ActivationDelay = ability.Ability.ActivationDelay;
            this.CanBeDodged = ability.CanBeDodged;
        }

        public float ActivationDelay { get; set; }

        public AbilityId[] Blinks { get; set; }

        public bool CanBeDodged { get; protected set; }

        public Unit9 Caster { get; }

        public AbilityId[] Counters { get; set; }

        public AbilityId[] Disables { get; set; }

        public float EndCastTime { get; set; }

        public float EndObstacleTime { get; set; }

        public EvadableAbility EvadableAbility { get; }

        public uint Id { get; set; }

        public virtual bool IsExpired
        {
            get
            {
                return Game.RawGameTime >= this.EndObstacleTime;
            }
        }

        public bool IsModifierObstacle { get; } = false;

        public bool IsProactiveObstacle { get; } = false;

        public uint? NavMeshId { get; set; }

        public Polygon Polygon { get; protected set; }

        public Vector3 Position { get; protected set; }

        protected IPathfinder Pathfinder { get; }

        public virtual void Dispose()
        {
            this.Drawer.Dispose();
        }

        public abstract void Draw();

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
                this.damages[ally] = damage = this.EvadableAbility.Ability.GetDamage(ally);
            }

            return damage;
        }

        public virtual IEnumerable<AbilityId> GetDisables(Unit9 ally)
        {
            return this.Disables;
        }

        public abstract float GetDisableTime(Unit9 enemy);

        public abstract float GetEvadeTime(Unit9 ally, bool blink);

        public virtual bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            if (this.Polygon == null)
            {
                return false;
            }

            if (!this.Polygon.IsOutside(unit.Position.To2D()))
            {
                return true;
            }

            if (!checkPrediction || this.NavMeshId == null || (!unit.IsMoving && !unit.IsRotating))
            {
                return false;
            }

            return !this.Polygon.IsOutside(unit.InFront(100).To2D());
        }
    }
}