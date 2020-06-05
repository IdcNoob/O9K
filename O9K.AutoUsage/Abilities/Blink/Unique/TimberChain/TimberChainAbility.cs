namespace O9K.AutoUsage.Abilities.Blink.Unique.TimberChain
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Abilities.Heroes.Timbersaw;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;

    using Settings;

    [AbilityId(AbilityId.shredder_timber_chain)]
    internal class TimberChainAbility : BlinkAbility
    {
        private readonly BlinkSettings settings;

        private readonly TimberChain timberChain;

        public TimberChainAbility(IBlink blink, GroupSettings settings)
            : base(blink)
        {
            this.timberChain = (TimberChain)blink;
            this.settings = new BlinkSettings(settings.Menu, blink);
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

                var blinkLocation = this.Owner.Position.Extend2D(this.Fountain, this.Ability.CastRange);
                var castRange = this.Ability.CastRange;

                foreach (var tree in EntityManager9.Trees
                    .Where(
                        x => x.Distance2D(blinkLocation) < castRange && x.Distance2D(this.Owner.Position) < castRange
                                                                     && x.Distance2D(this.Owner.Position) > 500)
                    .OrderBy(x => x.Distance2D(this.Fountain)))
                {
                    var rec = new Polygon.Rectangle(this.Owner.Position, tree.Position, this.timberChain.ChainRadius);

                    if (EntityManager9.Trees.Any(x => rec.IsInside(x.Position)))
                    {
                        continue;
                    }

                    return this.Ability.UseAbility(tree.Position);
                }
            }

            return false;
        }
    }
}