namespace O9K.Core.Plugins.OrderBlocker.Modules
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;

    using Entities.Heroes;

    using Helpers;

    using Managers.Entity;

    internal class NotSelectedBlock
    {
        private readonly HashSet<AbilityId> ignoredAbilities = new HashSet<AbilityId>
        {
            AbilityId.courier_autodeliver,
            AbilityId.courier_return_to_base,
            AbilityId.courier_transfer_items,
            AbilityId.courier_return_stash_items,
            AbilityId.courier_take_stash_items,
            AbilityId.courier_shield,
            AbilityId.courier_burst,
            AbilityId.courier_go_to_secretshop,
            AbilityId.courier_take_stash_and_transfer_items,
            AbilityId.courier_transfer_items_to_other_player,
            AbilityId.courier_go_to_enemy_secretshop,
            AbilityId.courier_go_to_sideshop,
            AbilityId.courier_go_to_sideshop2,
            AbilityId.courier_queue_pickup_from_stash,
            AbilityId.courier_dequeue_pickup_from_stash,
        };

        private readonly OrderBlockerMenu menu;

        private readonly HashSet<OrderId> orders = new HashSet<OrderId>
        {
            OrderId.MoveToDirection,
            OrderId.MoveLocation,
            OrderId.AttackLocation,
            OrderId.AttackTarget,
            OrderId.MoveTarget,
            OrderId.ConsumeRune,
            OrderId.Ability,
            OrderId.AbilityTarget,
            OrderId.AbilityLocation,
            OrderId.AbilityTargetRune,
            OrderId.AbilityTargetTree,
            OrderId.ToggleAbility,
            OrderId.DropItem,
            OrderId.PickItem,
        };

        private readonly Owner owner;

        private readonly Sleeper sleeper = new Sleeper();

        public NotSelectedBlock(OrderBlockerMenu menu)
        {
            this.menu = menu;
            this.owner = EntityManager9.Owner;
        }

        public bool ShouldBlock(ExecuteOrderEventArgs args)
        {
            if (!this.menu.BlockNotSelectUnits || !this.orders.Contains(args.OrderId))
            {
                return false;
            }

            if (args.Ability != null && this.ignoredAbilities.Contains(args.Ability.Id))
            {
                return false;
            }

            var ordered = args.Entities.ToArray();
            var selected = this.owner.Player.Selection.ToArray();

            if (ordered.Length == 1)
            {
                if (selected[0] != ordered[0])
                {
                    if (this.menu.SelectUnits && !this.sleeper)
                    {
                        this.sleeper.Sleep(0.15f);
                        ordered[0].Select();
                    }

                    return true;
                }
            }
            else
            {
                if (selected.Length != ordered.Length || selected.Any(x => !ordered.Contains(x)))
                {
                    if (this.menu.SelectUnits && !this.sleeper)
                    {
                        this.sleeper.Sleep(0.15f);

                        for (var i = 0; i < ordered.Length; i++)
                        {
                            ordered[i].Select(i != 0);
                        }
                    }

                    return true;
                }
            }

            return false;
        }
    }
}