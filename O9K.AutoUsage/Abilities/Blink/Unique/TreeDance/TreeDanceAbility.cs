namespace O9K.AutoUsage.Abilities.Blink.Unique.TreeDance
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Settings;

    [AbilityId(AbilityId.monkey_king_tree_dance)]
    internal class TreeDanceAbility : BlinkAbility
    {
        private readonly BlinkSettings settings;

        public TreeDanceAbility(IBlink blink, GroupSettings settings)
            : base(blink)
        {
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

            if (this.Owner.HasModifier("modifier_monkey_king_tree_dance_hidden"))
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

                var tree = EntityManager9.Trees.Where(x => x.Distance2D(this.Owner.Position) < this.Ability.CastRange)
                    .OrderBy(x => x.Distance2D(this.Fountain))
                    .FirstOrDefault();

                if (tree == null)
                {
                    return false;
                }

                return this.Ability.UseAbility(tree);
            }

            return false;
        }
    }
}