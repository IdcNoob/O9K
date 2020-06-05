namespace O9K.Core.Plugins.OrderBlocker.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;

    using Helpers;

    using Logger;

    using Managers.Entity;

    using SharpDX;

    internal class OutOfScreenBlock
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

        public OutOfScreenBlock(OrderBlockerMenu menu)
        {
            this.menu = menu;
        }

        public bool ShouldBlock(ExecuteOrderEventArgs args)
        {
            if (!this.menu.OfScreenBlock)
            {
                return false;
            }

            if (args.Ability != null && this.ignoredAbilities.Contains(args.Ability.Id))
            {
                return false;
            }

            Vector3 position;

            switch (args.OrderId)
            {
                case OrderId.MoveLocation:
                case OrderId.AttackLocation:
                case OrderId.AbilityLocation:
                case OrderId.DropItem:
                case OrderId.MoveToDirection:
                {
                    position = args.TargetPosition;
                    break;
                }
                case OrderId.AttackTarget:
                case OrderId.AbilityTarget:
                case OrderId.ConsumeRune:
                case OrderId.PickItem:
                case OrderId.MoveTarget:
                case OrderId.AbilityTargetRune:
                {
                    position = args.Target.Position;
                    break;
                }
                case OrderId.AbilityTargetTree:
                {
                    try
                    {
                        position = EntityManager9.Trees.First(x => x.TargetId == args.TargetId).Position;
                    }
                    catch (Exception e)
                    {
                        var tree = ObjectManager.GetEntities<Tree>().Any(x => x.TargetId == args.TargetId);
                        Logger.Error(e, "default: " + tree);
                        return false;
                    }

                    break;
                }
                default:
                    return false;
            }

            if (Hud.IsPositionOnScreen(position))
            {
                return false;
            }

            if (this.menu.MoveCamera)
            {
                Hud.CameraPosition = position;
            }

            return true;
        }
    }
}