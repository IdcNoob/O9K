namespace O9K.AutoUsage.Abilities.ManaRestore
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Prediction.Data;

    using Settings;

    internal class ManaRestoreAbility : UsableAbility
    {
        private readonly ManaRestoreSettings settings;

        public ManaRestoreAbility(IManaRestore manaRestore, GroupSettings settings)
            : base(manaRestore)
        {
            this.ManaRestore = manaRestore;
            this.settings = new ManaRestoreSettings(settings.Menu, manaRestore);
        }

        protected ManaRestoreAbility(IManaRestore manaRestore)
            : base(manaRestore)
        {
            this.ManaRestore = manaRestore;
        }

        protected IManaRestore ManaRestore { get; }

        public override bool UseAbility(List<Unit9> heroes)
        {
            var allies = heroes.Where(x => !x.IsInvulnerable && x.IsAlly(this.Owner)).OrderBy(x => x.Mana).ToList();

            foreach (var ally in allies)
            {
                if (!this.settings.IsHeroEnabled(ally.Name) && !this.settings.SelfOnly)
                {
                    continue;
                }

                var selfTarget = ally.Equals(this.Owner);

                if (selfTarget && !this.ManaRestore.RestoresOwner)
                {
                    continue;
                }

                if (!selfTarget && (!this.ManaRestore.RestoresAlly || this.settings.SelfOnly))
                {
                    continue;
                }

                if (!this.Ability.CanHit(ally, allies, this.settings.AlliesCount))
                {
                    continue;
                }

                if (ally.ManaPercentage > this.settings.MpThreshold)
                {
                    continue;
                }

                if (this.Owner.HealthPercentage < this.settings.HpThreshold)
                {
                    continue;
                }

                return this.Ability.UseAbility(ally, allies, HitChance.Medium, this.settings.AlliesCount);
            }

            return false;
        }
    }
}