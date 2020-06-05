namespace O9K.Core.Entities.Heroes
{
    using System;

    using Ensage;

    using Helpers;

    using SharpDX;

    using Units;

    public class Hero9 : Unit9
    {
        private Vector2 healthBarPositionCorrection;

        private Vector2 healthBarSize;

        public Hero9(Hero baseHero)
            : base(baseHero)
        {
            this.IsHero = true;
            this.BaseHero = baseHero;
            this.Id = baseHero.HeroId;
            this.PrimaryAttribute = baseHero.PrimaryAttribute;
            this.IsIllusion = baseHero.IsIllusion;
            this.IsImportant = !this.IsIllusion;
            this.CanUseAbilities = !this.IsIllusion;
            this.CanUseItems = this.CanUseAbilities;
        }

        public override float Agility
        {
            get
            {
                return this.BaseHero.Agility;
            }
        }

        public Hero BaseHero { get; }

        public override float Health
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

        public override Vector2 HealthBarSize
        {
            get
            {
                if (this.healthBarSize.IsZero)
                {
                    if (this.IsMyHero)
                    {
                        this.healthBarSize = new Vector2(Hud.Info.ScreenRatio * 106, Hud.Info.ScreenRatio * 10);
                    }
                    else if (this.IsAlly())
                    {
                        this.healthBarSize = new Vector2(Hud.Info.ScreenRatio * 98, Hud.Info.ScreenRatio * 8);
                    }
                    else
                    {
                        this.healthBarSize = new Vector2(Hud.Info.ScreenRatio * 98, Hud.Info.ScreenRatio * 10);
                    }
                }

                return this.healthBarSize;
            }
        }

        public override float HealthRegeneration
        {
            get
            {
                return this.BaseUnit.BaseHealthRegeneration;
            }
        }

        public HeroId Id { get; }

        public override float Intelligence
        {
            get
            {
                return this.BaseHero.Intelligence;
            }
        }

        public override bool IsAlive
        {
            get
            {
                return base.IsAlive || this.IsReincarnating;
            }
        }

        public override bool IsInvulnerable
        {
            get
            {
                return base.IsInvulnerable || this.IsReincarnating;
            }
        }

        public bool IsReincarnating { get; internal set; } = false;

        public override float Mana
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

        public override float ManaRegeneration
        {
            get
            {
                return this.BaseUnit.BaseManaRegeneration;
            }
        }

        public override Vector3 Position
        {
            get
            {
                if (!this.IsVisible)
                {
                    return this.GetPredictedPosition();
                }

                return this.BasePosition;
            }
        }

        public override float Strength
        {
            get
            {
                return this.BaseHero.Strength;
            }
        }

        public override float TotalAgility
        {
            get
            {
                return this.BaseHero.TotalAgility;
            }
        }

        public override float TotalIntelligence
        {
            get
            {
                return this.BaseHero.TotalIntelligence;
            }
        }

        public override float TotalStrength
        {
            get
            {
                return this.BaseHero.TotalStrength;
            }
        }

        protected override Vector2 HealthBarPositionCorrection
        {
            get
            {
                if (this.healthBarPositionCorrection.IsZero)
                {
                    if (this.IsMyHero)
                    {
                        this.healthBarPositionCorrection = new Vector2(this.HealthBarSize.X / 1.98f, Hud.Info.ScreenRatio * 35.5f);
                    }
                    else
                    {
                        this.healthBarPositionCorrection = new Vector2(this.HealthBarSize.X / 1.92f, Hud.Info.ScreenRatio * 30.5f);
                    }

                    //if (this.IsAlly())
                    //{
                    //    this.healthBarPositionCorrection = new Vector2(this.HealthBarSize.X / 1.98f, Hud.Info.ScreenRatio * 30);
                    //}
                    //else
                    //{
                    //    this.healthBarPositionCorrection =new Vector2(this.HealthBarSize.X / 1.92f, Hud.Info.ScreenRatio * 30);
                    //}
                }

                return this.healthBarPositionCorrection;
            }
        }

        public override float GetAngle(Vector3 position, bool rotationDifference = false)
        {
            var rotation = this.BaseUnit.NetworkRotationRad;
            if (rotationDifference)
            {
                rotation += MathUtil.DegreesToRadians(this.BaseUnit.RotationDifference);
            }

            var angle = Math.Abs(Math.Atan2(position.Y - this.Position.Y, position.X - this.Position.X) - rotation);
            if (angle > Math.PI)
            {
                angle = Math.Abs((Math.PI * 2) - angle);
            }

            return (float)angle;
        }

        public override float GetImmobilityDuration()
        {
            if (this.IsReincarnating)
            {
                return (this.BaseHero.RespawnTime - Game.RawGameTime) + 0.1f;
            }

            return base.GetImmobilityDuration();
        }

        public override float GetInvulnerabilityDuration()
        {
            if (this.IsReincarnating)
            {
                return (this.BaseHero.RespawnTime - Game.RawGameTime) + 0.1f;
            }

            return base.GetInvulnerabilityDuration();
        }

        public override Vector3 GetPredictedPosition(float delay = 0, bool forceMovement = false)
        {
            if (!forceMovement && !this.IsMoving)
            {
                return this.BasePosition;
            }

            var rotationDifference = MathUtil.DegreesToRadians(this.BaseUnit.RotationDifference);
            delay = Math.Max(delay - this.GetTurnTime(Math.Abs(rotationDifference)), 0);

            var rotation = this.BaseUnit.NetworkRotationRad + rotationDifference;
            var polar = new Vector3((float)Math.Cos(rotation), (float)Math.Sin(rotation), 0);

            return this.BasePosition + (polar * ((Game.RawGameTime - this.LastPositionUpdateTime) + delay) * this.Speed);
        }
    }
}