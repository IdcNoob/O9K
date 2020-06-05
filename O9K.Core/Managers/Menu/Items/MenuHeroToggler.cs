namespace O9K.Core.Managers.Menu.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.SDK.Renderer;

    using Entities.Heroes;
    using Entities.Units;

    using Entity;

    using EventArgs;

    using Logger;

    using Newtonsoft.Json.Linq;

    using SharpDX;

    using Color = System.Drawing.Color;

    public class MenuHeroToggler : MenuItem
    {
        private readonly bool allies;

        private readonly bool defaultValue;

        private readonly bool enemies;

        private readonly Dictionary<string, bool> heroes = new Dictionary<string, bool>();

        private readonly List<HeroId> loadTextures = new List<HeroId>();

        private readonly Dictionary<string, bool> savedHeroes = new Dictionary<string, bool>();

        public MenuHeroToggler(
            string displayName,
            bool allies = false,
            bool enemies = false,
            bool defaultValue = true,
            bool heroUnique = false)
            : this(displayName, displayName, allies, enemies, defaultValue, heroUnique)
        {
        }

        public MenuHeroToggler(
            string displayName,
            string name,
            bool allies = false,
            bool enemies = false,
            bool defaultValue = true,
            bool heroUnique = false)
            : base(displayName, name, heroUnique)
        {
            this.allies = allies;
            this.enemies = enemies;
            this.defaultValue = defaultValue;
        }

        public event EventHandler<HeroEventArgs> ValueChange;

        public bool IsEnabled(string name)
        {
            this.heroes.TryGetValue(name, out var value);
            return value;
        }

        public MenuHeroToggler SetTooltip(string tooltip)
        {
            this.LocalizedTooltip[Lang.En] = tooltip;
            return this;
        }

        internal override void CalculateSize()
        {
            this.DisplayNameSize = this.Renderer.MeasureText(this.DisplayName, this.MenuStyle.TextSize, this.MenuStyle.Font);
            var width = this.DisplayNameSize.X + this.MenuStyle.LeftIndent + this.MenuStyle.RightIndent + 10
                        + (this.MenuStyle.TextureArrowSize * 2) + (this.heroes.Count * this.MenuStyle.TextureHeroSize.X);
            this.Size = new Vector2(width, this.MenuStyle.Height);
            this.ParentMenu.CalculateWidth();

            this.SizeCalculated = true;
        }

        internal override object GetSaveValue()
        {
            foreach (var ability in this.heroes)
            {
                this.savedHeroes[ability.Key] = ability.Value;
            }

            return this.savedHeroes;
        }

        internal override void Load(JToken token)
        {
            try
            {
                token = token?[this.Name];
                if (token == null)
                {
                    return;
                }

                foreach (var item in token.ToObject<JObject>())
                {
                    var key = item.Key;
                    var value = (bool)item.Value;

                    this.savedHeroes[key] = value;

                    if (this.heroes.ContainsKey(key))
                    {
                        this.heroes[key] = value;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        internal override bool OnMouseRelease(Vector2 position)
        {
            if (this.heroes.Count == 0)
            {
                return false;
            }

            var startPosition = new Vector2(
                (this.Position.X + this.Size.X) - this.MenuStyle.TextureHeroSize.X - 4,
                this.Position.Y + ((this.Size.Y - this.MenuStyle.TextureHeroSize.Y) / 2.2f));

            foreach (var hero in this.heroes.Reverse())
            {
                var abilityPosition = new RectangleF(
                    startPosition.X - 1.5f,
                    startPosition.Y - 1.5f,
                    this.MenuStyle.TextureHeroSize.X + 3,
                    this.MenuStyle.TextureHeroSize.Y + 3);

                if (abilityPosition.Contains(position))
                {
                    var value = this.heroes[hero.Key];
                    this.heroes[hero.Key] = !value;
                    this.ValueChange?.Invoke(this, new HeroEventArgs(hero.Key, !value, value));
                    return true;
                }

                startPosition -= new Vector2(this.MenuStyle.TextureHeroSize.X + 4, 0);
            }

            return false;
        }

        internal override void SetRenderer(IRenderManager renderer)
        {
            base.SetRenderer(renderer);

            foreach (var texture in this.loadTextures)
            {
                this.Renderer.TextureManager.LoadHeroFromDota(texture);
            }

            EntityManager9.UnitAdded += this.OnUnitAdded;
        }

        protected override void Draw(IRenderer renderer)
        {
            base.Draw(renderer);

            //abilities
            var startPosition = new Vector2(
                (this.Position.X + this.Size.X) - this.MenuStyle.TextureHeroSize.X - 4,
                this.Position.Y + ((this.Size.Y - this.MenuStyle.TextureHeroSize.Y) / 2.2f));

            foreach (var ability in this.heroes.Reverse())
            {
                renderer.DrawFilledRectangle(
                    new RectangleF(
                        startPosition.X - 1.5f,
                        startPosition.Y - 1.5f,
                        this.MenuStyle.TextureHeroSize.X + 3,
                        this.MenuStyle.TextureHeroSize.Y + 3),
                    ability.Value ? Color.LightGreen : Color.Red);
                renderer.DrawTexture(
                    ability.Key,
                    new RectangleF(startPosition.X, startPosition.Y, this.MenuStyle.TextureHeroSize.X, this.MenuStyle.TextureHeroSize.Y));

                startPosition -= new Vector2(this.MenuStyle.TextureHeroSize.X + 4, 0);
            }
        }

        private void AddHero(HeroId id)
        {
            var name = id.ToString();

            if (this.heroes.ContainsKey(name))
            {
                return;
            }

            if (this.Renderer == null)
            {
                this.loadTextures.Add(id);
            }
            else
            {
                this.Renderer.TextureManager.LoadHeroFromDota(id);
            }

            if (this.savedHeroes.TryGetValue(name, out var savedValue))
            {
                this.heroes[name] = savedValue;
            }
            else
            {
                this.heroes[name] = this.defaultValue;
            }

            if (this.SizeCalculated)
            {
                this.CalculateSize();
                this.ParentMenu.CalculateWidth();
            }

            if (this.heroes.Count >= 5)
            {
                EntityManager9.UnitAdded -= this.OnUnitAdded;
            }
        }

        private void OnUnitAdded(Unit9 entity)
        {
            try
            {
                if (entity.IsIllusion)
                {
                    return;
                }

                if (!(entity is Hero9 hero))
                {
                    return;
                }

                if ((this.allies && hero.IsAlly()) || (this.enemies && hero.IsEnemy()))
                {
                    this.AddHero(hero.Id);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}