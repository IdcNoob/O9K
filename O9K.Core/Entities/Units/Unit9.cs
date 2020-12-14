namespace O9K.Core.Entities.Units
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Abilities.Base;
    using Abilities.Base.Components;
    using Abilities.Base.Types;
    using Abilities.Items;
    using Abilities.NeutralItems;

    using Data;

    using Ensage;

    using Extensions;

    using Helpers;
    using Helpers.Damage;
    using Helpers.Range;

    using Managers.Entity;
    using Managers.Renderer.Utils;

    using SharpDX;

    using Attribute = Ensage.Attribute;

    public class Unit9 : Entity9
    {
        private readonly List<Ability9> abilities = new List<Ability9>();

        private readonly Sleeper actionSleeper = new Sleeper();

        private readonly List<IHasDamageAmplify> amplifiers = new List<IHasDamageAmplify>();

        private readonly Dictionary<string, IHasDamageBlock> blockers = new Dictionary<string, IHasDamageBlock>();

        private readonly Sleeper channelingSleeper = new Sleeper();

        private readonly MultiSleeper<UnitState> expectedUnitStateSleeper = new MultiSleeper<UnitState>();

        private readonly List<Modifier> immobilityModifiers = new List<Modifier>();

        private readonly List<Modifier> invulnerabilityModifiers = new List<Modifier>();

        private readonly List<IHasPassiveDamageIncrease> passiveDamageAbilities = new List<IHasPassiveDamageIncrease>();

        private readonly List<IHasRangeIncrease> ranges = new List<IHasRangeIncrease>();

        private float baseAttackAnimationPoint = -1;

        private float channelEndTime;

        private Vector2 healthBarPositionCorrection;

        private Vector2 healthBarSize;

        private int projectileSpeed = -1;

        private float turnRate = -1;

        public Unit9(Unit baseUnit)
            : base(baseUnit)
        {
            this.BaseUnit = baseUnit;
            this.BaseOwner = baseUnit.Owner;
            this.BasePosition = baseUnit.Position;
            this.IsControllable = baseUnit.IsControllable;
            this.Team = baseUnit.Team;
            this.EnemyTeam = this.Team == Team.Radiant ? Team.Dire : Team.Radiant;
            this.IsRanged = baseUnit.IsRanged;
            this.BaseAttackRange = baseUnit.AttackRange;
            this.HullRadius = baseUnit.HullRadius;
            this.MoveCapability = baseUnit.MoveCapability;
            this.AttackCapability = baseUnit.AttackCapability;
            this.AttackType = baseUnit.AttackDamageType;
            this.ArmorType = baseUnit.ArmorType;
            this.IsCreep = baseUnit is Creep;
            this.IsAncient = baseUnit.IsAncient;
            this.IsLaneCreep = baseUnit.UnitType == 1152;
            this.IsUnit = baseUnit.UnitType != 0;
            this.HpBarOffset = baseUnit.HealthBarOffset;
            this.BaseAttackTime = baseUnit.BaseAttackTime;
            this.Speed = baseUnit.MovementSpeed;
            this.BaseHealth = baseUnit.Health;
            this.BaseMana = baseUnit.Mana;
            this.BaseIsAlive = baseUnit.IsAlive;
            this.CreateTime = this.LastVisibleTime = this.LastPositionUpdateTime = Game.RawGameTime;
        }

        public IEnumerable<Ability9> Abilities
        {
            get
            {
                return this.abilities.Where(x => x.IsValid);
            }
        }

        public virtual float Agility { get; } = 0;

        public ArmorType ArmorType { get; }

        public AttackCapability AttackCapability { get; }

        public float AttacksPerSecond
        {
            get
            {
                return this.BaseUnit.AttacksPerSecond;
            }
        }

        public AttackDamageType AttackType { get; }

        public Inventory BaseInventory
        {
            get
            {
                return this.BaseUnit.Inventory;
            }
        }

        public IEnumerable<Modifier> BaseModifiers
        {
            get
            {
                return this.BaseUnit.Modifiers;
            }
        }

        public Entity BaseOwner { get; internal set; }

        public Vector3 BasePosition { get; internal set; }

        public Spellbook BaseSpellbook
        {
            get
            {
                return this.BaseUnit.Spellbook;
            }
        }

        public Unit BaseUnit { get; }

        public bool CanBecomeInvisible
        {
            get
            {
                return this.Abilities.Any(x => x.IsInvisibility && x.CanBeCasted(false)) || this.BaseUnit.InvisiblityLevel > 0;
            }
        }

        public bool CanBecomeMagicImmune
        {
            get
            {
                return this.Abilities.OfType<IShield>().Any(x => x.ShieldsOwner && x.IsMagicImmunity() && x.CanBeCasted(false));
            }
        }

        public bool CanBeHealed { get; internal set; } = true;

        public bool CanDie
        {
            get
            {
                return !this.amplifiers.Any(
                           x => x.IsValid && (x.AmplifiesDamage & AmplifiesDamage.Incoming) != 0
                                          && (x.AmplifierDamageType & DamageType.Pure) != 0 && x.AmplifierValue(this, this) <= -1);
            }
        }

        public virtual bool CanReincarnate
        {
            get
            {
                return this.HasAegis;
            }
        }

        public bool CanUseAbilities { get; protected set; } = true;

        public bool CanUseAbilitiesInInvisibility { get; internal set; } = false;

        public bool CanUseItems { get; protected set; } = false;

        public bool ChannelActivatesOnCast { get; internal set; }

        public float ChannelEndTime
        {
            get
            {
                return this.channelEndTime;
            }
            internal set
            {
                this.channelEndTime = value;
                this.channelingSleeper.SleepUntil(value);
            }
        }

        public float CreateTime { get; }

        public Team EnemyTeam { get; internal set; }

        public bool HasAghanimsScepter
        {
            get
            {
                return this.HasAghanimsScepterBlessing || this.AghanimsScepter?.IsUsable == true;
            }
        }

        public virtual float Health
        {
            get
            {
                if (!this.IsVisible)
                {
                    return Math.Min(
                        this.BaseHealth + ((Game.RawGameTime - this.LastVisibleTime) * this.HealthRegeneration),
                        this.MaximumHealth);
                }

                return this.BaseHealth;
            }
        }

        public Rectangle9 HealthBar
        {
            get
            {
                var position = this.HealthBarPosition;
                if (position.IsZero)
                {
                    return Rectangle9.Zero;
                }

                var size = this.HealthBarSize;
                return new Rectangle9(position, size.X, size.Y);
            }
        }

        public Vector2 HealthBarPosition
        {
            get
            {
                //if ((UnitState & UnitState.NoHealthbar) != 0)
                //{
                //    return Vector2.Zero;
                //}

                var position = this.BasePosition.IncreaseZ(this.HpBarOffset);
                if (!Drawing.WorldToScreen(position, out var screenPosition))
                {
                    return Vector2.Zero;
                }

                return screenPosition - this.HealthBarPositionCorrection;
            }
        }

        public virtual Vector2 HealthBarSize
        {
            get
            {
                if (this.healthBarSize.IsZero)
                {
                    this.healthBarSize = new Vector2(Hud.Info.ScreenRatio * 78, Hud.Info.ScreenRatio * 6);
                }

                return this.healthBarSize;
            }
        }

        /// <summary>
        ///     x%
        /// </summary>
        public float HealthPercentage
        {
            get
            {
                return this.HealthPercentageBase * 100;
            }
        }

        /// <summary>
        ///     0.x%
        /// </summary>
        public float HealthPercentageBase
        {
            get
            {
                return this.Health / this.MaximumHealth;
            }
        }

        public virtual float HealthRegeneration
        {
            get
            {
                return this.BaseUnit.HealthRegeneration;
            }
        }

        public bool HideHud { get; internal set; } = false;

        public float HullRadius { get; }

        public virtual float Intelligence { get; } = 0;

        public virtual bool IsAlive
        {
            get
            {
                if (this.IsCreep && !this.BaseUnit.IsSpawned)
                {
                    return false;
                }

                return this.BaseIsAlive;
            }
        }

        public bool IsAncient { get; protected set; }

        public bool IsAttackImmune
        {
            get
            {
                return (this.UnitState & UnitState.AttackImmune) != 0;
            }
        }

        public bool IsAttacking { get; internal set; }

        public bool IsBarrack { get; internal set; }

        /// <summary>
        ///     Lotus, Linkens, Spell shield, Untargetable
        /// </summary>
        public bool IsBlockingAbilities
        {
            get
            {
                return this.IsLotusProtected || this.IsLinkensProtected || this.IsSpellShieldProtected || this.IsUntargetable;
            }
        }

        public bool IsBuilding { get; protected set; }

        public bool IsCasting { get; internal set; }

        public bool IsChanneling
        {
            get
            {
                return this.channelingSleeper.IsSleeping || this.abilities.Any(x => x.IsChanneling);
            }
        }

        public bool IsCharging { get; internal set; } = false;

        public bool IsCommandRestricted
        {
            get
            {
                return (this.UnitState & UnitState.CommandRestricted) != 0;
            }
        }

        public bool IsControllable { get; internal set; }

        public bool IsCourier { get; protected set; }

        public bool IsCreep { get; protected set; }

        public bool IsDarkPactProtected { get; internal set; }

        public bool IsDisarmed
        {
            get
            {
                return (this.UnitState & UnitState.Disarmed) != 0 || this.AttackCapability == AttackCapability.None;
            }
        }

        public bool IsEthereal { get; internal set; } = false;

        public bool IsFountain { get; internal set; }

        public bool IsHero { get; protected set; }

        public bool IsHexed
        {
            get
            {
                return (this.UnitState & UnitState.Hexed) != 0;
            }
        }

        public bool IsIllusion { get; protected set; } = false;

        public bool IsImportant { get; protected set; } = false;

        public bool IsInNormalState
        {
            get
            {
                return (this.UnitState & (UnitState.Disarmed | UnitState.Silenced | UnitState.Stunned | UnitState.Rooted | UnitState.Hexed
                                          | UnitState.Muted)) == 0 && this.GetImmobilityDuration() <= 0;
            }
        }

        public bool IsInvisible
        {
            get
            {
                return (this.UnitState & UnitState.Invisible) != 0 || this.BaseUnit.InvisiblityLevel > 0.5f;
            }
        }

        public virtual bool IsInvulnerable
        {
            get
            {
                return (this.UnitState & (UnitState.Invulnerable | UnitState.NoHealthbar)) != 0;
            }
        }

        public bool IsLaneCreep { get; }

        public bool IsLeashed
        {
            get
            {
                return (this.UnitState & UnitState.Tethered) != 0;
            }
        }

        public virtual bool IsLinkensProtected
        {
            get
            {
                return this.IsLinkensTargetProtected
                       || (this.CanUseAbilities && (this.LinkensSphere?.IsReady == true || this.MirrorShield?.IsReady == true));
            }
        }

        public bool IsLotusProtected { get; internal set; }

        public bool IsMagicImmune
        {
            get
            {
                return (this.UnitState & UnitState.MagicImmune) != 0;
            }
        }

        public bool IsMoving
        {
            get
            {
                return this.BaseUnit.IsMoving;
            }
        }

        public bool IsMuted
        {
            get
            {
                return (this.UnitState & UnitState.Muted) != 0;
            }
        }

        public bool IsMyControllable
        {
            get
            {
                if (!this.IsUnit || !this.IsControllable)
                {
                    return false;
                }

                if (this.IsHero)
                {
                    if (this.BaseOwner?.Handle == EntityManager9.Owner.PlayerHandle)
                    {
                        return true;
                    }
                }
                else if (this.BaseOwner?.Handle == EntityManager9.Owner.HeroHandle)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsMyHero { get; internal set; }

        public bool IsRanged { get; internal set; }

        public bool IsReflectingDamage { get; internal set; }

        public bool IsRooted
        {
            get
            {
                return (this.UnitState & UnitState.Rooted) != 0 || this.MoveCapability == MoveCapability.None;
            }
        }

        public bool IsRotating
        {
            get
            {
                return (int)this.BaseUnit.RotationDifference != 0;
            }
        }

        public bool IsRuptured { get; internal set; }

        public bool IsSilenced
        {
            get
            {
                return (this.UnitState & UnitState.Silenced) != 0;
            }
        }

        public virtual bool IsSpellShieldProtected { get; internal set; }

        public bool IsStunned
        {
            get
            {
                return (this.UnitState & (UnitState.Stunned | UnitState.CommandRestricted)) != 0;
            }
        }

        public bool IsTeleporting { get; internal set; } = false;

        public bool IsTower { get; protected set; }

        public bool IsUnit { get; protected set; }

        public bool IsUntargetable
        {
            get
            {
                return (this.UnitState & UnitState.Untargetable) != 0;
            }
        }

        public bool IsVisible { get; internal set; }

        public bool IsVisibleToEnemies
        {
            get
            {
                return this.BaseUnit.IsVisibleToEnemies;
            }
        }

        public float LastNotVisibleTime { get; internal set; }

        public float LastPositionUpdateTime { get; internal set; }

        public float LastVisibleTime { get; internal set; }

        public uint Level
        {
            get
            {
                return this.BaseUnit.Level;
            }
        }

        public virtual float Mana
        {
            get
            {
                if (!this.IsVisible)
                {
                    return Math.Min(this.BaseMana + ((Game.RawGameTime - this.LastVisibleTime) * this.ManaRegeneration), this.MaximumMana);
                }

                return this.BaseMana;
            }
        }

        public float ManaPercentage
        {
            get
            {
                return this.ManaPercentageBase * 100;
            }
        }

        public float ManaPercentageBase
        {
            get
            {
                return this.Mana / this.MaximumMana;
            }
        }

        public virtual float ManaRegeneration
        {
            get
            {
                return this.BaseUnit.ManaRegeneration;
            }
        }

        public float MaximumHealth
        {
            get
            {
                return this.BaseUnit.MaximumHealth;
            }
        }

        public float MaximumMana
        {
            get
            {
                return this.BaseUnit.MaximumMana;
            }
        }

        public MoveCapability MoveCapability { get; }

        public virtual Vector3 Position
        {
            get
            {
                return this.BasePosition;
            }
        }

        public Attribute PrimaryAttribute { get; protected set; } = Attribute.Invalid;

        public int ProjectileSpeed
        {
            get
            {
                if (this.projectileSpeed < 0)
                {
                    try
                    {
                        this.projectileSpeed = Game.FindKeyValues(
                                this.Name + "/ProjectileSpeed",
                                this.IsHero ? KeyValueSource.Hero : KeyValueSource.Unit)
                            .IntValue;
                    }
                    catch
                    {
                        this.projectileSpeed = 0;
                    }
                }

                return this.projectileSpeed;
            }
        }

        public float SecondsPerAttack
        {
            get
            {
                return this.BaseUnit.SecondsPerAttack;
            }
        }

        public float Speed { get; internal set; }

        /// <summary>
        ///     0.x%
        /// </summary>
        public float StatusResist
        {
            get
            {
                return this.BaseUnit.StatusResist;
            }
        }

        public virtual float Strength { get; } = 0;

        public Unit9 Target { get; internal set; }

        public Team Team { get; internal set; }

        public virtual float TotalAgility { get; } = 0;

        public virtual float TotalIntelligence { get; } = 0;

        public virtual float TotalStrength { get; } = 0;

        public float TurnRate
        {
            get
            {
                if (this.turnRate < 0)
                {
                    try
                    {
                        this.turnRate = Game.FindKeyValues(
                                this.Name + "/MovementTurnRate",
                                this.IsHero ? KeyValueSource.Hero : KeyValueSource.Unit)
                            .FloatValue;
                    }
                    catch
                    {
                        this.turnRate = 0.5f;
                    }
                }

                // todo phase boots modifier
                // todo add modifier check ?

                return this.turnRate;
            }
        }

        public UnitState UnitState
        {
            get
            {
                var state = this.BaseUnit.UnitState;

                foreach (var sleeper in this.expectedUnitStateSleeper)
                {
                    if (sleeper.Value.IsSleeping)
                    {
                        state |= sleeper.Key;
                    }
                }

                return state;
            }
        }

        internal IEnumerable<Ability9> AbilitiesFast
        {
            get
            {
                return this.abilities;
            }
        }

        internal AghanimsScepter AghanimsScepter { get; set; }

        internal virtual float BaseAttackRange { get; }

        internal float BaseHealth { get; set; }

        internal bool BaseIsAlive { get; set; }

        internal float BaseMana { get; set; }

        internal float BonusAttackRange
        {
            get
            {
                return this.ranges.Where(x => x.IsValid && (x.RangeIncreaseType & RangeIncreaseType.Attack) != 0)
                    .Sum(x => x.GetRangeIncrease(this, RangeIncreaseType.Attack));
            }
        }

        internal float BonusCastRange
        {
            get
            {
                return this.ranges.Where(x => x.IsValid && (x.RangeIncreaseType & RangeIncreaseType.Ability) != 0)
                    .Sum(x => x.GetRangeIncrease(this, RangeIncreaseType.Ability));
            }
        }

        internal bool HasAegis { get; set; }

        internal bool HasAghanimsScepterBlessing { get; set; }

        internal virtual float HpBarOffset { get; }

        internal Unit9 HurricanePikeTarget { get; set; }

        internal bool IsLinkensTargetProtected { get; set; }

        internal LinkensSphere LinkensSphere { get; set; }

        internal MirrorShield MirrorShield { get; set; }

        protected virtual float BaseAttackTime { get; }

        protected virtual Vector2 HealthBarPositionCorrection
        {
            get
            {
                if (this.healthBarPositionCorrection.IsZero)
                {
                    this.healthBarPositionCorrection = new Vector2(this.HealthBarSize.X / 2f, Hud.Info.ScreenRatio * 11);
                }

                return this.healthBarPositionCorrection;
            }
        }

        private float BaseAttackAnimationPoint
        {
            get
            {
                if (this.baseAttackAnimationPoint < 0)
                {
                    try
                    {
                        this.baseAttackAnimationPoint = Game.FindKeyValues(
                                this.Name + "/AttackAnimationPoint",
                                this.IsHero ? KeyValueSource.Hero : KeyValueSource.Unit)
                            .FloatValue;
                    }
                    catch
                    {
                        this.baseAttackAnimationPoint = 0;
                    }
                }

                return this.baseAttackAnimationPoint;
            }
        }

        public static implicit operator Unit(Unit9 unit)
        {
            return unit.BaseUnit;
        }

        public bool Attack(Unit9 target)
        {
            if (this.actionSleeper.IsSleeping)
            {
                return false;
            }

            if (!this.BaseUnit.Attack(target.BaseUnit))
            {
                return false;
            }

            this.actionSleeper.Sleep(0.1f);
            return true;
        }

        public bool CanAttack()
        {
            return !this.IsDisarmed && !this.IsCasting && !this.IsStunned && !this.IsChanneling
                   && this.AttackCapability != AttackCapability.None;
        }

        public bool CanAttack(Unit9 target, float additionalRange = 0)
        {
            if (!target.IsAlive || target.IsInvulnerable || target.IsUntargetable || target.IsAttackImmune || !target.IsVisible)
            {
                return false;
            }

            if (this.Distance(target) > this.GetAttackRange(target, additionalRange))
            {
                return false;
            }

            return this.CanAttack();
        }

        public bool CanMove(bool checkChanneling = true)
        {
            if (checkChanneling && this.IsChanneling)
            {
                return false;
            }

            return !this.IsRooted && !this.IsStunned && !this.IsInvulnerable && this.MoveCapability != MoveCapability.None
                   && this.Speed > 0;
        }

        public void ChangeBasePosition(Vector3 position)
        {
            if (this.IsVisible || this.BasePosition == position)
            {
                return;
            }

            //todo angle?

            this.LastPositionUpdateTime = Game.RawGameTime;
            this.BasePosition = position;
        }

        public float Distance(Unit9 unit)
        {
            var p1 = this.Position;
            var p2 = unit.Position;

            return Vector2.Distance(new Vector2(p1.X, p1.Y), new Vector2(p2.X, p2.Y));
        }

        public float Distance(Vector3 position)
        {
            var p1 = this.Position;

            return Vector2.Distance(new Vector2(p1.X, p1.Y), new Vector2(position.X, position.Y));
        }

        public float DistanceSquared(Unit9 unit)
        {
            var p1 = this.Position;
            var p2 = unit.Position;

            return Vector2.DistanceSquared(new Vector2(p1.X, p1.Y), new Vector2(p2.X, p2.Y));
        }

        public float DistanceSquared(Vector3 position)
        {
            var p1 = this.Position;

            return Vector2.DistanceSquared(new Vector2(p1.X, p1.Y), new Vector2(position.X, position.Y));
        }

        public Ability GetAbilityById(AbilityId id)
        {
            // todo change
            return this.BaseSpellbook.Spells.FirstOrDefault(x => x.Id == id);
        }

        public float GetAngle(Unit9 unit, bool rotationDifference = false)
        {
            return this.GetAngle(unit.Position, rotationDifference);
        }

        public virtual float GetAngle(Vector3 position, bool rotationDifference = false)
        {
            var angle = Math.Abs(Math.Atan2(position.Y - this.Position.Y, position.X - this.Position.X) - this.BaseUnit.NetworkRotationRad);
            if (angle > Math.PI)
            {
                angle = Math.Abs((Math.PI * 2) - angle);
            }

            return (float)angle;
        }

        public float GetAttackBackswing(Unit9 target = null)
        {
            return this.GetAttackRate(target) - this.GetAttackPoint(target);
        }

        public int GetAttackDamage(Unit9 target, DamageValue damageValue = DamageValue.Minimum, float additionalPhysicalDamage = 0)
        {
            var damage = 0;

            foreach (var raw in this.GetRawAttackDamage(target, damageValue, additionalPhysicalDamage: additionalPhysicalDamage))
            {
                var amplify = target.GetDamageAmplification(this, raw.Key, false);
                var block = target.GetDamageBlock(raw.Key);

                damage += (int)((raw.Value - block) * amplify);
            }

            return Math.Max(damage, 0);
        }

        public float GetAttackPoint(Unit9 target = null)
        {
            return this.BaseAttackAnimationPoint / (1 + ((this.GetAttackSpeed(target) - 100) / 100));
        }

        public float GetAttackRange()
        {
            return this.BaseAttackRange + this.BonusAttackRange + this.HullRadius;
        }

        public float GetAttackRange(Unit9 target, float additionalRange = 0)
        {
            if (this.HurricanePikeTarget?.Equals(target) == true)
            {
                return 9999999;
            }

            return this.GetAttackRange() + target.HullRadius + additionalRange;
        }

        public float GetDamageAmplification(Unit9 source, DamageType damageType, bool intAmplify)
        {
            var amp = 1f;

            //todo completely remove int amp
            //if (intAmplify)
            //{
            //    amp += source.TotalIntelligence * GameData.DamageAmplifyPerIntelligence;
            //}

            var outAmp = source.amplifiers
                .Where(x => x.IsValid && (x.AmplifiesDamage & AmplifiesDamage.Outgoing) != 0 && (x.AmplifierDamageType & damageType) != 0)
                .Sum(x => x.AmplifierValue(this, source));

            var inAmp = this.amplifiers
                .Where(x => x.IsValid && (x.AmplifiesDamage & AmplifiesDamage.Incoming) != 0 && (x.AmplifierDamageType & damageType) != 0)
                .Sum(x => x.AmplifierValue(source, this));

            amp *= Math.Max(1 + outAmp, 0) * Math.Max(1 + inAmp, 0);

            switch (damageType)
            {
                case DamageType.Magical:
                {
                    amp *= 1 - this.BaseUnit.MagicDamageResist;
                    break;
                }

                case DamageType.Physical:
                {
                    amp *= 1 - (this.IsEthereal ? 1 : this.BaseUnit.DamageResist);
                    break;
                }
            }

            return amp;
        }

        public float GetDamageBlock(DamageType damageType)
        {
            return this.blockers.Values.Where(x => x.IsValid && (x.BlockDamageType & damageType) != 0).Sum(x => x.BlockValue(this));
        }

        public IEnumerable<IHasDamageBlock> GetDamageBlockers()
        {
            return this.blockers.Values.Where(x => x.IsValid);
        }

        public virtual float GetImmobilityDuration()
        {
            var modifiers = this.immobilityModifiers.Where(x => x.IsValid && x.ElapsedTime > 0.1f).ToList();
            if (modifiers.Count == 0)
            {
                return 0;
            }

            return modifiers.Max(x => x.RemainingTime);
        }

        public virtual float GetInvulnerabilityDuration()
        {
            var modifiers = this.invulnerabilityModifiers.Where(x => x.IsValid && x.ElapsedTime > 0.1f).ToList();
            if (modifiers.Count == 0)
            {
                return 0;
            }

            return modifiers.Max(x => x.RemainingTime);
        }

        public Modifier GetModifier(string name)
        {
            return this.BaseModifiers.FirstOrDefault(x => x.Name == name);
        }

        public int GetModifierStacks(string name)
        {
            return this.BaseModifiers.FirstOrDefault(x => x.Name == name)?.StackCount ?? 0;
        }

        public virtual Vector3 GetPredictedPosition(float delay = 0, bool forceMovement = false)
        {
            if (!forceMovement && (!this.IsMoving || delay <= 0))
            {
                return this.Position;
            }

            var rotation = this.BaseUnit.NetworkRotationRad;
            var polar = new Vector3((float)Math.Cos(rotation), (float)Math.Sin(rotation), 0);

            return this.Position + (polar * delay * this.Speed);
        }

        public float GetTurnTime(Vector3 position)
        {
            return this.GetTurnTime(this.GetAngle(position));
        }

        public virtual float GetTurnTime(float angleRad)
        {
            angleRad -= 0.2f;

            if (angleRad <= 0)
            {
                return 0;
            }

            return (0.03f / this.TurnRate) * angleRad * 1.15f;
        }

        public bool HasModifier(params string[] names)
        {
            return this.BaseModifiers.Any(x => names.Contains(x.Name));
        }

        public bool HasModifier(string name)
        {
            return this.BaseModifiers.Any(x => x.Name == name);
        }

        public Vector3 InFront(float range, float angle = 0, bool rotationDifference = true)
        {
            var diff = MathUtil.DegreesToRadians((rotationDifference ? this.BaseUnit.RotationDifference : 0) + angle);
            var alpha = this.BaseUnit.NetworkRotationRad + diff;
            var polar = new Vector3((float)Math.Cos(alpha), (float)Math.Sin(alpha), 0);

            return this.Position + (polar * range);
        }

        public bool IsAlly()
        {
            return this.Team == EntityManager9.Owner.Team;
        }

        public bool IsAlly(Unit9 unit)
        {
            return unit.Team == this.Team;
        }

        public bool IsAlly(Team team)
        {
            return team == this.Team;
        }

        public bool IsAttackingHero()
        {
            return this.Target?.IsValid == true && this.Target.IsHero && !this.Target.IsIllusion && this.Target.IsAlive
                   && this.Distance(this.Target) < this.GetAttackRange(this.Target, 200);
        }

        public bool IsEnemy()
        {
            return this.Team != EntityManager9.Owner.Team;
        }

        public bool IsEnemy(Unit9 unit)
        {
            return unit.Team == this.EnemyTeam;
        }

        public bool IsEnemy(Team team)
        {
            return team == this.EnemyTeam;
        }

        public bool Move(Unit9 toTarget)
        {
            return this.Move(toTarget.Position);
        }

        public bool Move(Vector3 position)
        {
            if (this.actionSleeper.IsSleeping)
            {
                return false;
            }

            if (!this.BaseUnit.Move(position))
            {
                return false;
            }

            this.actionSleeper.Sleep(0.1f);
            return true;
        }

        public void RefreshUnitState()
        {
            this.expectedUnitStateSleeper.Reset();
        }

        public void SetExpectedUnitState(UnitState state, float time = 0.1f)
        {
            this.expectedUnitStateSleeper[state].ExtendSleep(Math.Min(time, 2));
        }

        public bool Stop()
        {
            if (this.actionSleeper.IsSleeping)
            {
                return false;
            }

            if (!this.BaseUnit.Stop())
            {
                return false;
            }

            this.actionSleeper.Sleep(0.1f);
            return true;
        }

        internal virtual void Ability(Ability9 ability, bool added)
        {
            if (added)
            {
                this.abilities.Add(ability);
            }
            else
            {
                this.abilities.Remove(ability);
            }
        }

        internal void Amplifier(IHasDamageAmplify amplify, bool added)
        {
            if (added)
            {
                this.amplifiers.Add(amplify);
            }
            else
            {
                this.amplifiers.Remove(amplify);
            }
        }

        internal void Blocker(IHasDamageBlock block, bool added)
        {
            if (added)
            {
                this.blockers[block.BlockModifierName] = block;
            }
            else
            {
                this.blockers.Remove(block.BlockModifierName);
            }
        }

        internal void Disabler(Modifier modifier, bool added, bool invulnerability)
        {
            if (added)
            {
                this.immobilityModifiers.Add(modifier);

                if (invulnerability)
                {
                    this.invulnerabilityModifiers.Add(modifier);
                }
            }
            else
            {
                // todo fix
                var mod = this.immobilityModifiers.Find(x => x.Handle == modifier.Handle);
                if (mod != null)
                {
                    this.immobilityModifiers.Remove(mod);

                    if (invulnerability)
                    {
                        this.invulnerabilityModifiers.Remove(mod);
                    }
                }
            }
        }

        internal void ForceUnitState(UnitState state, float time)
        {
            this.expectedUnitStateSleeper[state].Sleep(time);
        }

        internal virtual float GetAttackRate(Unit9 target = null)
        {
            return this.BaseAttackTime / (1 + ((this.GetAttackSpeed(target) - 100) / 100));
        }

        internal virtual float GetAttackSpeed(Unit9 target = null)
        {
            var bonusAttackSpeed = 0f;
            if (this.HurricanePikeTarget?.Equals(target) == true)
            {
                //todo change
                bonusAttackSpeed += GameData.HurricanePikeBonusAttackSpeed - 10;
            }

            return Math.Max(Math.Min(this.BaseUnit.AttackSpeedValue + bonusAttackSpeed, GameData.MaxAttackSpeed), GameData.MinAttackSpeed);
        }

        internal Damage GetOnHitEffectDamage(Unit9 unit)
        {
            var damageValues = new Damage();

            foreach (var passive in this.passiveDamageAbilities)
            {
                if (!passive.IsValid)
                {
                    continue;
                }

                damageValues += passive.GetRawDamage(unit);
            }

            return damageValues;
        }

        internal Damage GetRawAttackDamage(
            Unit9 target,
            DamageValue damageValue = DamageValue.Minimum,
            float physCritMultiplier = 1f,
            float additionalPhysicalDamage = 0)
        {
            var damage = new Damage();

            var multipliedPhysDamage = 0f;

            switch (damageValue)
            {
                case DamageValue.Minimum:
                    multipliedPhysDamage += this.BaseUnit.MinimumDamage;
                    break;
                case DamageValue.Average:
                    multipliedPhysDamage += this.BaseUnit.DamageAverage;
                    break;
                case DamageValue.Maximum:
                    multipliedPhysDamage += this.BaseUnit.MaximumDamage;
                    break;
            }

            if (this.IsIllusion && !this.CanUseAbilities)
            {
                //todo improve illusion damage ?
                multipliedPhysDamage *= 0.25f;
            }
            else
            {
                multipliedPhysDamage += this.BaseUnit.BonusDamage;
            }

            if (multipliedPhysDamage <= 0)
            {
                return damage;
            }

            foreach (var passive in this.passiveDamageAbilities)
            {
                if (!passive.IsValid)
                {
                    continue;
                }

                foreach (var passiveDamage in passive.GetRawDamage(target))
                {
                    if (passive.MultipliedByCrit && passiveDamage.Key == DamageType.Physical)
                    {
                        multipliedPhysDamage += passiveDamage.Value;
                    }
                    else
                    {
                        damage[passiveDamage.Key] += passiveDamage.Value;
                    }
                }
            }

            damage[DamageType.Physical] += (multipliedPhysDamage * physCritMultiplier) + additionalPhysicalDamage;
            damage[DamageType.Physical] *= DamageExtensions.GetMeleeDamageMultiplier(this, target);

            return damage;
        }

        internal void Passive(IHasPassiveDamageIncrease passive, bool added)
        {
            if (added)
            {
                this.passiveDamageAbilities.Add(passive);
            }
            else
            {
                this.passiveDamageAbilities.Remove(passive);
            }
        }

        internal void Range(IHasRangeIncrease range, bool added)
        {
            if (added)
            {
                this.ranges.Add(range);
            }
            else
            {
                this.ranges.Remove(range);
            }
        }
    }
}