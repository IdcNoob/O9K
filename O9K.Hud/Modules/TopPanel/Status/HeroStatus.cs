namespace O9K.Hud.Modules.TopPanel.Status
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Windows.Input;

    using Core.Data;
    using Core.Entities.Abilities.Base;
    using Core.Entities.Heroes.Unique;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;
    using Core.Managers.Renderer.Utils;

    using Ensage;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Renderer;
    using Ensage.SDK.Renderer.Texture;

    using Helpers;

    using MainMenu;

    using SharpDX;

    internal class HeroStatus : IHudModule
    {
        private readonly MenuHoldKey altKey;

        private readonly IContext9 context;

        private readonly MenuSwitcher dimHpMp;

        private readonly HashSet<AbilityId> drawItems = new HashSet<AbilityId>
        {
            AbilityId.item_gem,
            AbilityId.item_dust,
            AbilityId.item_rapier,
            AbilityId.item_aegis,
            AbilityId.item_cheese,
            AbilityId.item_refresher_shard,
            AbilityId.item_smoke_of_deceit,
            AbilityId.item_ward_sentry,
            AbilityId.item_ward_observer,
            AbilityId.item_ward_dispenser
        };

        private readonly HashSet<string> runeModifiers = new HashSet<string>
        {
            "modifier_rune_arcane",
            "modifier_rune_doubledamage",
            "modifier_rune_haste",
            "modifier_rune_invis",
            "modifier_rune_regen",
        };

        private readonly MenuSwitcher showAllyHealth;

        private readonly MenuSwitcher showAllyItems;

        private readonly MenuSwitcher showAllyMana;

        private readonly MenuSwitcher showAllyUlt;

        private readonly MenuSwitcher showBuyback;

        private readonly MenuSwitcher showEnemyHealth;

        private readonly MenuSwitcher showEnemyItems;

        private readonly MenuSwitcher showEnemyMana;

        private readonly MenuSwitcher showEnemyUlt;

        private readonly MenuSwitcher showUltCd;

        private readonly MenuSwitcher showUltCdTime;

        private readonly ITopPanel topPanel;

        private readonly TopPanelUnit[] units = new TopPanelUnit[10];

        [ImportingConstructor]
        public HeroStatus(IContext9 context, ITopPanel topPanel, IHudMenu hudMenu)
        {
            this.context = context;
            this.topPanel = topPanel;

            var statusMenu = hudMenu.TopPanelMenu.Add(new Menu("Status"));
            statusMenu.AddTranslation(Lang.Ru, "Статус");
            statusMenu.AddTranslation(Lang.Cn, "状态");

            var healthMenu = statusMenu.Add(new Menu("Health"));
            healthMenu.AddTranslation(Lang.Ru, "Здоровье");
            healthMenu.AddTranslation(Lang.Cn, "生命值");

            this.showEnemyHealth = healthMenu.Add(new MenuSwitcher("Show enemy health"));
            this.showEnemyHealth.AddTranslation(Lang.Ru, "Показывать здоровье врагов");
            this.showEnemyHealth.AddTranslation(Lang.Cn, "显示敌人血量状态");

            this.showAllyHealth = healthMenu.Add(new MenuSwitcher("Show ally health"));
            this.showAllyHealth.AddTranslation(Lang.Ru, "Показывать здоровье союзников");
            this.showAllyHealth.AddTranslation(Lang.Cn, "显示盟友血量状态");

            var manaMenu = statusMenu.Add(new Menu("Mana"));
            manaMenu.AddTranslation(Lang.Ru, "Мана");
            manaMenu.AddTranslation(Lang.Cn, "魔法值");

            this.showEnemyMana = manaMenu.Add(new MenuSwitcher("Show enemy mana"));
            this.showEnemyMana.AddTranslation(Lang.Ru, "Показывать ману врагов");
            this.showEnemyMana.AddTranslation(Lang.Cn, "显示敌人法力状态");

            this.showAllyMana = manaMenu.Add(new MenuSwitcher("Show ally mana"));
            this.showAllyMana.AddTranslation(Lang.Ru, "Показывать ману союзников");
            this.showAllyMana.AddTranslation(Lang.Cn, "显示盟友法力状态");

            var ultMenu = statusMenu.Add(new Menu("Ultimate"));
            ultMenu.AddTranslation(Lang.Ru, "Ульта");
            ultMenu.AddTranslation(Lang.Cn, "终极技能");

            this.showEnemyUlt = ultMenu.Add(new MenuSwitcher("Show enemy ultimate"));
            this.showEnemyUlt.AddTranslation(Lang.Ru, "Показывать ульту врагов");
            this.showEnemyUlt.AddTranslation(Lang.Cn, "显示敌人终极技能");

            this.showAllyUlt = ultMenu.Add(new MenuSwitcher("Show ally ultimate"));
            this.showAllyUlt.AddTranslation(Lang.Ru, "Показывать ульту союзников");
            this.showAllyUlt.AddTranslation(Lang.Cn, "显示盟友终极技能");

            this.showUltCd = ultMenu.Add(new MenuSwitcher("Show ultimate cooldown"));
            this.showUltCd.AddTranslation(Lang.Ru, "Показывать \"круг\" ульты");
            this.showUltCd.AddTranslation(Lang.Cn, "显示终极技能冷却时间");

            this.showUltCdTime = ultMenu.Add(new MenuSwitcher("Show ultimate cooldown time", false));
            this.showUltCdTime.AddTranslation(Lang.Ru, "Показывать время ульты в \"круге\"");
            this.showUltCdTime.AddTranslation(Lang.Cn, "显示终极技能冷却时间时间");

            var buyBackMenu = statusMenu.Add(new Menu("Buyback"));
            buyBackMenu.AddTranslation(Lang.Ru, "Выкуп");
            buyBackMenu.AddTranslation(Lang.Cn, "买活");

            this.showBuyback = buyBackMenu.Add(new MenuSwitcher("Show when dead").SetTooltip("Show if enemy has buyback when dead"));
            this.showBuyback.AddTranslation(Lang.Ru, "При смерти");
            this.showBuyback.AddTooltipTranslation(Lang.Ru, "Показывать есть ли выкуп, когда враг мертв");
            this.showBuyback.AddTranslation(Lang.Cn, "死亡显示");
            this.showBuyback.AddTooltipTranslation(Lang.Cn, "显示敌人死后是否有买活");

            var itemsMenu = hudMenu.TopPanelMenu.Add(new Menu("Items"));
            itemsMenu.AddTranslation(Lang.Ru, "Предметы");
            itemsMenu.AddTranslation(Lang.Cn, "物品");

            this.showEnemyItems = itemsMenu.Add(new MenuSwitcher("Show enemy items")).SetTooltip("Show important enemy items");
            this.showEnemyItems.AddTranslation(Lang.Ru, "Предметы врагов");
            this.showEnemyItems.AddTooltipTranslation(Lang.Ru, "Показывать важные предметы врагов");
            this.showEnemyItems.AddTranslation(Lang.Cn, "显示敌人物品");
            this.showEnemyItems.AddTooltipTranslation(Lang.Cn, "显示重要的敌人物品");

            this.showAllyItems = itemsMenu.Add(new MenuSwitcher("Show ally items")).SetTooltip("Show important ally items");
            this.showAllyItems.AddTranslation(Lang.Ru, "Предметы союзников");
            this.showAllyItems.AddTooltipTranslation(Lang.Ru, "Показывать важные предметы союзников");
            this.showAllyItems.AddTranslation(Lang.Cn, "盟友物品");
            this.showAllyItems.AddTooltipTranslation(Lang.Cn, "显示重要的盟友物品");

            var visibilityMenu = hudMenu.TopPanelMenu.Add(new Menu("Visibility"));
            visibilityMenu.AddTranslation(Lang.Ru, "Видимость");
            visibilityMenu.AddTranslation(Lang.Cn, "能见度");

            this.dimHpMp = visibilityMenu.Add(new MenuSwitcher("Dim health and mana"))
                .SetTooltip("Dim health and mana bars when unit is not visible");
            this.dimHpMp.AddTranslation(Lang.Ru, "Затемнять хп/мп");
            this.dimHpMp.AddTooltipTranslation(Lang.Ru, "Затемнять хп/мп если враг в тумане войны");
            this.dimHpMp.AddTranslation(Lang.Cn, "昏暗的健康和马纳");
            this.dimHpMp.AddTooltipTranslation(Lang.Cn, "当单位不可见时昏暗的生命值和法力值");

            // hidden alt key
            this.altKey = statusMenu.Add(new MenuHoldKey("alt", Key.LeftAlt));
            this.altKey.Hide();
        }

        public void Activate()
        {
            this.LoadTextures();

            EntityManager9.UnitAdded += this.OnUnitAdded;
            EntityManager9.AbilityAdded += this.OnAbilityAdded;
            EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
            Unit.OnModifierAdded += this.OnModifierAdded;
            Game.OnFireEvent += this.OnFireEvent;
            this.context.Renderer.Draw += this.OnDraw;
        }

        public void Dispose()
        {
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
            Unit.OnModifierAdded -= this.OnModifierAdded;
            Game.OnFireEvent -= this.OnFireEvent;
            this.context.Renderer.Draw -= this.OnDraw;
        }

        private void LoadTextures()
        {
            var tm = this.context.Renderer.TextureManager;

            tm.LoadFromDota(
                "o9k.health_ally",
                @"panorama\images\hud\reborn\topbar_health_psd.vtex_c",
                new TextureProperties
                {
                    Brightness = -60
                });
            tm.LoadFromDota("o9k.health_ally_visible", @"panorama\images\hud\reborn\topbar_health_psd.vtex_c");
            tm.LoadFromDota(
                "o9k.health_ally_bg",
                @"panorama\images\hud\reborn\topbar_health_psd.vtex_c",
                new TextureProperties
                {
                    Brightness = -170
                });
            tm.LoadFromDota("o9k.health_enemy", @"panorama\images\hud\reborn\topbar_health_dire_psd.vtex_c");
            tm.LoadFromDota(
                "o9k.health_enemy_invis",
                @"panorama\images\hud\reborn\topbar_health_dire_psd.vtex_c",
                new TextureProperties
                {
                    Brightness = -60
                });
            tm.LoadFromDota(
                "o9k.health_enemy_bg",
                @"panorama\images\hud\reborn\topbar_health_dire_psd.vtex_c",
                new TextureProperties
                {
                    Brightness = -170
                });
            tm.LoadFromDota("o9k.mana", @"panorama\images\hud\reborn\topbar_mana_psd.vtex_c");
            tm.LoadFromDota(
                "o9k.mana_invis",
                @"panorama\images\hud\reborn\topbar_mana_psd.vtex_c",
                new TextureProperties
                {
                    Brightness = -60
                });
            tm.LoadFromDota(
                "o9k.mana_bg",
                @"panorama\images\hud\reborn\topbar_mana_psd.vtex_c",
                new TextureProperties
                {
                    Brightness = -170
                });
            tm.LoadFromDota("o9k.ult_rdy", @"panorama\images\hud\reborn\ult_ready_psd.vtex_c");
            tm.LoadFromDota("o9k.ult_cd", @"panorama\images\hud\reborn\ult_cooldown_psd.vtex_c");
            tm.LoadFromDota("o9k.ult_mp", @"panorama\images\hud\reborn\ult_no_mana_psd.vtex_c");
            tm.LoadFromDota("o9k.buyback", @"panorama\images\hud\reborn\buyback_header_psd.vtex_c");
            tm.LoadFromDota("o9k.buyback_alive", @"panorama\images\hud\reborn\buyback_topbar_alive_psd.vtex_c");
            tm.LoadFromDota(
                "o9k.top_ult_cd_bg",
                @"panorama\images\masks\softedge_circle_sharp_png.vtex_c",
                new TextureProperties
                {
                    ColorRatio = new Vector4(0f, 0f, 0f, 0.5f)
                });
            tm.LoadFromDota("o9k.outline", @"panorama\images\hud\reborn\buff_outline_psd.vtex_c");
            tm.LoadFromDota(
                "o9k.outline_green_pct",
                @"panorama\images\hud\reborn\buff_outline_psd.vtex_c",
                new TextureProperties
                {
                    ColorRatio = new Vector4(0f, 1f, 0f, 1f),
                    Sliced = true
                });
            tm.LoadFromDota(
                "o9k.outline_blue_pct",
                @"panorama\images\hud\reborn\buff_outline_psd.vtex_c",
                new TextureProperties
                {
                    ColorRatio = new Vector4(0f, 0.4f, 0.9f, 1f),
                    Brightness = 40,
                    Sliced = true
                });

            foreach (var abilityId in this.drawItems)
            {
                tm.LoadAbilityFromDota(abilityId, true);
            }
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (ability.Owner.IsIllusion)
                {
                    return;
                }

                if (ability.IsItem)
                {
                    if (!this.drawItems.Contains(ability.Id))
                    {
                        return;
                    }

                    var id = ability.BaseItem.PurchaserId;
                    if (id == -1)
                    {
                        return;
                    }

                    this.units[id]?.AddItem(ability);
                }
                else if (ability.IsUltimate && !ability.IsStolen)
                {
                    foreach (var unit in this.units)
                    {
                        unit?.SetUltimate(ability);
                    }
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
                if (!ability.IsItem || ability.Owner.IsIllusion || !this.drawItems.Contains(ability.Id))
                {
                    return;
                }

                var id = ability.BaseItem.PurchaserId;
                if (id == -1)
                {
                    return;
                }

                this.units[id]?.RemoveItem(ability);
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
                var altPressed = this.altKey.IsActive;
                var ratio = Hud.Info.ScreenRatio;

                for (var i = 0; i < this.units.Length; i++)
                {
                    var hero = this.units[i];
                    if (hero?.IsValid != true)
                    {
                        continue;
                    }

                    hero.DrawRunes(renderer, this.topPanel.GetPlayersHealthBarPosition(i, 7 * ratio, 0));

                    if (hero.IsAlly)
                    {
                        var topIndent = 0f;

                        if (this.showAllyHealth)
                        {
                            hero.DrawAllyHealth(renderer, this.dimHpMp, this.topPanel.GetPlayersHealthBarPosition(i, 8 * ratio, 0));
                            topIndent += 8 * ratio;
                        }

                        if (this.showAllyMana)
                        {
                            hero.DrawAllyMana(renderer, this.dimHpMp, this.topPanel.GetPlayersHealthBarPosition(i, 8 * ratio, topIndent));
                            topIndent += 8 * ratio;
                        }

                        if (this.showAllyUlt)
                        {
                            if (hero.DrawUltimate(
                                renderer,
                                this.topPanel.GetPlayersUltimatePosition(i, 18 * ratio, 0),
                                this.showUltCd && !altPressed
                                    ? this.topPanel.GetPlayersUltimatePosition(i, 42 * ratio, topIndent + (28 * ratio))
                                    : Rectangle9.Zero,
                                this.showUltCdTime))
                            {
                                topIndent += 50 * ratio;
                            }
                        }

                        if (this.showAllyItems && !altPressed)
                        {
                            hero.DrawItems(renderer, this.topPanel.GetPlayersHealthBarPosition(i, 15 * ratio, topIndent + (5 * ratio)));
                        }
                    }
                    else
                    {
                        if (this.showBuyback)
                        {
                            hero.DrawBuyback(renderer, this.topPanel.GetPlayersHealthBarPosition(i, 28 * ratio, 0));
                        }

                        var topIndent = 0f;

                        if (this.showEnemyHealth || altPressed)
                        {
                            hero.DrawEnemyHealth(renderer, this.dimHpMp, this.topPanel.GetPlayersHealthBarPosition(i, 8 * ratio, 0));
                            topIndent += 8 * ratio;
                        }

                        if (this.showEnemyMana || altPressed)
                        {
                            hero.DrawEnemyMana(renderer, this.dimHpMp, this.topPanel.GetPlayersHealthBarPosition(i, 8 * ratio, topIndent));
                            topIndent += 8 * ratio;
                        }

                        if (altPressed)
                        {
                            var position = this.topPanel.GetPlayersHealthBarPosition(i, 10 * ratio, topIndent * 0.55f);
                            var deadPosition = this.topPanel.GetPlayersHealthBarPosition(i, 28 * ratio, 0);

                            hero.ForceDrawBuyback(renderer, position, deadPosition);
                        }

                        if (this.showEnemyUlt)
                        {
                            if (hero.DrawUltimate(
                                renderer,
                                this.topPanel.GetPlayersUltimatePosition(i, 16 * ratio, 0),
                                this.showUltCd && !altPressed
                                    ? this.topPanel.GetPlayersUltimatePosition(i, 42 * ratio, topIndent + (28 * ratio))
                                    : Rectangle9.Zero,
                                this.showUltCdTime))
                            {
                                topIndent += 50 * ratio;
                            }
                        }

                        if (this.showEnemyItems && !altPressed)
                        {
                            hero.DrawItems(renderer, this.topPanel.GetPlayersHealthBarPosition(i, 15 * ratio, topIndent + (5 * ratio)));
                        }
                    }
                }
            }
            catch (InvalidOperationException)
            {
                //ignore
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnFireEvent(FireEventEventArgs args)
        {
            if (args.GameEvent.Name != "dota_buyback")
            {
                return;
            }

            try
            {
                var id = args.GameEvent.GetInt("player_id");
                this.units[id]?.BuybackSleeper.Sleep(GameData.BuybackCooldown);
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
                var modifier = args.Modifier;
                if (!this.runeModifiers.Contains(modifier.Name))
                {
                    return;
                }

                var hero = this.units.Find(x => x?.Handle == sender.Handle);
                if (hero == null)
                {
                    return;
                }

                hero.AddModifier(modifier);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitAdded(Unit9 hero)
        {
            try
            {
                if (!hero.IsHero || hero.IsIllusion)
                {
                    return;
                }

                if (hero is Meepo meepo && !meepo.IsMainMeepo)
                {
                    return;
                }

                if (!(hero.BaseOwner is Player player))
                {
                    return;
                }

                this.units[player.Id] = new TopPanelUnit(hero);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}