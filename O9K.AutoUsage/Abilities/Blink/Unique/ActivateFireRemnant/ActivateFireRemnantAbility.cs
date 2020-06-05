namespace O9K.AutoUsage.Abilities.Blink.Unique.ActivateFireRemnant
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Ensage;

    using Settings;

    [AbilityId(AbilityId.ember_spirit_activate_fire_remnant)]
    internal class ActivateFireRemnantAbility : BlinkAbility
    {
        private readonly BlinkSettings settings;

        public ActivateFireRemnantAbility(IBlink blink, GroupSettings settings)
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

            var remnant = EntityManager9.Units.FirstOrDefault(x => x.IsAlly(this.Owner) && x.Name == "npc_dota_ember_spirit_remnant");
            if (remnant == null || this.Owner.Distance(remnant) < 500)
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

                var position = this.Owner.InFront(100);
                return this.Ability.UseAbility(position);
            }

            return false;
        }
    }
}