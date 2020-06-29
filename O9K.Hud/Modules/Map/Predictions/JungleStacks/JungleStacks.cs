namespace O9K.Hud.Modules.Map.Predictions.JungleStacks
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer;

    using Helpers;

    using MainMenu;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal class JungleStacks : IHudModule
    {
        private readonly IContext9 context;

        private readonly HashSet<string> doubleCreeps = new HashSet<string>
        {
            "npc_dota_neutral_satyr_soulstealer",
            "npc_dota_neutral_mud_golem",
            "npc_dota_neutral_gnoll_assassin"
        };

        private readonly IMinimap minimap;

        private readonly MenuSwitcher showOnMinimap;

        private readonly HashSet<string> singleCreeps = new HashSet<string>
        {
            "npc_dota_neutral_harpy_storm",
            "npc_dota_neutral_ghost",
            "npc_dota_neutral_forest_troll_high_priest",
            "npc_dota_neutral_kobold_taskmaster",
            "npc_dota_neutral_alpha_wolf",
            "npc_dota_neutral_ogre_magi",
            "npc_dota_neutral_satyr_hellcaller",
            "npc_dota_neutral_centaur_khan",
            "npc_dota_neutral_dark_troll_warlord",
            "npc_dota_neutral_enraged_wildkin",
            "npc_dota_neutral_polar_furbolg_ursa_warrior",
            "npc_dota_neutral_big_thunder_lizard",
            "npc_dota_neutral_black_dragon",
            "npc_dota_neutral_granite_golem"
        };

        private readonly Dictionary<Vector3, float> stacks = new Dictionary<Vector3, float>();

        private Vector3[] campPositions;

        [ImportingConstructor]
        public JungleStacks(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
        {
            this.context = context;
            this.minimap = minimap;

            var predictionsMenu = hudMenu.MapMenu.GetOrAdd(new Menu("Predictions"));
            predictionsMenu.AddTranslation(Lang.Ru, "Предположения");
            predictionsMenu.AddTranslation(Lang.Cn, "预测");

            var menu = predictionsMenu.Add(new Menu("Jungle stacks"));
            menu.AddTranslation(Lang.Ru, "Стаки");
            menu.AddTranslation(Lang.Cn, "丛林堆栈");

            this.showOnMinimap = menu.Add(new MenuSwitcher("Show on minimap")).SetTooltip("Show predicted stacks on minimap");
            this.showOnMinimap.AddTranslation(Lang.Ru, "Показывать на миникарте");
            this.showOnMinimap.AddTooltipTranslation(Lang.Ru, "Показывать предполагаемое количество стаков в лесу");
            this.showOnMinimap.AddTranslation(Lang.Cn, "小地图上显示");
            this.showOnMinimap.AddTooltipTranslation(Lang.Cn, "在小地图上显示预测的兵线");
        }

        public void Activate()
        {
            this.campPositions = this.context.JungleManager.JungleCamps.Select(x => x.CreepsPosition).ToArray();

            this.showOnMinimap.ValueChange += this.ShowOnMinimapOnValueChange;
        }

        public void Dispose()
        {
            this.showOnMinimap.ValueChange -= this.ShowOnMinimapOnValueChange;
            this.context.Renderer.Draw -= this.OnDraw;
            UpdateManager.Unsubscribe(this.OnUpdate);
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                foreach (var stack in this.stacks)
                {
                    if (stack.Value <= 1)
                    {
                        continue;
                    }

                    var size = 16 * Hud.Info.ScreenRatio;
                    var minimapPosition = this.minimap.WorldToMinimap(stack.Key, size);

                    renderer.DrawText(
                        minimapPosition + new Size2F(size, 0),
                        stack.Value.ToString(),
                        Color.Orange,
                        RendererFontFlags.Left,
                        size);
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

        private void OnUpdate()
        {
            try
            {
                var neutrals = EntityManager9.Units.Where(x => x.Team == Team.Neutral && x.BaseUnit.IsAlive).ToArray();

                foreach (var camp in this.campPositions)
                {
                    var units = neutrals.Where(x => x.Position.Distance2D(camp) < 500).ToList();
                    var count = units.Count(x => this.singleCreeps.Contains(x.Name))
                                + (units.Count(x => this.doubleCreeps.Contains(x.Name)) / 2);

                    this.stacks[camp] = count - 1;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void ShowOnMinimapOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                UpdateManager.Subscribe(this.OnUpdate, 5000);
                this.context.Renderer.Draw += this.OnDraw;
            }
            else
            {
                UpdateManager.Unsubscribe(this.OnUpdate);
                this.context.Renderer.Draw -= this.OnDraw;
            }
        }
    }
}