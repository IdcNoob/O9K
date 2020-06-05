namespace O9K.AutoUsage.Abilities.LinkensBreak
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Entities.Units;

    using Settings;

    internal class LinkensBreakAbility : UsableAbility
    {
        private readonly ActiveAbility activeAbility;

        private readonly LinkensBreakSettings settings;

        public LinkensBreakAbility(IActiveAbility ability, GroupSettings settings)
            : base(ability)
        {
            this.settings = new LinkensBreakSettings(settings.Menu, ability);
            this.activeAbility = (ActiveAbility)ability;
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            var enemies = heroes.Where(x => !x.IsInvulnerable && x.IsEnemy(this.Owner));

            foreach (var enemy in enemies)
            {
                if (!this.settings.IsHeroEnabled(enemy.Name))
                {
                    continue;
                }

                if (enemy.IsSpellShieldProtected && !this.settings.BreakSpellShield)
                {
                    continue;
                }

                if (!enemy.IsLinkensProtected && (!this.settings.BreakSpellShield || !enemy.IsSpellShieldProtected))
                {
                    continue;
                }

                if (!this.Ability.CanHit(enemy))
                {
                    continue;
                }

                if (this.settings.MaxCastRange > 0 && enemy.Distance(this.Owner) > this.settings.MaxCastRange)
                {
                    continue;
                }

                if (!this.activeAbility.BreaksLinkens || enemy.IsLotusProtected)
                {
                    continue;
                }

                return this.Ability.UseAbility(enemy);
            }

            return false;
        }
    }
}