namespace O9K.ItemManager.Utils
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base;

    using Ensage;

    using Enums;

    internal static class ItemUtils
    {
        private static readonly HashSet<string> BonusAllStats = new HashSet<string>
        {
            "bonus_all_stats",
            "bonus_stats"
        };

        private static readonly HashSet<string> BonusHealth = new HashSet<string>
        {
            "bonus_strength",
            "bonus_str",
            "bonus_health"
        };

        private static readonly HashSet<string> BonusMana = new HashSet<string>
        {
            "bonus_intellect",
            "bonus_intelligence",
            "bonus_int",
            "bonus_mana"
        };

        private static readonly Dictionary<AbilityId, int> ItemPrice = new Dictionary<AbilityId, int>();

        private static readonly Dictionary<AbilityId, ShopFlags> ItemShopFlags = new Dictionary<AbilityId, ShopFlags>();

        private static readonly Dictionary<AbilityId, Stats> ItemStats = new Dictionary<AbilityId, Stats>();

        private static readonly Dictionary<AbilityId, bool> Recipes = new Dictionary<AbilityId, bool>();

        public static bool CanBePurchased(this AbilityId itemId, Team team)
        {
            var itemStockInfo = Game.StockInfo.FirstOrDefault(x => x.AbilityId == itemId && x.Team == team);
            if (itemStockInfo != null && itemStockInfo.StockCount <= 0)
            {
                return false;
            }

            return true;
        }

        public static ItemSlot GetItemSlot(this Ability9 ability)
        {
            var hero = ability.Owner.BaseUnit;

            for (var i = ItemSlot.InventorySlot_1; i <= ItemSlot.StashSlot_6; i++)
            {
                var inventoryItem = hero.Inventory.GetItem(i);
                if (inventoryItem?.Handle != ability.Handle)
                {
                    continue;
                }

                return i;
            }

            var neutralItem = hero.Inventory.GetItem(ItemSlot.NeutralItemSlot);
            if (neutralItem?.Handle == ability.Handle)
            {
                return ItemSlot.NeutralItemSlot;
            }

            return (ItemSlot)(-1);
        }

        public static int GetPrice(this AbilityId itemId)
        {
            if (ItemPrice.TryGetValue(itemId, out var price))
            {
                return price;
            }

            try
            {
                price = Game.FindKeyValues(itemId + "/ItemCost", KeyValueSource.Ability).IntValue;
            }
            catch (KeyValuesNotFoundException)
            {
                price = 0;
            }

            ItemPrice[itemId] = price;
            return price;
        }

        public static ShopFlags GetShopFlags(this AbilityId itemId)
        {
            if (ItemShopFlags.TryGetValue(itemId, out var flags))
            {
                return flags;
            }

            var itemName = itemId.ToString();

            if (HasGlobalTag(itemName))
            {
                flags |= ShopFlags.Base;
            }

            if (HasSecretShopFlag(itemName))
            {
                flags |= ShopFlags.Secret;
            }

            if (HasSideShopFlag(itemName))
            {
                flags |= ShopFlags.Side;

                if ((flags & ShopFlags.Secret) == 0)
                {
                    flags |= ShopFlags.Base;
                }
            }

            if (flags == ShopFlags.None)
            {
                flags = ShopFlags.Base;
            }

            ItemShopFlags[itemId] = flags;
            return flags;
        }

        public static bool HasHealthStats(this AbilityId itemId)
        {
            return (GetItemStats(itemId) & Stats.Health) != 0;
        }

        public static bool HasManaStats(this AbilityId itemId)
        {
            return (GetItemStats(itemId) & Stats.Mana) != 0;
        }

        public static bool IsRecipe(this AbilityId itemId)
        {
            if (Recipes.TryGetValue(itemId, out var value))
            {
                return value;
            }

            value = itemId.ToString().Contains("recipe");
            Recipes[itemId] = value;

            return value;
        }

        private static Stats GetItemStats(AbilityId itemId)
        {
            if (ItemStats.TryGetValue(itemId, out var stats))
            {
                return stats;
            }

            var data = Ability.GetAbilityDataById(itemId).AbilitySpecialData.ToList();

            if (data.Any(x => BonusAllStats.Contains(x.Name)))
            {
                stats = Stats.All;
            }
            else
            {
                if (data.Any(x => BonusHealth.Contains(x.Name)))
                {
                    stats |= Stats.Health;
                }

                if (data.Any(x => BonusMana.Contains(x.Name)))
                {
                    stats |= Stats.Mana;
                }
            }

            ItemStats[itemId] = stats;
            return stats;
        }

        private static bool HasGlobalTag(string name)
        {
            try
            {
                Game.FindKeyValues(name + "/GlobalShop", KeyValueSource.Ability);
                return true;
            }
            catch (KeyValuesNotFoundException)
            {
                return false;
            }
        }

        private static bool HasSecretShopFlag(string name)
        {
            try
            {
                Game.FindKeyValues(name + "/SecretShop", KeyValueSource.Ability);
                return true;
            }
            catch (KeyValuesNotFoundException)
            {
                return false;
            }
        }

        private static bool HasSideShopFlag(string name)
        {
            try
            {
                Game.FindKeyValues(name + "/SideShop", KeyValueSource.Ability);
                return true;
            }
            catch (KeyValuesNotFoundException)
            {
                return false;
            }
        }
    }
}