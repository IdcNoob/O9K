namespace O9K.Core.Plugins.OrderBlocker.Modules
{
    using Ensage;

    using Helpers;

    using Managers.Entity;

    internal class BlockTooFastReaction
    {
        private readonly OrderBlockerMenu menu;

        public BlockTooFastReaction(OrderBlockerMenu menu)
        {
            this.menu = menu;
        }

        public bool ShouldBlock(ExecuteOrderEventArgs args)
        {
            if (!this.menu.BlockTooFastReaction)
            {
                return false;
            }

            switch (args.OrderId)
            {
                case OrderId.AttackTarget:
                case OrderId.AbilityTarget:
                case OrderId.MoveTarget:
                {
                    var target = EntityManager9.GetUnitFast(args.Target.Handle);
                    if (target == null)
                    {
                        return false;
                    }

                    if (Game.RawGameTime - target.LastNotVisibleTime < RNG.Randomize(0.25f, 0.03f))
                    {
                        return true;
                    }

                    break;
                }
            }

            return false;
        }
    }
}