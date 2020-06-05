namespace O9K.ItemManager.Modules.AutoActions
{
    using System;
    using System.ComponentModel.Composition;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Helpers;

    using Metadata;

    internal class BlinkRangeChange : IModule
    {
        private readonly MenuSwitcher enabled;

        [ImportingConstructor]
        public BlinkRangeChange(IMainMenu mainMenu)
        {
            var menu = mainMenu.AutoActionsMenu.Add(new Menu(LocalizationHelper.LocalizeName(AbilityId.item_blink), "BlinkDagger"));

            this.enabled = menu.Add(new MenuSwitcher("Maximize blink range"));
            this.enabled.AddTranslation(Lang.Ru, "Максимизировать дальность блинка");
            this.enabled.AddTranslation(Lang.Cn, "最大化眨眼范围");
        }

        public void Activate()
        {
            this.enabled.ValueChange += this.OnValueChange;
        }

        public void Dispose()
        {
            this.enabled.ValueChange -= this.OnValueChange;
            Player.OnExecuteOrder -= this.OnExecuteOrder;
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (!args.IsPlayerInput || !args.Process || args.IsQueued)
                {
                    return;
                }

                if (args.OrderId != OrderId.AbilityLocation || args.Ability.Id != AbilityId.item_blink)
                {
                    return;
                }

                var blink = (ActiveAbility)EntityManager9.GetAbility(args.Ability.Handle);
                var hero = blink.Owner;

                if (hero.IsChanneling)
                {
                    return;
                }

                var blinkRange = blink.Range;
                var blinkPosition = args.TargetPosition;
                var heroPosition = hero.Position;
                if (heroPosition.Distance2D(blinkPosition) < blinkRange)
                {
                    return;
                }

                var newBlinkPosition = heroPosition.Extend2D(blinkPosition, blinkRange - 50);
                if (!Hud.IsPositionOnScreen(newBlinkPosition))
                {
                    return;
                }

                blink.UseAbility(newBlinkPosition);
                args.Process = false;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                Player.OnExecuteOrder += this.OnExecuteOrder;
            }
            else
            {
                Player.OnExecuteOrder -= this.OnExecuteOrder;
            }
        }
    }
}