namespace O9K.Hud.Modules.Unique.MorphlingAbilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Heroes;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;
    using Core.Managers.Renderer.Utils;

    using Ensage;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer;

    using Helpers;

    using MainMenu;

    using SharpDX;

    internal class MorphlingAbilities : IHudModule
    {
        private readonly MenuSwitcher abilitiesEnabled;

        private readonly MenuVectorSlider abilitiesPosition;

        private readonly MenuSlider abilitiesSize;

        private readonly MenuSlider abilitiesTextSize;

        private readonly IContext9 context;

        private readonly List<IMorphlingAbility> morphedAbilities = new List<IMorphlingAbility>();

        private readonly HashSet<AbilityId> morphlingAbilityIds = new HashSet<AbilityId>
        {
            AbilityId.morphling_waveform,
            AbilityId.morphling_adaptive_strike_agi,
            AbilityId.morphling_adaptive_strike_str
        };

        private readonly Sleeper sleeper = new Sleeper();

        private bool isMorphed;

        private Owner owner;

        [ImportingConstructor]
        public MorphlingAbilities(IContext9 context, IHudMenu hudMenu)
        {
            this.context = context;

            var abilitiesMenu = hudMenu.UniqueMenu
                .Add(new Menu(LocalizationHelper.LocalizeName(HeroId.npc_dota_hero_morphling), "Morphling"))
                .SetTexture(HeroId.npc_dota_hero_morphling);

            this.abilitiesEnabled = abilitiesMenu.Add(
                new MenuSwitcher("Abilities").SetTooltip("Show ability cooldowns when playing morphling"));
            this.abilitiesEnabled.AddTranslation(Lang.Ru, "Cпособности");
            this.abilitiesEnabled.AddTooltipTranslation(Lang.Ru, "Показывать время перезарядки способностей");
            this.abilitiesEnabled.AddTranslation(Lang.Cn, "播放声音");
            this.abilitiesEnabled.AddTooltipTranslation(Lang.Cn, "玩水人时显示隐藏技能冷却时间");

            var abilitiesSettings = abilitiesMenu.Add(new Menu("Settings"));
            abilitiesSettings.AddTranslation(Lang.Ru, "Настройки");
            abilitiesSettings.AddTranslation(Lang.Cn, "设置");

            this.abilitiesPosition = new MenuVectorSlider(abilitiesSettings, new Vector3(0, -250, 250), new Vector3(0, -250, 250));
            this.abilitiesSize = abilitiesSettings.Add(new MenuSlider("Size", 25, 20, 50));
            this.abilitiesSize.AddTranslation(Lang.Ru, "Размер");
            this.abilitiesSize.AddTranslation(Lang.Cn, "大小");

            this.abilitiesTextSize = abilitiesSettings.Add(new MenuSlider("Cooldown size", 16, 10, 35));
            this.abilitiesTextSize.AddTranslation(Lang.Ru, "Размер текста");
            this.abilitiesTextSize.AddTranslation(Lang.Cn, "文本大小");
        }

        public void Activate()
        {
            this.owner = EntityManager9.Owner;

            if (this.owner.HeroId != HeroId.npc_dota_hero_morphling)
            {
                return;
            }

            this.abilitiesEnabled.ValueChange += this.EnabledOnValueChange;
        }

        public void Dispose()
        {
            this.abilitiesEnabled.ValueChange -= this.EnabledOnValueChange;
            Unit.OnModifierAdded -= this.OnModifierAdded;
            Unit.OnModifierRemoved -= this.OnModifierRemoved;
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityMonitor.AbilityCasted -= this.OnAbilityCasted;
            EntityManager9.AbilityMonitor.AbilityCastChange -= this.OnAbilityCastChange;
            UpdateManager.Unsubscribe(this.OnUpdate);
            Player.OnExecuteOrder -= this.OnExecuteOrder;
            this.context.Renderer.Draw -= this.OnDraw;
            this.abilitiesPosition.Dispose();
            this.morphedAbilities.Clear();
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                Player.OnExecuteOrder += this.OnExecuteOrder;
                Unit.OnModifierAdded += this.OnModifierAdded;
                Unit.OnModifierRemoved += this.OnModifierRemoved;
                EntityManager9.AbilityAdded += this.OnAbilityAdded;
                EntityManager9.AbilityMonitor.AbilityCasted += this.OnAbilityCasted;
                EntityManager9.AbilityMonitor.AbilityCastChange += this.OnAbilityCastChange;
                UpdateManager.Subscribe(this.OnUpdate, 500);
            }
            else
            {
                Player.OnExecuteOrder -= this.OnExecuteOrder;
                Unit.OnModifierAdded -= this.OnModifierAdded;
                Unit.OnModifierRemoved -= this.OnModifierRemoved;
                EntityManager9.AbilityAdded -= this.OnAbilityAdded;
                EntityManager9.AbilityMonitor.AbilityCasted -= this.OnAbilityCasted;
                EntityManager9.AbilityMonitor.AbilityCastChange -= this.OnAbilityCastChange;
                UpdateManager.Unsubscribe(this.OnUpdate);
                this.context.Renderer.Draw -= this.OnDraw;
                this.morphedAbilities.Clear();
            }
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (ability.IsItem || !ability.Owner.IsMyHero)
                {
                    return;
                }

                if (ability.BaseAbility.IsReplicated)
                {
                    if ((ability.AbilityBehavior & AbilityBehavior.Hidden) != 0)
                    {
                        return;
                    }
                }
                else if (!this.morphlingAbilityIds.Contains(ability.Id))
                {
                    return;
                }

                this.morphedAbilities.Add(new MorphlingAbility(ability));
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAbilityCastChange(Ability9 ability)
        {
            try
            {
                if (ability.Id != AbilityId.morphling_replicate || !ability.IsCasting || !ability.Owner.IsMyHero)
                {
                    return;
                }

                this.sleeper.Sleep(0.5f);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAbilityCasted(Ability9 ability)
        {
            try
            {
                if (ability.IsItem || !ability.Owner.IsMyHero)
                {
                    return;
                }

                var morphlingAbility = this.morphedAbilities.Find(x => x.Handle == ability.Handle);
                if (morphlingAbility == null)
                {
                    return;
                }

                morphlingAbility.Update(this.isMorphed);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                var hpPosition = this.owner.Hero.HealthBarPosition;
                if (hpPosition.IsZero)
                {
                    return;
                }

                var healthBarSize = this.owner.Hero.HealthBarSize;
                var abilities = this.morphedAbilities.Where(x => x.Display(this.isMorphed)).OrderBy(x => x.AbilitySlot).ToArray();
                var start = (new Vector2(hpPosition.X + (healthBarSize.X * 0.5f), hpPosition.Y - this.abilitiesSize)
                             + this.abilitiesPosition) - new Vector2((this.abilitiesSize * abilities.Length) / 2f, 0);

                for (var i = 0; i < abilities.Length; i++)
                {
                    var ability = abilities[i];

                    ability.Draw(
                        renderer,
                        new Rectangle9(start + new Vector2(i * this.abilitiesSize, 0), this.abilitiesSize, this.abilitiesSize),
                        this.abilitiesTextSize);
                }
            }
            catch (InvalidOperationException)
            {
                // ignore
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (args.OrderId != OrderId.Ability || args.Ability.Id != AbilityId.morphling_morph_replicate)
                {
                    return;
                }

                this.sleeper.Sleep(0.2f);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Handle != this.owner.HeroHandle)
                {
                    return;
                }

                var name = args.Modifier.Name;

                if (name == "modifier_morphling_replicate_manager")
                {
                    this.morphedAbilities.Add(new ReplicateTimer(args.Modifier.RemainingTime));
                    this.context.Renderer.Draw += this.OnDraw;
                }
                else if (name == "modifier_morphling_replicate")
                {
                    this.isMorphed = true;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Handle != this.owner.HeroHandle)
                {
                    return;
                }

                var name = args.Modifier.Name;

                if (name == "modifier_morphling_replicate_manager")
                {
                    this.context.Renderer.Draw -= this.OnDraw;
                }
                else if (name == "modifier_morphling_replicate")
                {
                    this.isMorphed = false;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
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

                for (var i = this.morphedAbilities.Count - 1; i > -1; i--)
                {
                    var ability = this.morphedAbilities[i];

                    if (!ability.Update(this.isMorphed))
                    {
                        this.morphedAbilities.Remove(ability);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}