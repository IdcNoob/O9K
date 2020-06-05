namespace O9K.Hud.Modules.Screen.Panels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Heroes.Unique;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Input;
    using Core.Managers.Input.EventArgs;
    using Core.Managers.Input.Keys;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;
    using Core.Managers.Renderer.Utils;

    using Ensage;
    using Ensage.SDK.Renderer;
    using Ensage.SDK.Renderer.Texture;

    using Helpers;

    using MainMenu;

    using SharpDX;

    using Color = System.Drawing.Color;
    using KeyEventArgs = Core.Managers.Menu.EventArgs.KeyEventArgs;

    internal class ItemPanel : IHudModule
    {
        private readonly IContext9 context;

        private readonly MenuHoldKey holdKey;

        private readonly IInputManager9 input;

        private readonly MenuSwitcher ping;

        private readonly MenuVectorSlider position;

        private readonly MenuSwitcher show;

        private readonly MenuSwitcher showCharges;

        private readonly MenuSwitcher showCooldown;

        private readonly MenuSlider size;

        private readonly MenuToggleKey toggleKey;

        private readonly List<Unit9> units = new List<Unit9>();

        private Vector2 heroSize;

        private Vector2 itemSize;

        private Team ownerTeam;

        [ImportingConstructor]
        public ItemPanel(IContext9 context, IHudMenu hudMenu, IInputManager9 input)
        {
            this.context = context;
            this.input = input;

            var panelsMenu = hudMenu.ScreenMenu.GetOrAdd(new Menu("Panels"));
            panelsMenu.AddTranslation(Lang.Ru, "Панели");
            panelsMenu.AddTranslation(Lang.Cn, "面板");

            var menu = panelsMenu.Add(new Menu("Item panel"));
            menu.AddTranslation(Lang.Ru, "Панель предметов");
            menu.AddTranslation(Lang.Cn, "物品面板");

            this.show = menu.Add(new MenuSwitcher("Enabled", "enabled", false)).SetTooltip("Show enemy items");
            this.show.AddTranslation(Lang.Ru, "Включено");
            this.show.AddTooltipTranslation(Lang.Ru, "Показывать предметы врагов");
            this.show.AddTranslation(Lang.Cn, "启用");
            this.show.AddTooltipTranslation(Lang.Cn, "显示敌人物品");

            this.showCooldown = menu.Add(new MenuSwitcher("Show cooldowns", "cooldown"));
            this.showCooldown.AddTranslation(Lang.Ru, "Время перезарядки");
            this.showCooldown.AddTranslation(Lang.Cn, "显示冷却时间");

            this.showCharges = menu.Add(new MenuSwitcher("Show charges", "charges"));
            this.showCharges.AddTranslation(Lang.Ru, "Количество чарджей");
            this.showCharges.AddTranslation(Lang.Cn, "显示充能");

            this.ping = menu.Add(new MenuSwitcher("Ping on click").SetTooltip("Ping item to allies"));
            this.ping.AddTranslation(Lang.Ru, "Оповещения");
            this.ping.AddTooltipTranslation(Lang.Ru, "Оповещать союзников при нажатии");
            this.ping.AddTranslation(Lang.Cn, "单击时发出警报");
            this.ping.AddTooltipTranslation(Lang.Cn, "按下時通知盟友");

            var settings = menu.Add(new Menu("Settings"));
            settings.AddTranslation(Lang.Ru, "Настройки");
            settings.AddTranslation(Lang.Cn, "设置");

            this.size = settings.Add(new MenuSlider("Size", "size", 35, 20, 60));
            this.size.AddTranslation(Lang.Ru, "Размер");
            this.size.AddTranslation(Lang.Cn, "大小");

            this.position = new MenuVectorSlider(settings, new Vector2(Hud.Info.ScreenSize.X * 0.71f, Hud.Info.ScreenSize.Y * 0.82f));

            var keys = menu.Add(new Menu("Keys"));
            keys.AddTranslation(Lang.Ru, "Клавишы");
            keys.AddTranslation(Lang.Cn, "键");

            this.toggleKey = keys.Add(new MenuToggleKey("Toggle key", "toggle")).SetTooltip("Show/hide items panel");
            this.toggleKey.AddTranslation(Lang.Ru, "Клавиша переключения");
            this.toggleKey.AddTooltipTranslation(Lang.Ru, "Показать/спрятать панель");
            this.toggleKey.AddTranslation(Lang.Cn, "切换键");
            this.toggleKey.AddTooltipTranslation(Lang.Cn, "显示/隐藏项目面板");

            this.holdKey = keys.Add(new MenuHoldKey("Hold key", "hold")).SetTooltip("Show/hide items panel");
            this.holdKey.AddTranslation(Lang.Ru, "Клавиша удержания");
            this.holdKey.AddTooltipTranslation(Lang.Ru, "Показать/спрятать панель");
            this.holdKey.AddTranslation(Lang.Cn, "按住键");
            this.holdKey.AddTooltipTranslation(Lang.Cn, "显示/隐藏项目面板");
        }

        public void Activate()
        {
            this.LoadTextures();

            this.ownerTeam = EntityManager9.Owner.Team;
            this.show.ValueChange += this.ShowOnValueChange;
            this.size.ValueChange += this.SizeOnValueChange;
        }

        public void Dispose()
        {
            this.size.ValueChange -= this.SizeOnValueChange;
            this.show.ValueChange -= this.ShowOnValueChange;
            this.toggleKey.ValueChange -= this.ToggleKeyOnValueChange;
            this.holdKey.ValueChange -= this.HoldKeyOnValueChange;
            this.ping.ValueChange -= this.PingOnValueChange;
            this.input.MouseKeyUp -= this.InputOnMouseKeyDown;
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
            this.context.Renderer.Draw -= this.OnDraw;
            this.position.Dispose();
            this.units.Clear();
        }

        private void HoldKeyOnValueChange(object sender, KeyEventArgs e)
        {
            this.toggleKey.IsActive = e.NewValue;
        }

        private void InputOnMouseKeyDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.MouseKey != MouseKey.Left)
                {
                    return;
                }

                var startPosition = this.position.Value;
                var itemPanel = new Rectangle9(
                    startPosition,
                    new Vector2(this.heroSize.X + ((this.itemSize.X + 1) * 7), this.heroSize.Y * this.units.Count));

                if (!itemPanel.Contains(e.ScreenPosition))
                {
                    return;
                }

                foreach (var unit in this.units)
                {
                    if (!unit.IsValid)
                    {
                        continue;
                    }

                    var borderPosition = new Rectangle9(
                        startPosition.X + this.heroSize.X + 1,
                        startPosition.Y,
                        this.itemSize.X,
                        this.itemSize.Y);

                    foreach (var ability in unit.Abilities.OrderBy(x => x.Id == AbilityId.item_tpscroll))
                    {
                        if (ability.Id == AbilityId.item_tpscroll)
                        {
                            continue;
                        }

                        if (!ability.IsItem || !ability.IsUsable)
                        {
                            continue;
                        }

                        var itemPosition = borderPosition - 4;

                        if (itemPosition.Contains(e.ScreenPosition))
                        {
                            ability.BaseAbility.Announce();
                            return;
                        }

                        borderPosition += new Vector2(this.itemSize.X + 1, 0);
                    }

                    startPosition += new Vector2(0, this.size + 1);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void LoadTextures()
        {
            var tm = this.context.Renderer.TextureManager;

            tm.LoadFromDota("o9k.inventory_item_bg", @"panorama\images\hud\reborn\inventory_item_well_psd.vtex_c");
            tm.LoadFromDota(
                "o9k.inventory_tp_cd_bg",
                @"panorama\images\masks\softedge_circle_sharp_png.vtex_c",
                new TextureProperties
                {
                    ColorRatio = new Vector4(0f, 0f, 0f, 0.8f)
                });
            tm.LoadFromDota(
                "o9k.inventory_item_cd_bg",
                @"panorama\images\masks\softedge_horizontal_png.vtex_c",
                new TextureProperties
                {
                    ColorRatio = new Vector4(0f, 0f, 0f, 0.6f)
                });
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                var heroPosition = this.position.Value;

                foreach (var unit in this.units)
                {
                    if (!unit.IsValid)
                    {
                        continue;
                    }

                    renderer.DrawTexture(unit.TextureName, heroPosition, this.heroSize);

                    var borderPosition = new Rectangle9(
                        heroPosition.X + this.heroSize.X + 1,
                        heroPosition.Y,
                        this.itemSize.X,
                        this.itemSize.Y);

                    for (var i = 0; i < 7; i++)
                    {
                        renderer.DrawTexture("o9k.inventory_item_bg", borderPosition + new Vector2((this.itemSize.X + 1) * i, 0));
                    }

                    foreach (var ability in unit.Abilities.OrderBy(x => x.Id == AbilityId.item_tpscroll))
                    {
                        if (ability.Id == AbilityId.item_tpscroll)
                        {
                            var tpPosition = new Rectangle9(
                                heroPosition + new Vector2(this.heroSize.X * 0.65f, this.heroSize.Y * 0.4f),
                                this.itemSize.X * 0.6f,
                                this.itemSize.X * 0.6f);

                            renderer.DrawTexture("o9k.outline", tpPosition + 1);
                            renderer.DrawTexture(ability.TextureName + "_rounded", tpPosition);

                            if (this.showCooldown)
                            {
                                var cooldown = ability.RemainingCooldown;
                                if (cooldown > 0)
                                {
                                    renderer.DrawTexture("o9k.inventory_tp_cd_bg", tpPosition);
                                    renderer.DrawText(
                                        tpPosition,
                                        Math.Ceiling(cooldown).ToString("N0"),
                                        Color.White,
                                        RendererFontFlags.Center | RendererFontFlags.VerticalCenter,
                                        this.size * 0.35f);
                                }
                            }

                            continue;
                        }

                        if (!ability.IsItem || !ability.IsUsable)
                        {
                            continue;
                        }

                        var itemPosition = borderPosition - 4;

                        renderer.DrawTexture(ability.TextureName, itemPosition);

                        if (this.showCharges && ability.IsDisplayingCharges)
                        {
                            var chargesText = ability.BaseItem.CurrentCharges.ToString("N0");
                            var chargesSize = renderer.MeasureText(chargesText, this.size * 0.35f);
                            var chargesPosition = itemPosition.SinkToBottomRight(chargesSize.X * 1.1f, chargesSize.Y * 0.8f);

                            renderer.DrawFilledRectangle(chargesPosition, Color.Black);
                            renderer.DrawText(chargesPosition, chargesText, Color.White, RendererFontFlags.Left, this.size * 0.35f);
                        }

                        if (this.showCooldown)
                        {
                            var cooldown = ability.RemainingCooldown;
                            if (cooldown > 0)
                            {
                                renderer.DrawTexture("o9k.inventory_item_cd_bg", itemPosition);
                                renderer.DrawText(
                                    itemPosition,
                                    Math.Ceiling(cooldown).ToString("N0"),
                                    Color.White,
                                    RendererFontFlags.Center | RendererFontFlags.VerticalCenter,
                                    this.size * 0.7f);
                            }
                        }

                        borderPosition += new Vector2(this.itemSize.X + 1, 0);
                    }

                    heroPosition += new Vector2(0, this.size + 1);
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

        private void OnUnitAdded(Unit9 unit)
        {
            try
            {
                if (!unit.IsHero || unit.IsIllusion || unit.Team == this.ownerTeam)
                {
                    return;
                }

                if (unit is Meepo meepo && !meepo.IsMainMeepo)
                {
                    return;
                }

                this.units.Add(unit);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitRemoved(Unit9 unit)
        {
            try
            {
                if (!unit.IsHero || unit.IsIllusion || unit.Team == this.ownerTeam)
                {
                    return;
                }

                this.units.Remove(unit);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void PingOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.input.MouseKeyUp += this.InputOnMouseKeyDown;
            }
            else
            {
                this.input.MouseKeyUp -= this.InputOnMouseKeyDown;
            }
        }

        private void ShowOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                EntityManager9.UnitAdded += this.OnUnitAdded;
                EntityManager9.UnitRemoved += this.OnUnitRemoved;
                this.toggleKey.ValueChange += this.ToggleKeyOnValueChange;
                this.holdKey.ValueChange += this.HoldKeyOnValueChange;
            }
            else
            {
                EntityManager9.UnitAdded -= this.OnUnitAdded;
                EntityManager9.UnitRemoved -= this.OnUnitRemoved;
                this.toggleKey.ValueChange -= this.ToggleKeyOnValueChange;
                this.holdKey.ValueChange -= this.HoldKeyOnValueChange;
                this.input.MouseKeyUp -= this.InputOnMouseKeyDown;
                this.ping.ValueChange -= this.PingOnValueChange;
                this.context.Renderer.Draw -= this.OnDraw;
                this.units.Clear();
            }
        }

        private void SizeOnValueChange(object sender, SliderEventArgs e)
        {
            this.heroSize = new Vector2(e.NewValue * 1.6f, e.NewValue);
            this.itemSize = new Vector2(e.NewValue, e.NewValue);
        }

        private void ToggleKeyOnValueChange(object sender, KeyEventArgs e)
        {
            if (e.NewValue)
            {
                this.context.Renderer.Draw += this.OnDraw;
                this.ping.ValueChange += this.PingOnValueChange;
            }
            else
            {
                this.ping.ValueChange -= this.PingOnValueChange;
                this.input.MouseKeyUp -= this.InputOnMouseKeyDown;
                this.context.Renderer.Draw -= this.OnDraw;
            }
        }
    }
}