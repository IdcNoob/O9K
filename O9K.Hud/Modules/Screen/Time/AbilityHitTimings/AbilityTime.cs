namespace O9K.Hud.Modules.Screen.Time.AbilityHitTimings
{
    using Core.Entities.Abilities.Base;
    using Core.Extensions;

    using Ensage.SDK.Geometry;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal class AbilityTime
    {
        private readonly PredictionAbility ability;

        public AbilityTime(PredictionAbility ability)
        {
            this.ability = ability;
            this.Handle = ability.Handle;
            this.Name = ability.Name;
        }

        public Color Color { get; private set; }

        public bool Display { get; set; }

        public uint Handle { get; }

        public string Name { get; }

        public string Time { get; private set; } = string.Empty;

        public void UpdateTime(Vector3 position)
        {
            if (this.ability.Owner.Distance(position) > this.ability.CastRange)
            {
                var speed = this.ability.Owner.Speed;
                if (speed <= 0)
                {
                    this.Time = "?";
                    this.Color = Color.Red;
                    return;
                }

                var maxCastPosition = this.ability.Owner.Position.Extend2D(position, this.ability.CastRange);
                var moveTime = maxCastPosition.Distance2D(position) / speed;
                var time = this.ability.GetHitTime(maxCastPosition) + moveTime;

                this.Time = time.ToString("N2") + "s";
                this.Color = Color.Orange;
            }
            else
            {
                this.Time = this.ability.GetHitTime(position).ToString("N2") + "s";
                this.Color = Color.White;
            }
        }
    }
}