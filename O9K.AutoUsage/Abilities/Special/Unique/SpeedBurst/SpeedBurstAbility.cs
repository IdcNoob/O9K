namespace O9K.AutoUsage.Abilities.Special.Unique.SpeedBurst
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Metadata;
    using Core.Logger;

    using Ensage;
    using Ensage.SDK.Helpers;

    [AbilityId(AbilityId.courier_burst)]
    internal class SpeedBurstAbility
    {
        private readonly HashSet<AbilityId> ids = new HashSet<AbilityId>
        {
            AbilityId.courier_transfer_items,
            AbilityId.courier_take_stash_and_transfer_items,
            AbilityId.courier_go_to_secretshop,
        };

        public void Activate()
        {
            Player.OnExecuteOrder += this.OnExecuteOrder;
        }

        public void Deactivate()
        {
            Player.OnExecuteOrder -= this.OnExecuteOrder;
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (!args.Process || args.OrderId != OrderId.Ability)
                {
                    return;
                }

                if (!this.ids.Contains(args.Ability.Id))
                {
                    return;
                }

                if (!(args.Entities.FirstOrDefault() is Courier courier))
                {
                    return;
                }

                var burst = courier.Spellbook.Spells.FirstOrDefault(x => x.Id == AbilityId.courier_burst);
                if (burst == null || burst.Level == 0 || burst.Cooldown > 0)
                {
                    return;
                }

                UpdateManager.BeginInvoke(() => Game.ExecuteCommand("dota_courier_burst"), 200);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}