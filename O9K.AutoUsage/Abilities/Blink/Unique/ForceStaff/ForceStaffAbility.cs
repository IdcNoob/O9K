namespace O9K.AutoUsage.Abilities.Blink.Unique.ForceStaff
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Settings;

    [AbilityId(AbilityId.item_force_staff)]
    [AbilityId(AbilityId.item_hurricane_pike)]
    internal class ForceStaffAbility : BlinkAbility
    {
        private readonly ForceStaffSettings settings;

        public ForceStaffAbility(IBlink blink, GroupSettings settings)
            : base(blink)
        {
            this.settings = new ForceStaffSettings(settings.Menu, blink);
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

                if (this.settings.UseOnEnemy)
                {
                    if (!this.Ability.CanHit(enemy) || enemy.IsLinkensProtected)
                    {
                        return false;
                    }

                    return this.Ability.UseAbility(enemy);
                }
                else
                {
                    var move = this.Owner.GetAngle(this.Fountain) > 0.4;

                    if (move)
                    {
                        var position = this.Owner.Position.Extend(this.Fountain, 50);
                        this.Owner.BaseUnit.Move(position);
                    }

                    return this.Ability.UseAbility(this.Owner, move);
                }
            }

            return false;
        }
    }
}