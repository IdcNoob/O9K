namespace O9K.AIO.Heroes.Dynamic.Abilities.Specials.Unique
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Logger;

    using Ensage;

    [AbilityId(AbilityId.item_hurricane_pike)]
    internal class HurricanePikeSpecial : OldSpecialAbility, IDisposable
    {
        private readonly List<AbilityId> enabledOrbs = new List<AbilityId>();

        public HurricanePikeSpecial(IActiveAbility ability)
            : base(ability)
        {
        }

        public void Dispose()
        {
            Unit.OnModifierAdded -= this.OnModifierAdded;
            Unit.OnModifierRemoved -= this.OnModifierRemoved;
        }

        public override bool ShouldCast(Unit9 target)
        {
            if (target.IsLinkensProtected || target.IsSpellShieldProtected)
            {
                return false;
            }

            if (this.Ability.Owner.Distance(target) < target.GetAttackRange() + 100)
            {
                return true;
            }

            return false;
        }

        public override bool Use(Unit9 target)
        {
            if (!this.Ability.UseAbility(target))
            {
                return false;
            }

            Unit.OnModifierAdded += this.OnModifierAdded;

            this.OrbwalkSleeper.Sleep(this.Ability.Owner.Handle, this.Ability.GetCastDelay(target));
            this.AbilitySleeper.Sleep(this.Ability.Handle, this.Ability.GetHitTime(target) + 0.5f);

            return true;
        }

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Handle != this.Ability.Owner.Handle || args.Modifier.Name != "modifier_item_hurricane_pike_range")
                {
                    return;
                }

                foreach (var orb in this.Ability.Owner.Abilities.OfType<OrbAbility>())
                {
                    if (orb.Enabled || !orb.CanBeCasted())
                    {
                        continue;
                    }

                    this.enabledOrbs.Add(orb.Id);
                    orb.Enabled = true;
                }

                Unit.OnModifierRemoved += this.OnModifierRemoved;
                Unit.OnModifierAdded -= this.OnModifierAdded;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                Unit.OnModifierAdded -= this.OnModifierAdded;
            }
        }

        private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Handle != this.Ability.Owner.Handle || args.Modifier.Name != "modifier_item_hurricane_pike_range")
                {
                    return;
                }

                foreach (var orb in this.Ability.Owner.Abilities.OfType<OrbAbility>().Where(x => this.enabledOrbs.Contains(x.Id)))
                {
                    orb.Enabled = false;
                }

                Unit.OnModifierRemoved -= this.OnModifierRemoved;
            }
            catch (Exception e)
            {
                Unit.OnModifierRemoved -= this.OnModifierRemoved;
                Logger.Error(e);
            }
        }
    }
}