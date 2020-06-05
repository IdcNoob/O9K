namespace O9K.Hud.Modules.Units.Modifiers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Units;
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
    using Ensage.SDK.Renderer.Texture;

    using Helpers;

    using MainMenu;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal partial class Modifiers : IHudModule
    {
        private readonly IContext9 context;

        private readonly MenuSwitcher enabled;

        private readonly MenuSwitcher ignoreAuras;

        private readonly MenuSwitcher ignoreHiddenAuras;

        private readonly MenuSwitcher ignoreUnknownTime;

        private readonly HashSet<string> loadedTextures = new HashSet<string>
        {
            "phantom_assassin_armor_corruption_debuff" // no texture
        };

        private readonly MenuVectorSlider position;

        private readonly MenuSwitcher showAlly;

        private readonly MenuSwitcher showTime;

        private readonly MenuSlider size;

        private readonly MenuSlider textSize;

        private readonly List<ModifierUnit> units = new List<ModifierUnit>();

        [ImportingConstructor]
        public Modifiers(IContext9 context, IHudMenu hudMenu)
        {
            this.context = context;
            var menu = hudMenu.UnitsMenu.Add(new Menu("Modifiers"));
            menu.AddTranslation(Lang.Ru, "Баффы/дебаффы");
            menu.AddTranslation(Lang.Cn, "特效");

            this.enabled = menu.Add(new MenuSwitcher("Enabled")).SetTooltip("Show buffs/debuffs");
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTooltipTranslation(Lang.Ru, "Показывать баффы/дебаффы");
            this.enabled.AddTranslation(Lang.Cn, "启用");
            this.enabled.AddTooltipTranslation(Lang.Cn, "显示特效");

            this.showTime = menu.Add(new MenuSwitcher("Show remaining time", false));
            this.showTime.AddTranslation(Lang.Ru, "Показывать оставшееся время");
            this.showTime.AddTranslation(Lang.Cn, "显示剩余时间");

            this.showAlly = menu.Add(new MenuSwitcher("Show ally modifiers"));
            this.showAlly.AddTranslation(Lang.Ru, "Показывать баффы/дебаффы союзников");
            this.showAlly.AddTranslation(Lang.Cn, "显示盟友特殊特效");

            var settings = menu.Add(new Menu("Settings"));
            settings.AddTranslation(Lang.Ru, "Настройки");
            settings.AddTranslation(Lang.Cn, "设置");

            this.position = new MenuVectorSlider(settings, new Vector3(0, -100, 100), new Vector3(0, -100, 100));
            this.size = settings.Add(new MenuSlider("Size", 30, 20, 50));
            this.size.AddTranslation(Lang.Ru, "Размер");
            this.size.AddTranslation(Lang.Cn, "大小");

            this.textSize = settings.Add(new MenuSlider("Time size", 16, 10, 35));
            this.textSize.AddTranslation(Lang.Ru, "Размер времени");
            this.textSize.AddTranslation(Lang.Cn, "文本大小");

            var ignoreMenu = menu.Add(new Menu("Ignore"));
            ignoreMenu.AddTranslation(Lang.Ru, "Игнорировать");
            ignoreMenu.AddTranslation(Lang.Cn, "忽略");

            this.ignoreAuras = ignoreMenu.Add(new MenuSwitcher("Ignore auras").SetTooltip("Lunar blessing, feral impulse etc."));
            this.ignoreAuras.AddTranslation(Lang.Ru, "Игнорировать ауры");
            this.ignoreAuras.AddTooltipTranslation(Lang.Ru, "Lunar blessing, feral impulse и т.д.");
            this.ignoreAuras.AddTranslation(Lang.Cn, "忽略光环");
            this.ignoreAuras.AddTooltipTranslation(
                Lang.Cn,
                LocalizationHelper.LocalizeName(AbilityId.luna_lunar_blessing) + ","
                                                                               + LocalizationHelper.LocalizeName(
                                                                                   AbilityId.lycan_feral_impulse) + "等");

            this.ignoreHiddenAuras = ignoreMenu.Add(
                new MenuSwitcher("Ignore hidden auras", false).SetTooltip("True sight, presence of dark lord etc."));
            this.ignoreHiddenAuras.AddTranslation(Lang.Ru, "Игнорировать скрытые ауры");
            this.ignoreHiddenAuras.AddTooltipTranslation(Lang.Ru, "True sight, presence of dark lord и т.д.");
            this.ignoreHiddenAuras.AddTranslation(Lang.Cn, "忽略隐藏光环");
            this.ignoreHiddenAuras.AddTooltipTranslation(
                Lang.Cn,
                "真实视域," + LocalizationHelper.LocalizeName(AbilityId.nevermore_dark_lord) + "等");

            this.ignoreUnknownTime = ignoreMenu.Add(
                new MenuSwitcher("Ignore undefined remaining time", false).SetTooltip("Ice vortex, static storm, pulse nova etc."));
            this.ignoreUnknownTime.AddTranslation(Lang.Ru, "Игнорировать если время не определено");
            this.ignoreUnknownTime.AddTooltipTranslation(Lang.Ru, "Ice vortex, static storm, pulse nova и т.д.");
            this.ignoreUnknownTime.AddTranslation(Lang.Cn, "忽略未定义的剩余时间");
            this.ignoreUnknownTime.AddTooltipTranslation(
                Lang.Cn,
                LocalizationHelper.LocalizeName(AbilityId.ancient_apparition_ice_vortex) + ","
                                                                                         + LocalizationHelper.LocalizeName(
                                                                                             AbilityId.disruptor_static_storm) + ","
                                                                                         + LocalizationHelper.LocalizeName(
                                                                                             AbilityId.leshrac_pulse_nova) + "等");
        }

        public void Activate()
        {
            this.LoadTextures();

            this.enabled.ValueChange += this.EnabledOnValueChange;
        }

        public void Dispose()
        {
            this.position.Dispose();
            this.enabled.ValueChange -= this.EnabledOnValueChange;
            this.ignoreAuras.ValueChange -= this.IgnoreOnValueChange;
            this.ignoreUnknownTime.ValueChange -= this.IgnoreOnValueChange;
            this.ignoreHiddenAuras.ValueChange -= this.IgnoreOnValueChange;
            this.context.Renderer.Draw -= this.OnDraw;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            Unit.OnModifierAdded -= this.OnModifierAdded;
            Unit.OnModifierRemoved -= this.OnModifierRemoved;
            UpdateManager.Unsubscribe(this.OnUpdate);

            this.units.Clear();
            this.loadedTextures.Clear();
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                EntityManager9.UnitAdded += this.OnUnitAdded;
                EntityManager9.UnitRemoved += this.OnUnitRemoved;
                Unit.OnModifierAdded += this.OnModifierAdded;
                Unit.OnModifierRemoved += this.OnModifierRemoved;
                UpdateManager.Subscribe(this.OnUpdate, 500);
                this.context.Renderer.Draw += this.OnDraw;
                this.ignoreAuras.ValueChange += this.IgnoreOnValueChange;
                this.ignoreUnknownTime.ValueChange += this.IgnoreOnValueChange;
                this.ignoreHiddenAuras.ValueChange += this.IgnoreOnValueChange;
            }
            else
            {
                EntityManager9.UnitAdded -= this.OnUnitAdded;
                EntityManager9.UnitRemoved -= this.OnUnitRemoved;
                Unit.OnModifierAdded -= this.OnModifierAdded;
                Unit.OnModifierRemoved -= this.OnModifierRemoved;
                UpdateManager.Unsubscribe(this.OnUpdate);
                this.context.Renderer.Draw -= this.OnDraw;
                this.ignoreAuras.ValueChange -= this.IgnoreOnValueChange;
                this.ignoreUnknownTime.ValueChange -= this.IgnoreOnValueChange;
                this.ignoreHiddenAuras.ValueChange -= this.IgnoreOnValueChange;
                this.units.Clear();
            }
        }

        private void IgnoreOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue == e.OldValue)
            {
                return;
            }

            UpdateManager.BeginInvoke(
                () =>
                    {
                        this.units.Clear();
                        EntityManager9.UnitAdded -= this.OnUnitAdded;
                        EntityManager9.UnitAdded += this.OnUnitAdded;
                    });
        }

        private bool LoadModifierTexture(Modifier modifier)
        {
            if (modifier.Name == "modifier_truesight")
            {
                return true;
            }

            var textureKey = modifier.TextureName;

            if (string.IsNullOrEmpty(textureKey))
            {
                return false;
            }

            if (this.loadedTextures.Contains(textureKey))
            {
                return true;
            }

            this.loadedTextures.Add(textureKey);

            var isItem = textureKey.StartsWith("item_");
            var textureFolder = isItem ? "items" : "spellicons";
            var textureName = isItem ? textureKey.Substring("item_".Length) : textureKey.Replace('/', '\\');
            var file = $@"panorama\images\{textureFolder}\{textureName}_png.vtex_c";
            this.context.Renderer.TextureManager.LoadFromDota(textureKey + "_rounded", file, TextureProperties.Round);

            return true;
        }

        private void LoadTextures()
        {
            var tm = this.context.Renderer.TextureManager;

            tm.LoadFromResource(
                "o9k.modifier_truesight_rounded",
                "modifier_truesight.png",
                new TextureProperties
                {
                    Rounded = true
                });
            tm.LoadFromDota(
                "o9k.modifier_bg",
                @"panorama\images\masks\softedge_circle_sharp_png.vtex_c",
                new TextureProperties
                {
                    ColorRatio = new Vector4(0f, 0f, 0f, 0.45f)
                });
            tm.LoadFromDota(
                "o9k.outline_green",
                @"panorama\images\hud\reborn\buff_outline_psd.vtex_c",
                new TextureProperties
                {
                    ColorRatio = new Vector4(0f, 0.9f, 0f, 1f)
                });
            tm.LoadFromDota(
                "o9k.outline_red",
                @"panorama\images\hud\reborn\buff_outline_psd.vtex_c",
                new TextureProperties
                {
                    ColorRatio = new Vector4(0.9f, 0f, 0f, 1f)
                });
            tm.LoadFromDota(
                "o9k.outline_yellow",
                @"panorama\images\hud\reborn\buff_outline_psd.vtex_c",
                new TextureProperties
                {
                    ColorRatio = new Vector4(0.9f, 0.9f, 0f, 1f),
                    Brightness = 50
                });
            tm.LoadFromDota(
                "o9k.outline_black",
                @"panorama\images\hud\reborn\buff_outline_psd.vtex_c",
                new TextureProperties
                {
                    ColorRatio = new Vector4(0f, 0f, 0f, 1f),
                    Sliced = true
                });
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                foreach (var unit in this.units)
                {
                    if (!unit.IsValid(this.showAlly))
                    {
                        continue;
                    }

                    var hpPosition = unit.HealthBarPosition;
                    if (hpPosition.IsZero)
                    {
                        continue;
                    }

                    var start = new Rectangle9(hpPosition.X, hpPosition.Y + 55, this.size, this.size) + this.position;

                    foreach (var modifier in unit.Modifiers.ToArray())
                    {
                        renderer.DrawTexture(modifier.TextureName, start);

                        if (!modifier.IgnoreTime)
                        {
                            var remainingTime = modifier.RemainingTime;

                            if (this.showTime)
                            {
                                var timePosition = start * 1.5f;
                                renderer.DrawTexture("o9k.modifier_bg", timePosition);
                                renderer.DrawText(
                                    timePosition,
                                    remainingTime < 10 ? remainingTime.ToString("N1") : remainingTime.ToString("N0"),
                                    Color.White,
                                    RendererFontFlags.Center | RendererFontFlags.VerticalCenter,
                                    this.textSize);
                            }

                            var pct = (int)(100 - ((remainingTime / modifier.Duration) * 100));
                            var outlinePosition = start * 1.17f;

                            renderer.DrawTexture(modifier.IsDebuff ? "o9k.outline_red" : "o9k.outline_green", outlinePosition);
                            renderer.DrawTexture("o9k.outline_black" + pct, outlinePosition);
                        }
                        else
                        {
                            renderer.DrawTexture(modifier.IsDebuff ? "o9k.outline_red" : "o9k.outline_green", start * 1.17f);
                        }

                        start += new Vector2(0, this.size + 5);
                    }
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

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                var modifier = args.Modifier;
                if (!modifier.IsValid)
                {
                    return;
                }

                if (!this.data.TryGetValue(modifier.Name, out var type))
                {
                    return;
                }

                if (modifier.IsHidden)
                {
                    if (this.ignoreHiddenAuras || type != ModifierType.Aura)
                    {
                        return;
                    }
                }
                else
                {
                    if (this.ignoreAuras && type == ModifierType.Aura)
                    {
                        return;
                    }

                    if (this.ignoreUnknownTime && type == ModifierType.TemporaryNoTime)
                    {
                        return;
                    }
                }

                var modifierUnit = this.units.Find(x => x.Unit.Handle == sender.Handle);
                if (modifierUnit == null)
                {
                    return;
                }

                if (!this.LoadModifierTexture(modifier))
                {
                    return;
                }

                modifierUnit.AddModifier(new DrawableModifier(modifier, type));
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
                var unit = this.units.Find(x => x.Unit.Handle == sender.Handle);
                unit?.RemoveModifier(args.Modifier);
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
                if ((!unit.IsHero && !this.unitNames.Contains(unit.Name)) || unit.IsIllusion)
                {
                    return;
                }

                this.units.Add(new ModifierUnit(unit));

                foreach (var modifier in unit.BaseModifiers)
                {
                    this.OnModifierAdded(unit, new ModifierChangedEventArgs(modifier));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitRemoved(Unit9 entity)
        {
            try
            {
                var modifierUnit = this.units.Find(x => x.Unit.Handle == entity.Handle);
                if (modifierUnit == null)
                {
                    return;
                }

                this.units.Remove(modifierUnit);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUpdate()
        {
            if (Game.IsPaused)
            {
                return;
            }

            try
            {
                for (var i = this.units.Count - 1; i > -1; i--)
                {
                    var unit = this.units[i];

                    if (!unit.Unit.IsValid)
                    {
                        this.units.RemoveAt(i);
                        continue;
                    }

                    unit.CheckModifiers();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}