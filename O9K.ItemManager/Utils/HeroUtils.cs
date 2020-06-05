namespace O9K.ItemManager.Utils
{
    using System;
    using System.Linq;

    using Core.Entities.Heroes;

    using Ensage;
    using Ensage.Items;

    internal static class HeroUtils
    {
        public static bool CanBeMovedToBackpack(this Item item)
        {
            switch (item.Id)
            {
                case AbilityId.item_gem:
                case AbilityId.item_rapier:
                case AbilityId.item_bloodstone:
                {
                    return false;
                }
                case AbilityId.item_bottle:
                {
                    return ((Bottle)item).StoredRune == RuneType.None;
                }
                default:
                {
                    return item.IsDroppable;
                }
            }
        }

        public static (int Reliable, int Unreliable) GetAvailableGold(this Owner owner, float saveForBuyback)
        {
            var reliable = owner.Player.ReliableGold;
            var unreliable = owner.Player.UnreliableGold;

            if (Game.GameTime / 60f >= saveForBuyback)
            {
                var required = GetBuybackCost(owner) + GetGoldLossOnDeath(owner);

                if (unreliable + reliable > required)
                {
                    if (required - reliable > 0)
                    {
                        required -= reliable;

                        if (required - unreliable <= 0)
                        {
                            reliable = 0;
                            unreliable -= required;
                        }
                    }
                    else
                    {
                        reliable -= required;
                    }
                }
            }

            return (reliable, unreliable);
        }

        public static int GetBuybackCost(this Owner owner)
        {
            return (int)(200 + (GetNetWorth(owner) / 12f));
        }

        public static int GetGoldLossOnDeath(this Owner owner)
        {
            return (int)Math.Min(owner.Player.UnreliableGold, GetNetWorth(owner) / 40f);
        }

        public static int GetMissingHealth(this Owner owner)
        {
            return (int)(owner.Hero.MaximumHealth - owner.Hero.Health);
        }

        public static int GetMissingMana(this Owner owner)
        {
            return (int)(owner.Hero.MaximumMana - owner.Hero.Mana);
        }

        public static int GetNetWorth(this Owner owner)
        {
            return owner.Hero.Abilities.Where(x => x.IsItem).Sum(x => x.Id.GetPrice()) + owner.Player.ReliableGold
                                                                                       + owner.Player.UnreliableGold;
        }

        public static bool HasMissingResources(this Owner owner)
        {
            return owner.GetMissingHealth() > 10 || owner.GetMissingMana() > 10;
        }

        public static bool IsAtBase(this Owner owner)
        {
            return owner.Hero.BaseHero.ActiveShop == ShopType.Base;
        }
    }
}