namespace O9K.Hud.Modules.Screen.Time
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Windows.Input;

    using Core.Entities.Heroes;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer;
    using Ensage.SDK.Renderer.Texture;

    using Helpers;

    using MainMenu;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal class MoveTime : IHudModule
    {
        private readonly IContext9 context;

        private readonly MenuHoldKey key;

        private readonly MenuVectorSlider textPosition;

        private readonly MenuSlider textSize;

        private int moveTime;

        private Owner owner;

        private NavMeshPathfinding pathfinder;

        private IUpdateHandler updateHandler;

        [ImportingConstructor]
        public MoveTime(IContext9 context, IHudMenu hudMenu)
        {
            this.context = context;

            var timeMenu = hudMenu.ScreenMenu.GetOrAdd(new Menu("Time"));
            timeMenu.AddTranslation(Lang.Ru, "Время");
            timeMenu.AddTranslation(Lang.Cn, "时间");

            var menu = timeMenu.Add(new Menu("Move time"));
            menu.AddTranslation(Lang.Ru, "Время движения");
            menu.AddTranslation(Lang.Cn, "移动时间");

            this.key = menu.Add(
                new MenuHoldKey("Key", Key.LeftAlt).SetTooltip("Show approximate hero move time to mouse cursor's position"));
            this.key.AddTranslation(Lang.Ru, "Клавиша");
            this.key.AddTooltipTranslation(Lang.Ru, "Показать преблизительное время движения героя до курсора мыши");
            this.key.AddTranslation(Lang.Cn, "键");
            this.key.AddTooltipTranslation(Lang.Cn, "显示英雄移动到鼠标光标位置的大概时间");

            var settings = menu.Add(new Menu("Settings"));
            settings.AddTranslation(Lang.Ru, "Настройки");
            settings.AddTranslation(Lang.Cn, "设置");

            this.textSize = settings.Add(new MenuSlider("Size", 17, 10, 35));
            this.textSize.AddTranslation(Lang.Ru, "Размер");
            this.textSize.AddTranslation(Lang.Cn, "大小");

            this.textPosition = new MenuVectorSlider(
                settings,
                new Vector3(34 * Hud.Info.ScreenRatio, -300, 300),
                new Vector3(10 * Hud.Info.ScreenRatio, -300, 300));
        }

        public void Activate()
        {
            this.owner = EntityManager9.Owner;

            this.context.Renderer.TextureManager.LoadFromDota(
                "o9k.waypoint_white",
                @"panorama\images\hud\reborn\ping_icon_waypoint_psd.vtex_c",
                new TextureProperties
                {
                    ColorRatio = new Vector4(1f, 1f, 1f, 1f)
                });

            this.pathfinder = new NavMeshPathfinding();
            this.updateHandler = UpdateManager.Subscribe(this.OnUpdate, 300, false);
            this.key.ValueChange += this.KeyOnValueChange;
        }

        public void Dispose()
        {
            this.pathfinder.Dispose();
            this.textPosition.Dispose();
            this.pathfinder.Dispose();
            this.key.ValueChange -= this.KeyOnValueChange;
            this.context.Renderer.Draw -= this.OnDraw;
            UpdateManager.Unsubscribe(this.updateHandler);
        }

        private void KeyOnValueChange(object sender, KeyEventArgs e)
        {
            if (e.NewValue)
            {
                this.pathfinder.UpdateNavMesh();
                this.updateHandler.IsEnabled = true;
                this.context.Renderer.Draw += this.OnDraw;
            }
            else
            {
                this.context.Renderer.Draw -= this.OnDraw;
                this.updateHandler.IsEnabled = false;
                this.moveTime = 0;
            }
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                if (this.moveTime <= 0)
                {
                    return;
                }

                var position = Game.MouseScreenPosition + this.textPosition;
                var size = this.textSize * Hud.Info.ScreenRatio;
                var textureSize = new Vector2(size * 1.8f);

                renderer.DrawTexture("o9k.waypoint_white", position - new Vector2(0, textureSize.Y * 0.15f), textureSize);
                renderer.DrawText(position + new Vector2(textureSize.X, 0), this.moveTime + "s", Color.White, size);
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
                var hero = this.owner.Hero;
                if (hero.Speed <= 0)
                {
                    this.moveTime = 0;
                    return;
                }

                var position = hero.Position;
                var path = this.pathfinder.CalculateStaticLongPath(position, Game.MousePosition, 999999, true, out var completed).ToArray();

                if (!completed || path.Length == 0)
                {
                    return;
                }

                var time = 0f;

                foreach (var vector3 in path)
                {
                    time += position.Distance2D(vector3) / hero.Speed;
                    position = vector3;
                }

                this.moveTime = (int)Math.Ceiling(time + hero.GetTurnTime(path.Last()) + (Game.Ping / 1000f));
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}