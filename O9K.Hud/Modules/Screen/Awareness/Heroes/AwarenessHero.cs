namespace O9K.Hud.Modules.Screen.Awareness.Heroes
{
    using System;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Heroes;
    using Core.Entities.Units;
    using Core.Extensions;

    using Ensage;
    using Ensage.SDK.Geometry;

    using SharpDX;

    internal class AwarenessHero
    {
        private enum DistanceState
        {
            Far,

            Close,

            Near
        }

        private readonly Unit9 unit;

        private DistanceState state;

        public AwarenessHero(Unit9 unit)
        {
            this.unit = unit;
            this.Handle = unit.Handle;
            this.TextureName = unit.Name + "_icon";
        }

        public uint Handle { get; }

        public bool IsValid
        {
            get
            {
                return this.unit.IsValid;
            }
        }

        public string OutlineTextureName
        {
            get
            {
                switch (this.state)
                {
                    case DistanceState.Far:
                        return "o9k.outline_green";
                    case DistanceState.Close:
                        return "o9k.outline_yellow";
                    case DistanceState.Near:
                        return "o9k.outline_red";
                }

                return null;
            }
        }

        public Vector3 Position
        {
            get
            {
                return this.unit.BasePosition;
            }
        }

        public string TextureName { get; }

        private float UnitSpeed
        {
            get
            {
                var speed = this.unit.Speed;
                var abilities = this.unit.Abilities.ToArray();

                if (abilities.Any(x => x is IBlink))
                {
                    speed *= 1.1f;
                }

                if (abilities.Any(x => x is ISpeedBuff))
                {
                    speed *= 1.1f;
                }

                if (abilities.Any(x => x.Id == AbilityId.weaver_shukuchi || x.Id == AbilityId.clinkz_wind_walk))
                {
                    speed *= 1.3f;
                }

                if (abilities.Any(x => x.Id == AbilityId.shredder_timber_chain))
                {
                    speed *= 1.3f;
                }

                if (abilities.Any(x => x.Id == AbilityId.storm_spirit_ball_lightning))
                {
                    speed *= 1.2f;
                }

                if (this.unit.HealthPercentage < 20)
                {
                    speed *= 0.5f;
                }

                return speed;
            }
        }

        public void Update(Hero9 myHero)
        {
            if (!this.unit.IsAlive || !myHero.IsAlive)
            {
                this.state = DistanceState.Far;
                return;
            }

            var heroPosition = myHero.Position;
            var enemyPosition = this.unit.BasePosition;
            var distance = heroPosition.Distance2D(enemyPosition);

            if (!this.unit.IsVisible)
            {
                var speed = this.UnitSpeed;
                if (speed <= 0)
                {
                    return;
                }

                var maxTime = distance / speed;
                var invisTime = Math.Min(maxTime, Game.RawGameTime - this.unit.LastPositionUpdateTime);
                var predictedPosition = enemyPosition.Extend2D(heroPosition, invisTime * speed);

                distance = heroPosition.Distance2D(predictedPosition);
            }

            if (distance < 2000)
            {
                this.state = DistanceState.Near;
            }
            else if (distance < 3500)
            {
                this.state = DistanceState.Close;
            }
            else
            {
                this.state = DistanceState.Far;
            }
        }
    }
}