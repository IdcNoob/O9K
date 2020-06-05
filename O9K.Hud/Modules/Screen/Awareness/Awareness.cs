namespace O9K.Hud.Modules.Screen.Awareness
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Core.Entities.Heroes;
    using Core.Entities.Heroes.Unique;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;
    using Core.Managers.Renderer.Utils;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer;

    using Helpers;

    using Heroes;

    using MainMenu;

    using SharpDX;

    internal class Awareness : IHudModule
    {
        private readonly IContext9 context;

        private readonly MenuSwitcher enabled;

        private readonly List<AwarenessHero> heroes = new List<AwarenessHero>();

        private readonly IMinimap minimap;

        private float margin;

        private Owner owner;

        private Vector2 start;

        private float textureSize;

        [ImportingConstructor]
        public Awareness(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
        {
            this.context = context;
            this.minimap = minimap;

            var menu = hudMenu.ScreenMenu.GetOrAdd(new Menu("Map awareness"));
            menu.AddTranslation(Lang.Ru, "Осведомленность");
            menu.AddTranslation(Lang.Cn, "地图意识");

            this.enabled = menu.Add(new MenuSwitcher("Enabled"));
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTranslation(Lang.Cn, "启用");
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
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
            this.context.Renderer.Draw -= this.OnDraw;
            this.heroes.Clear();
        }

        private void CalculatePosition()
        {
            var map = this.minimap.GetMinimap();
            var width = map.Width;
            var ratio = Hud.Info.ScreenRatio;
            this.textureSize = 30 * ratio;
            var count = this.heroes.Count;
            this.margin = (8 * ratio);
            var offset = (width - ((count * this.textureSize) + ((count - 1) * this.margin))) / 2f;
            this.start = new Vector2(map.X + offset, map.Y - this.textureSize - (this.textureSize / 8f));
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                UpdateManager.Subscribe(this.OnUpdate, 500);
                EntityManager9.UnitAdded += this.OnUnitAdded;
                EntityManager9.UnitRemoved += this.OnUnitRemoved;
                this.context.Renderer.Draw += this.OnDraw;
            }
            else
            {
                UpdateManager.Unsubscribe(this.OnUpdate);
                EntityManager9.UnitAdded -= this.OnUnitAdded;
                EntityManager9.UnitRemoved -= this.OnUnitRemoved;
                this.context.Renderer.Draw -= this.OnDraw;
                this.heroes.Clear();
            }
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                var rec = new Rectangle9(this.start, this.textureSize, this.textureSize);

                foreach (var hero in this.heroes)
                {
                    if (!hero.IsValid)
                    {
                        continue;
                    }

                    renderer.DrawTexture(hero.OutlineTextureName, rec * 1.2f);
                    renderer.DrawTexture(hero.TextureName, rec);

                    rec += new Vector2(this.textureSize + this.margin, 0);
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

        private void OnUnitAdded(Unit9 entity)
        {
            try
            {
                if (!entity.IsHero || entity.IsIllusion || entity.IsAlly(this.owner.Team))
                {
                    return;
                }

                if (entity is Meepo meepo && !meepo.IsMainMeepo)
                {
                    return;
                }

                this.heroes.Add(new AwarenessHero(entity));
                this.CalculatePosition();
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
                if (!entity.IsHero || entity.IsIllusion || entity.IsAlly(this.owner.Team))
                {
                    return;
                }

                var hero = this.heroes.Find(x => x.Handle == entity.Handle);
                if (hero == null)
                {
                    return;
                }

                this.heroes.Remove(hero);
                this.CalculatePosition();
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
                var myHero = this.owner.Hero;
                if (myHero == null)
                {
                    return;
                }

                foreach (var hero in this.heroes)
                {
                    if (!hero.IsValid)
                    {
                        continue;
                    }

                    hero.Update(myHero);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}