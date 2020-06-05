namespace O9K.ItemManager.Modules.Hotkeys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Core.Entities.Heroes;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Metadata;

    internal class Purchase : IModule
    {
        private readonly string[] items =
        {
            nameof(AbilityId.item_tango),
            nameof(AbilityId.item_clarity),
            nameof(AbilityId.item_flask),
            nameof(AbilityId.item_enchanted_mango),
            nameof(AbilityId.item_tpscroll),
            nameof(AbilityId.item_tome_of_knowledge),
            nameof(AbilityId.item_ward_observer),
            nameof(AbilityId.item_ward_sentry),
        };

        private readonly List<MenuHoldKey> keys = new List<MenuHoldKey>();

        private Owner owner;

        [ImportingConstructor]
        public Purchase(IMainMenu mainMenu)
        {
            var menu = mainMenu.Hotkeys.Add(new Menu("Purchase"));
            menu.AddTranslation(Lang.Ru, "Покупка");
            menu.AddTranslation(Lang.Cn, "采购");

            foreach (var item in this.items)
            {
                this.keys.Add(menu.Add(new MenuHoldKey(LocalizationHelper.LocalizeAbilityName(item), item)));
            }
        }

        public void Activate()
        {
            this.owner = EntityManager9.Owner;

            foreach (var key in this.keys)
            {
                key.ValueChange += this.KeyOnValueChange;
            }
        }

        public void Dispose()
        {
            foreach (var key in this.keys)
            {
                key.ValueChange -= this.KeyOnValueChange;
            }
        }

        private void KeyOnValueChange(object sender, KeyEventArgs args)
        {
            try
            {
                if (!args.NewValue || this.owner.Hero == null)
                {
                    return;
                }

                var menuItem = (MenuHoldKey)sender;
                var itemData = Ability.GetAbilityDataByName(menuItem.Name);

                if (this.owner.Player.ReliableGold + this.owner.Player.UnreliableGold < itemData.Cost)
                {
                    return;
                }

                Player.BuyItem(this.owner, itemData.Id);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}