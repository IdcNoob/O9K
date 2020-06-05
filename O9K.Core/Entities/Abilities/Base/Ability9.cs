namespace O9K.Core.Entities.Abilities.Base
{
    using System;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Prediction;

    public abstract class Ability9 : Entity9, IDisposable
    {
        protected SpecialData ActivationDelayData;

        protected SpecialData DamageData;

        protected SpecialData DurationData;

        protected SpecialData RadiusData;

        private string displayName;

        protected Ability9(Ability baseAbility)
            : base(baseAbility)
        {
            this.BaseAbility = baseAbility;
            this.Id = baseAbility.Id;
            this.AbilityBehavior = baseAbility.AbilityBehavior;
            this.DamageType = baseAbility.DamageType;
            this.MaximumLevel = baseAbility.MaximumLevel;
            this.DamageData = new SpecialData(baseAbility, baseAbility.GetDamage);
            this.DurationData = new SpecialData(baseAbility, (Func<uint, float>)baseAbility.GetDuration);
            this.CastPoint = baseAbility.OverrideCastPoint < 0 ? this.BaseAbility.GetCastPoint(0) : 0;

            if (baseAbility is Item item)
            {
                this.BaseItem = item;
                this.IsItem = true;
                this.IsDisplayingCharges = item.IsStackable || item.IsRequiringCharges || item.IsDisplayingCharges;
            }
            else
            {
                this.IsDisplayingCharges = baseAbility.GetChargesCount(0) > 0;
                this.IsUltimate = baseAbility.AbilityType == AbilityType.Ultimate;
                this.IsStolen = baseAbility.IsStolen;
            }

            switch (baseAbility.SpellPierceImmunityType)
            {
                case SpellPierceImmunityType.EnemiesYes:
                    this.CanHitSpellImmuneEnemy = true;
                    this.CanHitSpellImmuneAlly = true;
                    break;
                case SpellPierceImmunityType.AlliesYes:
                case SpellPierceImmunityType.None:
                    this.CanHitSpellImmuneAlly = true;
                    break;
            }
        }

        public static float InputLag
        {
            get
            {
                return Game.Ping / 1000;
            }
        }

        public virtual AbilityBehavior AbilityBehavior { get; }

        public AbilitySlot AbilitySlot
        {
            get
            {
                return this.BaseAbility.AbilitySlot;
            }
        }

        public virtual float ActivationDelay
        {
            get
            {
                return this.ActivationDelayData?.GetValue(this.Level) ?? 0;
            }
        }

        public Ability BaseAbility { get; }

        public Item BaseItem { get; }

        public virtual bool CanHitSpellImmuneAlly { get; }

        public virtual bool CanHitSpellImmuneEnemy { get; }

        public virtual float CastPoint { get; }

        public virtual float CastRange
        {
            get
            {
                return this.BaseCastRange;
            }
        }

        public virtual uint Charges
        {
            get
            {
                if (!this.IsDisplayingCharges)
                {
                    return 0;
                }

                return this.BaseAbility.CurrentCharges;
            }
        }

        public float Cooldown
        {
            get
            {
                if (!this.IsItem && this.IsDisplayingCharges)
                {
                    var level = this.Level - 1;
                    return Math.Max(this.BaseAbility.GetChargesRestoreTime(level), this.BaseAbility.GetCooldown(level));
                }

                return this.BaseAbility.CooldownLength;
            }
        }

        public virtual DamageType DamageType { get; }

        public override string DisplayName
        {
            get
            {
                if (this.displayName == null)
                {
                    try
                    {
                        this.displayName = LocalizationHelper.LocalizeAbilityName(this.Name);
                    }
                    catch
                    {
                        this.displayName = this.Name;
                    }
                }

                return this.displayName;
            }
        }

        public float Duration
        {
            get
            {
                return this.DurationData.GetValue(this.Level);
            }
        }

        public AbilityId Id { get; protected set; }

        public virtual bool IntelligenceAmplify { get; } = true;

        public bool IsAvailable { get; internal set; }

        public bool IsCasting { get; internal set; }

        public bool IsChanneling { get; internal set; }

        public bool IsControllable
        {
            get
            {
                return this.Owner.IsControllable;
            }
        }

        public virtual bool IsDisplayingCharges { get; }

        public bool IsFake
        {
            get
            {
                if (this.IsItem)
                {
                    return !this.Owner.CanUseItems;
                }

                return !this.Owner.CanUseAbilities;
            }
        }

        public bool IsHidden
        {
            get
            {
                if (this.IsItem)
                {
                    return false;
                }

                return this.BaseAbility.IsHidden;
            }
        }

        public virtual bool IsInvisibility { get; } = false;

        public bool IsItem { get; }

        public bool IsReady
        {
            get
            {
                if (!this.IsUsable || this.Level == 0 || this.Owner.Mana < this.ManaCost || this.RemainingCooldown > 0)
                {
                    return false;
                }

                return true;
            }
        }

        public bool IsStolen { get; }

        public virtual bool IsTalent { get; } = false;

        public bool IsUltimate { get; protected set; }

        public virtual bool IsUsable
        {
            get
            {
                if (!this.IsAvailable)
                {
                    return false;
                }

                if (this.IsItem)
                {
                    return !this.ItemEnableTimeSleeper.IsSleeping;
                }

                return !this.BaseAbility.IsHidden && this.BaseAbility.IsActivated;
            }
        }

        public virtual uint Level
        {
            get
            {
                return this.BaseAbility.Level;
            }
        }

        public float ManaCost
        {
            get
            {
                return this.BaseAbility.ManaCost;
            }
        }

        public int MaximumLevel { get; }

        public IPredictionManager9 PredictionManager { get; private set; }

        public virtual float Radius
        {
            get
            {
                return this.RadiusData?.GetValue(this.Level) ?? 0;
            }
        }

        public virtual float Range
        {
            get
            {
                return this.CastRange;
            }
        }

        public float RemainingCooldown
        {
            get
            {
                float cooldown;

                if (!this.IsItem && this.IsDisplayingCharges)
                {
                    if (this.Charges > 0)
                    {
                        return 0;
                    }

                    cooldown = this.BaseAbility.ChargeRestoreTimeRemaining;
                }
                else
                {
                    cooldown = this.BaseAbility.Cooldown;
                }

                if (this.IsItem && this.ItemEnableTimeSleeper)
                {
                    cooldown = Math.Max(cooldown, this.ItemEnableTimeSleeper.RemainingSleepTime);
                }

                if (!this.Owner.IsVisible)
                {
                    cooldown -= Game.RawGameTime - this.Owner.LastVisibleTime;
                }

                return cooldown;
            }
        }

        public virtual float TimeSinceCasted
        {
            get
            {
                var cooldownLength = this.BaseAbility.CooldownLength;
                return cooldownLength <= 0 ? 9999999 : cooldownLength - this.BaseAbility.Cooldown;
            }
        }

        internal Sleeper ItemEnableTimeSleeper { get; } = new Sleeper();

        protected virtual float BaseCastRange { get; } = 0;

        public static implicit operator Ability(Ability9 ability)
        {
            return ability.BaseAbility;
        }

        public static implicit operator Item(Ability9 ability)
        {
            return ability.BaseItem;
        }

        public abstract bool CanBeCasted(bool checkChanneling = true);

        public virtual void Dispose()
        {
            this.IsAvailable = false;
            this.Owner.Ability(this, false);
        }

        public virtual int GetDamage(Unit9 unit)
        {
            var damage = 0f;

            foreach (var raw in this.GetRawDamage(unit))
            {
                var amplify = unit.GetDamageAmplification(this.Owner, raw.Key, this.IntelligenceAmplify);
                var block = unit.GetDamageBlock(raw.Key);

                damage += (raw.Value - block) * amplify;
            }

            return (int)damage;
        }

        public virtual Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            return new Damage
            {
                [this.DamageType] = this.DamageData.GetValue(this.Level)
            };
        }

        public bool PiercesMagicImmunity(Unit9 target)
        {
            return (target.IsEnemy(this.Owner) && this.CanHitSpellImmuneEnemy) || (target.IsAlly(this.Owner) && this.CanHitSpellImmuneAlly);
        }

        internal virtual void SetOwner(Unit9 owner)
        {
            this.Owner = owner;
            this.Owner.Ability(this, true);
            this.IsAvailable = !this.IsItem;
        }

        internal void SetPrediction(IPredictionManager9 predictionManager)
        {
            this.PredictionManager = predictionManager;
        }
    }
}