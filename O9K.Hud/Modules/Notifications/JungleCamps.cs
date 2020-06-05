namespace O9K.Hud.Modules.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Drawing;
    using System.Linq;

    using Core.Data;
    using Core.Entities.Heroes;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Jungle;
    using Core.Managers.Jungle.Camp;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer;

    using MainMenu;

    internal class JungleCamps : IHudModule
    {
        private readonly IContext9 context;

        private readonly List<IJungleCamp> drawCamps = new List<IJungleCamp>();

        private readonly MenuSwitcher enabled;

        private readonly IJungleManager jungleManager;

        private bool drawing;

        private Owner owner;

        [ImportingConstructor]
        public JungleCamps(IContext9 context, IHudMenu hudMenu)
        {
            this.context = context;
            this.jungleManager = context.JungleManager;

            var menu = hudMenu.NotificationsMenu.GetOrAdd(new Menu("Jungle stacks"));
            menu.AddTranslation(Lang.Ru, "Стаки");
            menu.AddTranslation(Lang.Cn, "丛林堆栈");

            this.enabled = menu.Add(new MenuSwitcher("Enabled")).SetTooltip("Notify to stack jungle camps");
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTooltipTranslation(Lang.Ru, "Напоминать о стаке нейтральных крипов");
            this.enabled.AddTranslation(Lang.Cn, "启用");
            this.enabled.AddTooltipTranslation(Lang.Cn, "提醒丛林堆栈");
        }

        public void Activate()
        {
            this.owner = EntityManager9.Owner;

            this.enabled.ValueChange += this.Enabled_OnValueChange;
        }

        public void Dispose()
        {
            this.enabled.ValueChange -= this.Enabled_OnValueChange;
            this.context.Renderer.Draw -= this.OnDraw;
            UpdateManager.Unsubscribe(this.OnUpdatePreSpawn);
            UpdateManager.Unsubscribe(this.OnUpdate);
            this.DisableDraw();
        }

        private void DisableDraw()
        {
            if (!this.drawing)
            {
                return;
            }

            this.context.Renderer.Draw -= this.OnDraw;
            this.drawCamps.Clear();
            this.drawing = false;
        }

        private void Enabled_OnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                UpdateManager.Subscribe(this.OnUpdatePreSpawn, 1000);
            }
            else
            {
                UpdateManager.Unsubscribe(this.OnUpdatePreSpawn);
                UpdateManager.Unsubscribe(this.OnUpdate);
                this.DisableDraw();
            }
        }

        private void EnableDraw(IJungleCamp camp)
        {
            if (!this.drawCamps.Contains(camp))
            {
                this.drawCamps.Add(camp);
            }

            if (this.drawing)
            {
                return;
            }

            this.drawing = true;
            this.context.Renderer.Draw += this.OnDraw;
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                var seconds = Game.GameTime % 60;

                foreach (var camp in this.drawCamps)
                {
                    var position = Drawing.WorldToScreen(camp.DrawPosition);
                    if (position.IsZero)
                    {
                        continue;
                    }

                    var stackTime = Math.Ceiling(camp.StackTime - seconds);
                    if (stackTime <= -2)
                    {
                        continue;
                    }

                    var stackTimeText = stackTime <= 0 ? "now" : stackTime.ToString("N0");
                    renderer.DrawText(position, "Stack in: " + stackTimeText, Color.White, 18 * Hud.Info.ScreenRatio);

                    //renderer.DrawText(
                    //    position,
                    //    "Stack in: " + stackTimeText + " " + (camp.StackTime - seconds) + " " + camp.StackTime,
                    //    Color.White,
                    //    18 * Hud.Info.ScreenRatio);
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

        private void OnUpdate()
        {
            try
            {
                var time = Game.GameTime;
                if (time % 60 < 45)
                {
                    this.DisableDraw();
                    return;
                }

                var hero = this.owner.Hero;
                if (!hero.IsAlive)
                {
                    this.DisableDraw();
                    return;
                }

                var position = hero.Position;
                var closestCamp = this.jungleManager.JungleCamps
                    .Where(x => x.Team == hero.Team && !x.IsSmall && x.CreepsPosition.Distance2D(position) < 1000)
                    .OrderBy(x => x.CreepsPosition.DistanceSquared(position))
                    .FirstOrDefault();

                if (closestCamp == null)
                {
                    this.DisableDraw();
                    return;
                }

                this.EnableDraw(closestCamp);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUpdatePreSpawn()
        {
            try
            {
                var time = Game.GameTime;
                if (time < GameData.JungleCreepsSpawnStartTime)
                {
                    return;
                }

                UpdateManager.Unsubscribe(this.OnUpdatePreSpawn);
                UpdateManager.Subscribe(this.OnUpdate, 1000);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}