namespace O9K.Core.Plugins.OrderBlocker
{
    using System;
    using System.ComponentModel.Composition;

    using DebugDraw;

    using Ensage;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    using Logger;

    using Managers.Context;
    using Managers.Menu;

    using Modules;

    [ExportPlugin("Order blocker", priority: int.MaxValue - 10)]
    internal class OrderBlocker : Plugin
    {
        private readonly IContext9 context;

        private readonly IMenuManager9 menuManager;

        private BlockTooFastReaction blockTooFastReaction;

        private DrawBlockInfo drawInfo;

        private OrderBlockerMenu menu;

        private NotSelectedBlock notSelectedBlock;

        private OutOfScreenBlock oosBlock;

        private SlowDown slowDown;

        private SpamBlock spamBlock;

        private ZoomBlock zoomBlock;

        [ImportingConstructor]
        public OrderBlocker(IContext9 context, IMenuManager9 menuManager)
        {
            this.context = context;
            this.menuManager = menuManager;
        }

        protected override void OnActivate()
        {
            this.menu = new OrderBlockerMenu(this.context, this.menuManager, this.context.AssemblyEventManager);
            this.drawInfo = new DrawBlockInfo(this.context.Renderer, this.menu);
            this.oosBlock = new OutOfScreenBlock(this.menu);
            this.zoomBlock = new ZoomBlock(this.menu);
            this.spamBlock = new SpamBlock(this.menu);
            this.notSelectedBlock = new NotSelectedBlock(this.menu);
            this.slowDown = new SlowDown(this.menu);
            this.blockTooFastReaction = new BlockTooFastReaction(this.menu);

            Player.OnExecuteOrder += this.OnExecuteOrder;
            Player.OnExecuteOrder += this.OnExecuteOrderAPM;
            this.context.AssemblyEventManager.ForceBlockerResubscribe += this.ForceBlockerResubscribe;
        }

        protected override void OnDeactivate()
        {
            this.context.AssemblyEventManager.ForceBlockerResubscribe -= this.ForceBlockerResubscribe;
            Player.OnExecuteOrder -= this.OnExecuteOrder;
            Player.OnExecuteOrder -= this.OnExecuteOrderAPM;

            this.zoomBlock.Dispose();
            this.drawInfo.Dispose();
            this.menu.Dispose();
        }

        private void ForceBlockerResubscribe(object sender, EventArgs e)
        {
            Player.OnExecuteOrder -= this.OnExecuteOrder;
            Player.OnExecuteOrder += this.OnExecuteOrder;

            Player.OnExecuteOrder -= this.OnExecuteOrderAPM;
            Player.OnExecuteOrder += this.OnExecuteOrderAPM;
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (!args.Process || args.IsPlayerInput)
                {
                    return;
                }

                if (this.notSelectedBlock.ShouldBlock(args))
                {
                    args.Process = false;
                    this.drawInfo.Blocked(args, "Not selected");
                    return;
                }

                if (this.blockTooFastReaction.ShouldBlock(args))
                {
                    args.Process = false;
                    this.drawInfo.Blocked(args, "Too fast");
                    return;
                }

                if (this.oosBlock.ShouldBlock(args))
                {
                    args.Process = false;
                    this.drawInfo.Blocked(args, "Off screen");
                    return;
                }

                if (this.slowDown.ShouldBlock(args))
                {
                    args.Process = false;
                    this.drawInfo.Blocked(args, "Slow down");
                    return;
                }

                if (this.spamBlock.ShouldBlock(args))
                {
                    args.Process = false;
                    this.drawInfo.Blocked(args, "Spam");
                    return;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnExecuteOrderAPM(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (!args.Process)
                {
                    return;
                }

                this.drawInfo.IncreaseAPM(args);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}