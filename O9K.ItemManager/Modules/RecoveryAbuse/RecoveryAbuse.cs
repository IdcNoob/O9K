namespace O9K.ItemManager.Modules.RecoveryAbuse
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Input;

    using Abilities;

    using Core.Data;
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Abilities.Items;
    using Core.Entities.Heroes;
    using Core.Entities.Metadata;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    using Metadata;

    using Utils;

    using Attribute = Ensage.Attribute;

    internal class RecoveryAbuse : IModule
    {
        private readonly MenuAbilityPriorityChanger abilitiesAltToggler;

        private readonly MenuAbilityPriorityChanger abilitiesToggler;

        private readonly Dictionary<AbilityId, Type> abilityTypes = new Dictionary<AbilityId, Type>();

        private readonly HashSet<OrderId> blockedOrders = new HashSet<OrderId>
        {
            OrderId.MoveLocation,
            OrderId.MoveTarget,
            OrderId.AttackLocation,
            OrderId.AttackTarget,
        };

        private readonly MenuHoldKey ctrlKey;

        private readonly MenuSlider delay;

        private readonly Dictionary<ItemSlot, Ability9> droppedItems = new Dictionary<ItemSlot, Ability9>();

        private readonly HashSet<AbilityId> ignoredAbilities = new HashSet<AbilityId>
        {
            AbilityId.morphling_morph_str,
            AbilityId.terrorblade_sunder,
            AbilityId.item_tango,
            AbilityId.item_tango_single,
        };

        private readonly List<uint> ignoredItems = new List<uint>();

        private readonly List<ItemSlot> ignoredSlots = new List<ItemSlot>();

        private readonly MenuHoldKey key;

        private readonly Sleeper pickSleeper = new Sleeper();

        private readonly List<RecoveryAbility> recoveryAbilities = new List<RecoveryAbility>();

        private readonly Sleeper sortSleeper = new Sleeper();

        private readonly Sleeper useSleeper = new Sleeper();

        private ItemSlot bottleSlot;

        private bool bottleTaken;

        private bool heroIsMoving;

        private Owner owner;

        private IUpdateHandler pickUpItemsHandler;

        private Attribute powerTreadsAttribute;

        private bool powerTreadsChanged;

        private IUpdateHandler sortItemsHandler;

        private MenuAbilityPriorityChanger toggler;

        private IUpdateHandler useAbilitiesHandler;

        [ImportingConstructor]
        public RecoveryAbuse(IMainMenu mainMenu)
        {
            var menu = mainMenu.RecoveryAbuseMenu;

            foreach (var type in Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass && typeof(RecoveryAbility).IsAssignableFrom(x)))
            {
                foreach (var attribute in type.GetCustomAttributes<AbilityIdAttribute>())
                {
                    this.abilityTypes.Add(attribute.AbilityId, type);
                }
            }

            this.key = menu.Add(new MenuHoldKey("Key"));
            this.key.AddTranslation(Lang.Ru, "Клавиша");
            this.key.AddTranslation(Lang.Cn, "键");

            this.delay = menu.Add(new MenuSlider("Delay", 300, 50, 500));
            this.delay.AddTranslation(Lang.Ru, "Задержка");
            this.delay.AddTranslation(Lang.Cn, "延迟");

            this.abilitiesToggler = menu.Add(new MenuAbilityPriorityChanger("Abilities"));
            this.abilitiesToggler.AddTranslation(Lang.Ru, "Способности");
            this.abilitiesToggler.AddTranslation(Lang.Cn, "播放声音");

            this.abilitiesAltToggler = menu.Add(new MenuAbilityPriorityChanger("Alt. abilities")).SetTooltip("(CTRL+Key)");
            this.abilitiesAltToggler.AddTranslation(Lang.Ru, "Альт. способности");
            this.abilitiesAltToggler.AddTooltipTranslation(Lang.Ru, "(CTRL+Клавиша)");
            this.abilitiesAltToggler.AddTranslation(Lang.Cn, "另类播放声音");
            this.abilitiesAltToggler.AddTooltipTranslation(Lang.Cn, "(CTRL+键)");

            this.ctrlKey = menu.Add(new MenuHoldKey("ctrl", Key.LeftCtrl));
            this.ctrlKey.Hide();
        }

        public float ShortDelay
        {
            get
            {
                return Math.Max(0.05f, this.DefaultDelay);
            }
        }

        private float DefaultDelay
        {
            get
            {
                return this.delay / 1000f;
            }
        }

        public void Activate()
        {
            this.owner = EntityManager9.Owner;

            this.pickUpItemsHandler = UpdateManager.Subscribe(this.OnUpdatePickUp, 0, false);
            this.useAbilitiesHandler = UpdateManager.Subscribe(this.OnUpdateUse, 0, false);
            this.sortItemsHandler = UpdateManager.Subscribe(this.OnUpdateSort, 0, false);
            EntityManager9.AbilityAdded += this.OnAbilityAdded;
            EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
            this.key.ValueChange += this.KeyOnValueChange;
        }

        public void Dispose()
        {
            this.key.ValueChange -= this.KeyOnValueChange;
            Player.OnExecuteOrder -= this.OnExecuteOrder;
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
            UpdateManager.Unsubscribe(this.useAbilitiesHandler);
            UpdateManager.Unsubscribe(this.pickUpItemsHandler);
            UpdateManager.Unsubscribe(this.sortItemsHandler);
            this.recoveryAbilities.Clear();
        }

        public bool SortItems()
        {
            foreach (var droppedItem in this.droppedItems.DistinctBy(x => x.Value))
            {
                var item = droppedItem.Value;
                if (!item.IsValid)
                {
                    continue;
                }

                var savedSlot = droppedItem.Key;
                if (savedSlot < 0)
                {
                    continue;
                }

                var currentSlot = item.GetItemSlot();
                if (currentSlot < 0 || currentSlot == savedSlot)
                {
                    continue;
                }

                Player.MoveItem(this.owner, item, savedSlot);
                return true;
            }

            return false;
        }

        private bool DropItem(Ability9 ability, bool toBackpack)
        {
            var item = ability.BaseItem;
            var hero = ability.Owner;

            if (this.ignoredItems.Contains(item.Handle))
            {
                return false;
            }

            this.droppedItems[ability.GetItemSlot()] = ability;

            if (toBackpack && !this.owner.IsAtBase())
            {
                ItemSlot? slot = null;

                for (var i = ItemSlot.BackPack_1; i <= ItemSlot.BackPack_3; i++)
                {
                    if (this.ignoredSlots.Contains(i))
                    {
                        continue;
                    }

                    var bpItem = this.owner.Hero.BaseInventory.GetItem(i);
                    if (bpItem == null || bpItem.NeutralTierIndex < 0)
                    {
                        slot = i;
                        break;
                    }
                }

                if (slot == null)
                {
                    return false;
                }

                if (Player.MoveItem(hero, item, slot.Value))
                {
                    if (item.NeutralTierIndex >= 0)
                    {
                        this.ignoredSlots.Add(slot.Value);
                    }

                    this.ignoredItems.Add(item.Handle);
                    return true;
                }

                return false;
            }

            if (Player.DropItem(hero, item, RNG.Randomize(hero.Position, 40)))
            {
                this.ignoredItems.Add(item.Handle);
                return true;
            }

            return false;
        }

        private bool DropItems(RecoveryAbility ability)
        {
            var items = ability.Owner.Abilities.Where(x => x.IsItem && x.IsUsable && x.Handle != ability.Handle).ToList();
            var toBackPack = this.heroIsMoving;

            if (ability.RestoresHealth && items.Any(x => x.Id.HasHealthStats() && this.DropItem(x, toBackPack)))
            {
                return true;
            }

            if (ability.RestoresMana && items.Any(x => x.Id.HasManaStats() && this.DropItem(x, toBackPack)))
            {
                return true;
            }

            return false;
        }

        private bool IsAllItemsDropped(RecoveryAbility ability)
        {
            var dropped = this.droppedItems.Values.ToArray();
            var remainingItems = ability.Owner.Abilities
                .Where(x => x.IsItem && x.IsUsable && x.Handle != ability.Handle && !dropped.Contains(x))
                .ToList();

            if (ability.RestoresHealth && remainingItems.Any(x => x.Id.HasHealthStats()))
            {
                return false;
            }

            if (ability.RestoresMana && remainingItems.Any(x => x.Id.HasManaStats()))
            {
                return false;
            }

            return true;
        }

        private bool IsHeroHealing()
        {
            if (!this.owner.Hero.HasModifier(ModifierNames.HealingSalveRegeneration, ModifierNames.BottleRegeneration))
            {
                return false;
            }

            if (this.owner.GetMissingHealth() < 10)
            {
                return false;
            }

            return true;
        }

        private void KeyOnValueChange(object sender, KeyEventArgs e)
        {
            if (this.pickUpItemsHandler.IsEnabled)
            {
                return;
            }

            this.toggler = this.ctrlKey ? this.abilitiesAltToggler : this.abilitiesToggler;

            this.ignoredItems.Clear();
            this.ignoredSlots.Clear();

            if (e.NewValue)
            {
                this.heroIsMoving = this.owner.Hero.IsMoving;
                Player.OnExecuteOrder += this.OnExecuteOrder;
                this.sortItemsHandler.IsEnabled = false;
                this.useAbilitiesHandler.IsEnabled = true;
            }
            else
            {
                this.useAbilitiesHandler.IsEnabled = false;
                this.pickUpItemsHandler.IsEnabled = true;
            }
        }

        private bool MoveBottleToStash()
        {
            if (!this.bottleTaken)
            {
                return false;
            }

            var bottle = this.recoveryAbilities.Find(x => x.Id == AbilityId.item_bottle);
            if (bottle == null)
            {
                this.bottleTaken = false;
                return false;
            }

            if (!Player.MoveItem(this.owner, bottle.Ability, this.bottleSlot))
            {
                return true;
            }

            this.bottleTaken = false;
            return true;
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (!ability.Owner.IsMyHero)
                {
                    return;
                }

                if (this.ignoredAbilities.Contains(ability.Id))
                {
                    return;
                }

                if ((ability is IHealthRestore healthRestore && healthRestore.RestoresOwner)
                    || (ability is IManaRestore manaRestore && manaRestore.RestoresOwner))
                {
                    if (this.abilityTypes.TryGetValue(ability.Id, out var type))
                    {
                        this.recoveryAbilities.Add((RecoveryAbility)Activator.CreateInstance(type, ability));
                    }
                    else
                    {
                        this.recoveryAbilities.Add(new RecoveryAbility(ability));
                    }

                    this.abilitiesToggler.AddAbility(ability.Name);
                    this.abilitiesAltToggler.AddAbility(ability.Name, false);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAbilityRemoved(Ability9 ability)
        {
            try
            {
                if (!ability.Owner.IsMyHero)
                {
                    return;
                }

                var recoveryAbility = this.recoveryAbilities.Find(x => x.Handle == ability.Handle);
                if (recoveryAbility == null)
                {
                    return;
                }

                this.recoveryAbilities.Remove(recoveryAbility);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            //todo delete ?

            //try
            //{
            //    if (!args.Entities.Contains(this.owner))
            //    {
            //        return;
            //    }

            //    if (this.blockedOrders.Contains(args.OrderId))
            //    {
            //        args.Process = false;
            //    }
            //}
            //catch (Exception e)
            //{
            //    Logger.Error(e);
            //}
        }

        private void OnUpdatePickUp()
        {
            try
            {
                if (Game.IsPaused || this.pickSleeper)
                {
                    return;
                }

                var hero = this.owner.Hero;
                if (!hero.IsAlive)
                {
                    Player.OnExecuteOrder -= this.OnExecuteOrder;
                    this.pickUpItemsHandler.IsEnabled = false;
                    this.sortItemsHandler.IsEnabled = true;
                    return;
                }

                if (this.MoveBottleToStash())
                {
                    this.pickSleeper.Sleep(RNG.Randomize(0.15f, 0.05f));
                    return;
                }

                var pickUp = this.droppedItems.Values.Select(x => x.Handle).ToArray();
                var item = ObjectManager.GetEntitiesFast<PhysicalItem>()
                    .FirstOrDefault(
                        x => !this.ignoredItems.Contains(x.Handle) && pickUp.Contains(x.Item.Handle) && hero.Distance(x.Position) < 400);

                if (item != null)
                {
                    if (Player.PickUpItem(hero, item))
                    {
                        this.ignoredItems.Add(item.Handle);
                        this.pickSleeper.Sleep(RNG.Randomize(this.ShortDelay, 0.05f));
                    }
                }
                else
                {
                    if (!this.sortItemsHandler.IsEnabled)
                    {
                        this.sortSleeper.Sleep(0.5f);
                    }

                    Player.OnExecuteOrder -= this.OnExecuteOrder;
                    this.sortItemsHandler.IsEnabled = true;
                    this.pickUpItemsHandler.IsEnabled = false;
                }
            }
            catch (Exception e)
            {
                //todo test
                Player.OnExecuteOrder -= this.OnExecuteOrder;

                Logger.Error(e);
            }
        }

        private void OnUpdateSort()
        {
            try
            {
                if (Game.IsPaused || this.sortSleeper)
                {
                    return;
                }

                if (this.IsHeroHealing())
                {
                    if (this.SortItems())
                    {
                        this.sortSleeper.Sleep(RNG.Randomize(0.35f, 0.1f));
                        return;
                    }

                    this.sortSleeper.Sleep(0.3f);
                }
                else
                {
                    if (this.SwitchPowerTreadsBack())
                    {
                        this.sortSleeper.Sleep(RNG.Randomize(0.1f, 0.02f));
                        return;
                    }

                    if (this.SortItems())
                    {
                        this.sortSleeper.Sleep(RNG.Randomize(0.35f, 0.1f));
                        return;
                    }

                    this.sortItemsHandler.IsEnabled = false;
                }

                this.droppedItems.Clear();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUpdateUse()
        {
            try
            {
                if (Game.IsPaused || this.useSleeper)
                {
                    return;
                }

                var abilities = this.recoveryAbilities.Where(x => this.toggler.IsEnabled(x.Name))
                    .OrderBy(x => this.toggler.GetPriority(x.Name))
                    .ToList();

                if (abilities.Count == 0)
                {
                    return;
                }

                if (this.TakeBottleFromStash(abilities))
                {
                    this.useSleeper.Sleep(RNG.Randomize(0.15f, 0.05f));
                    return;
                }

                foreach (var ability in abilities)
                {
                    if (!ability.CanBeCasted())
                    {
                        continue;
                    }

                    if (this.SwitchPowerTreads(ability))
                    {
                        this.useSleeper.Sleep(RNG.Randomize(0.1f, 0.02f));
                        return;
                    }

                    if (this.DropItems(ability))
                    {
                        this.useSleeper.Sleep(
                            this.IsAllItemsDropped(ability) ? RNG.Randomize(0.1f, 0.05f) : RNG.Randomize(this.DefaultDelay, 0.05f));
                        return;
                    }

                    if (ability.UseAbility())
                    {
                        this.useSleeper.Sleep(ability.Delay + RNG.Randomize(this.DefaultDelay, 0.05f));
                        return;
                    }
                }

                if (abilities.Any(x => !x.CanPickUpItems()))
                {
                    return;
                }

                if (this.PickUpUsable(abilities))
                {
                    this.useSleeper.Sleep(RNG.Randomize(0.3f, 0.05f));
                    return;
                }

                this.pickSleeper.Sleep(0.1f);
                this.useAbilitiesHandler.IsEnabled = false;
                this.pickUpItemsHandler.IsEnabled = true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private bool PickUpUsable(IEnumerable<RecoveryAbility> abilities)
        {
            var hero = this.owner.Hero;
            var usable = abilities.Where(x => x.Ability.IsItem && x.Ability.RemainingCooldown <= 0 && x.Owner.Mana >= x.Ability.ManaCost)
                .Select(x => x.Handle)
                .ToArray();
            var dropped = this.droppedItems.Values.Select(x => x.Handle).ToArray();

            var item = ObjectManager.GetEntitiesFast<PhysicalItem>()
                .FirstOrDefault(x => usable.Contains(x.Item.Handle) && dropped.Contains(x.Item.Handle) && hero.Distance(x.Position) < 400);

            if (item == null)
            {
                return false;
            }

            Player.PickUpItem(hero, item);
            return true;
        }

        private bool SwitchPowerTreads(RecoveryAbility ability)
        {
            var powerTreads = (PowerTreads)this.owner.Hero.Abilities.FirstOrDefault(x => x.Id == AbilityId.item_power_treads);
            if (powerTreads?.CanBeCasted() != true)
            {
                return false;
            }

            var currentAtt = powerTreads.ActiveAttribute;
            var requiredAtt = ability.PowerTreadsAttribute;

            if (requiredAtt == Attribute.Invalid || currentAtt == requiredAtt)
            {
                return false;
            }

            if (!this.powerTreadsChanged)
            {
                this.powerTreadsAttribute = currentAtt;
                this.powerTreadsChanged = true;
            }

            powerTreads.UseAbility();
            return true;
        }

        private bool SwitchPowerTreadsBack()
        {
            if (!this.powerTreadsChanged)
            {
                return false;
            }

            var powerTreads = (PowerTreads)this.owner.Hero.Abilities.FirstOrDefault(x => x.Id == AbilityId.item_power_treads);
            if (powerTreads == null)
            {
                this.powerTreadsChanged = false;
                return false;
            }

            if (powerTreads.ActiveAttribute == this.powerTreadsAttribute)
            {
                this.powerTreadsChanged = false;
                return false;
            }

            if (!powerTreads.CanBeCasted())
            {
                return true;
            }

            powerTreads.UseAbility();
            return true;
        }

        private bool TakeBottleFromStash(List<RecoveryAbility> abilities)
        {
            if (this.bottleTaken || !this.owner.Hero.HasModifier(ModifierNames.FountainRegeneration))
            {
                return false;
            }

            if (!this.owner.HasMissingResources())
            {
                return false;
            }

            var bottle = abilities.Find(x => x.Id == AbilityId.item_bottle);
            if (bottle == null)
            {
                return false;
            }

            this.bottleSlot = bottle.Ability.GetItemSlot();
            if (this.bottleSlot <= ItemSlot.InventorySlot_6)
            {
                return false;
            }

            if (!Player.MoveItem(this.owner, bottle.Ability, ItemSlot.InventorySlot_1))
            {
                return true;
            }

            this.bottleTaken = true;
            return true;
        }
    }
}