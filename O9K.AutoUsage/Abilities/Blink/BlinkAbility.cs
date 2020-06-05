namespace O9K.AutoUsage.Abilities.Blink
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Ensage.SDK.Extensions;

    using Settings;

    using SharpDX;

    internal class BlinkAbility : UsableAbility
    {
        private readonly BlinkSettings settings;

        public BlinkAbility(IBlink blink, GroupSettings settings)
            : base(blink)
        {
            this.Blink = blink;
            this.settings = new BlinkSettings(settings.Menu, blink);
        }

        protected BlinkAbility(IBlink blink)
            : base(blink)
        {
            this.Blink = blink;
        }

        protected IBlink Blink { get; }

        protected Vector3 Fountain
        {
            get
            {
                return EntityManager9.Units.First(x => x.IsFountain && x.IsAlly(this.Owner)).Position;
            }
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            if (!this.settings.UseWhenMagicImmune && this.Owner.IsMagicImmune)
            {
                return false;
            }

            if (this.Owner.HealthPercentage > this.settings.HpThreshold)
            {
                return false;
            }

            var enemies = heroes.Where(x => x.IsEnemy(this.Owner)).ToList();
            var closeEnemies = enemies.Count(x => x.Distance(this.Owner) < this.settings.Distance);

            foreach (var enemy in enemies)
            {
                if (!this.settings.IsHeroEnabled(enemy.Name))
                {
                    continue;
                }

                if (this.Owner.Distance(enemy) > this.settings.Distance)
                {
                    continue;
                }

                if (closeEnemies < this.settings.EnemiesCount)
                {
                    continue;
                }

                switch (this.Blink.BlinkType)
                {
                    case BlinkType.Blink:
                    {
                        var position = this.Owner.Position.Extend(this.Fountain, this.Ability.CastRange);
                        return this.Ability.UseAbility(position);
                    }
                    case BlinkType.Leap:
                    {
                        var move = this.Owner.GetAngle(this.Fountain) > 0.4;

                        if (move)
                        {
                            var position = this.Owner.Position.Extend(this.Fountain, 50);
                            this.Owner.BaseUnit.Move(position);
                        }

                        return this.Ability.UseAbility(this.Owner, move);
                    }
                    case BlinkType.Targetable:
                    {
                        var target = EntityManager9.Units
                            .Where(
                                x => x.IsUnit && !x.Equals(this.Owner) && x.IsAlly(this.Owner) && x.IsAlive && !x.IsInvulnerable
                                     && !x.IsMagicImmune && x.Distance(this.Owner) < this.Ability.CastRange)
                            .OrderBy(x => x.Distance(this.Fountain))
                            .FirstOrDefault(x => x.Distance(this.Owner) > 400);

                        if (target == null)
                        {
                            return false;
                        }

                        return this.Ability.UseAbility(target);
                    }
                }
            }

            return false;
        }
    }
}