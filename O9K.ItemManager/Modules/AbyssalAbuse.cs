namespace O9K.ItemManager.Modules
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Heroes;
    using Core.Entities.Units.Unique;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Metadata;

    internal class AbyssalAbuse : IModule
    {
        private const AbilityId abyssalId = AbilityId.item_abyssal_blade;

        private readonly HashSet<AbilityId> disassembledAbyssalIds = new HashSet<AbilityId>
        {
            AbilityId.item_basher,
            AbilityId.item_vanguard,
            AbilityId.item_recipe_abyssal_blade
        };

        private readonly MenuSwitcher enabled;

        private readonly Sleeper sleeper = new Sleeper();

        private Owner owner;

        [ImportingConstructor]
        public AbyssalAbuse(IMainMenu mainMenu)
        {
            this.enabled = mainMenu.AbyssalAbuseMenu.Add(
                new MenuSwitcher("Auto disassemble", false).SetTooltip("Auto disassemble to remove passive bash cooldown"));
            this.enabled.AddTranslation(Lang.Ru, "Авто разбор");
            this.enabled.AddTooltipTranslation(Lang.Ru, "Автоматически разбирать, чтобы убрать кд пассивного баша");
            this.enabled.AddTranslation(Lang.Cn, "自动分解");
            this.enabled.AddTooltipTranslation(Lang.Cn, "自动分解以消除技能CD");
        }

        public void Activate()
        {
            this.owner = EntityManager9.Owner;
            this.enabled.ValueChange += this.EnabledOnValueChange;
        }

        public void Dispose()
        {
            this.enabled.ValueChange -= this.EnabledOnValueChange;
            UpdateManager.Unsubscribe(this.OnUpdate);
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                UpdateManager.Subscribe(this.OnUpdate, 100);
            }
            else
            {
                UpdateManager.Unsubscribe(this.OnUpdate);
            }
        }

        private void OnUpdate()
        {
            try
            {
                if (this.sleeper)
                {
                    return;
                }

                var hero = this.owner.Hero;
                if (hero == null || !hero.IsValid || !hero.IsAlive)
                {
                    return;
                }

                var baseHero = hero.BaseHero;
                var items = baseHero.Inventory.Items.Concat(baseHero.Inventory.Backpack).ToList();
                var abyssal = items.Find(x => x.Id == abyssalId);

                if (abyssal != null)
                {
                    var enemies = EntityManager9.Units.Where(
                        x => (x.IsHero || x is Roshan || x is SpiritBear) && x.Team != this.owner.Team && x.IsAlive
                             && x.Distance(baseHero.Position) < hero.GetAttackRange() + 300);
                    var bashed = enemies.Select(x => x.BaseModifiers.FirstOrDefault(z => z.TextureName == "item_abyssal_blade"))
                        .Any(x => x?.RemainingTime > 1f);

                    if (!bashed)
                    {
                        return;
                    }

                    var freeSlots = baseHero.Inventory.FreeInventorySlots.Count() + baseHero.Inventory.FreeBackpackSlots.Count();
                    if (freeSlots < 2)
                    {
                        return;
                    }

                    abyssal.DisassembleItem();
                    return;
                }

                var disassembledAbyssal = items.Where(x => this.disassembledAbyssalIds.Contains(x.Id)).ToList();
                if (disassembledAbyssal.Count != 3)
                {
                    return;
                }

                var delay = 0;

                foreach (var disassembled in disassembledAbyssal)
                {
                    if (!disassembled.IsCombineLocked)
                    {
                        continue;
                    }

                    UpdateManager.BeginInvoke(() => disassembled.UnlockCombining(), delay += 25);
                }

                this.sleeper.Sleep(0.5f);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}