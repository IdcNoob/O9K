namespace O9K.AutoUsage.Abilities.Buff.Unique.PhaseBoots
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Logger;

    using Ensage;

    using Settings;

    [AbilityId(AbilityId.item_phase_boots)]
    internal class PhaseBootsAbility : BuffAbility, IDisposable
    {
        private readonly PhaseBootsSettings settings;

        public PhaseBootsAbility(IBuff ability, GroupSettings settings)
            : base(ability)
        {
            this.settings = new PhaseBootsSettings(settings.Menu, ability);
        }

        public void Dispose()
        {
            Player.OnExecuteOrder -= this.OnExecuteOrder;
        }

        public override void Enabled(bool enabled)
        {
            base.Enabled(enabled);

            if (enabled)
            {
                Player.OnExecuteOrder += this.OnExecuteOrder;
            }
            else
            {
                Player.OnExecuteOrder -= this.OnExecuteOrder;
            }
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            return false;
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (args.IsQueued || !args.Process || !this.Ability.CanBeCasted())
                {
                    return;
                }

                if (args.Entities.All(x => x.Handle != this.OwnerHandle))
                {
                    return;
                }

                if (!this.Owner.CanMove() || (!this.Owner.CanUseAbilitiesInInvisibility && this.Owner.IsInvisible))
                {
                    return;
                }

                switch (args.OrderId)
                {
                    case OrderId.AttackLocation:
                    case OrderId.AttackTarget:
                    {
                        var location = args.Target?.Position ?? args.TargetPosition;
                        if (this.Owner.Distance(location) - this.Owner.GetAttackRange() >= this.settings.Distance)
                        {
                            this.Ability.UseAbility();
                        }

                        break;
                    }
                    case OrderId.MoveTarget:
                    case OrderId.MoveLocation:
                    {
                        var location = args.Target?.Position ?? args.TargetPosition;
                        if (this.Owner.Distance(location) >= this.settings.Distance)
                        {
                            this.Ability.UseAbility();
                        }

                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}