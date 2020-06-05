namespace O9K.Core.Managers.Menu.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Renderer;

    using Entities.Heroes;
    using Entities.Units;

    using Entity;

    using EventArgs;

    using Input.EventArgs;

    using Logger;

    using Newtonsoft.Json.Linq;

    using SharpDX;

    using Color = System.Drawing.Color;

    public class MenuHeroPriorityChanger : MenuItem
    {
        private readonly bool allies;

        private readonly bool defaultValue;

        private readonly bool enemies;

        private readonly Dictionary<string, bool> heroes = new Dictionary<string, bool>();

        private readonly Dictionary<string, int> heroPriority = new Dictionary<string, int>();

        private readonly List<HeroId> loadTextures = new List<HeroId>();

        private readonly Dictionary<string, bool> savedHeroes = new Dictionary<string, bool>();

        private readonly Dictionary<string, int> savedHeroPriority = new Dictionary<string, int>();

        private int autoPriority;

        private EventHandler<EventArgs> change;

        private Vector2 currentMousePosition;

        private bool drag;

        private Vector2 dragAbilityPosition;

        private string dragItem;

        private string dragTargetAbility;

        private bool drawDrag;

        private bool increasePriority;

        private Vector2 mousePressDiff;

        private Vector2 mousePressPosition;

        public MenuHeroPriorityChanger(
            string displayName,
            bool allies = false,
            bool enemies = false,
            bool defaultValue = true,
            bool heroUnique = false)
            : this(displayName, displayName, allies, enemies, defaultValue, heroUnique)
        {
        }

        public MenuHeroPriorityChanger(
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

        public event EventHandler<EventArgs> Change
        {
            add
            {
                value(this, EventArgs.Empty);
                this.change += value;
            }
            remove
            {
                this.change -= value;
            }
        }

        public event EventHandler<HeroPriorityEventArgs> OrderChange;

        public event EventHandler<HeroEventArgs> ValueChange;

        public IEnumerable<string> Heroes
        {
            get
            {
                return this.heroes.Where(x => x.Value).OrderBy(x => this.heroPriority[x.Key]).Select(x => x.Key);
            }
        }

        public void AddHero(HeroId id, bool? value = null, int priority = 0)
        {
            if (this.Renderer == null)
            {
                this.loadTextures.Add(id);
            }
            else
            {
                this.Renderer.TextureManager.LoadHeroFromDota(id);
            }

            this.AddHero(id.ToString(), value, priority);
        }

        public void AddHero(string name, bool? value = null, int priority = 0)
        {
            if (this.heroes.ContainsKey(name))
            {
                return;
            }

            if (this.savedHeroes.TryGetValue(name, out var savedValue))
            {
                this.heroes[name] = savedValue;
            }
            else
            {
                this.heroes[name] = value ?? this.defaultValue;
            }

            if (this.savedHeroPriority.TryGetValue(name, out var savedPriority))
            {
                this.heroPriority[name] = savedPriority;
            }
            else
            {
                if (priority != 0)
                {
                    this.heroPriority[name] = this.TryGetPriority(priority);
                }
                else
                {
                    this.heroPriority[name] = this.autoPriority++;
                }
            }

            if (this.SizeCalculated)
            {
                this.CalculateSize();
            }

            if (this.heroes.Count >= 5)
            {
                EntityManager9.UnitAdded -= this.OnUnitAdded;
            }
        }

        public int GetPriority(string name)
        {
            if (this.heroPriority.TryGetValue(name, out var value))
            {
                return value;
            }

            return 99999;
        }

        public bool IsEnabled(string name)
        {
            this.heroes.TryGetValue(name, out var value);
            return value;
        }

        public MenuHeroPriorityChanger SetTooltip(string tooltip)
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

        internal override MenuItem GetItemUnder(Vector2 position)
        {
            if (this.drag)
            {
                return this;
            }

            return base.GetItemUnder(position);
        }

        internal override object GetSaveValue()
        {
            foreach (var ability in this.heroes)
            {
                this.savedHeroes[ability.Key] = ability.Value;
            }

            foreach (var ability in this.heroPriority)
            {
                this.savedHeroPriority[ability.Key] = ability.Value;
            }

            return new
            {
                Heroes = this.savedHeroes,
                Priority = this.savedHeroPriority
            };
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

                foreach (var item in token["Heroes"].ToObject<JObject>())
                {
                    var key = item.Key;
                    var value = (bool)item.Value;

                    this.savedHeroes[key] = value;

                    if (this.heroes.ContainsKey(key))
                    {
                        this.heroes[key] = value;
                    }
                }

                foreach (var item in token["Priority"].ToObject<JObject>())
                {
                    var key = item.Key;
                    var value = (int)item.Value;

                    this.savedHeroPriority[key] = value;

                    if (value >= this.autoPriority)
                    {
                        this.autoPriority = value + 1;
                    }

                    if (this.heroPriority.ContainsKey(key))
                    {
                        this.heroPriority[key] = value;
                    }
                }

                this.change?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        internal override bool OnMousePress(Vector2 position)
        {
            if (this.heroes.Count == 0)
            {
                return false;
            }

            var startPosition = new Vector2(
                (this.Position.X + this.Size.X) - this.MenuStyle.TextureHeroSize.X - 4,
                this.Position.Y + ((this.Size.Y - this.MenuStyle.TextureHeroSize.Y) / 2.2f));

            foreach (var ability in this.heroes.OrderByDescending(x => this.heroPriority[x.Key]))
            {
                var abilityPosition = new RectangleF(
                    startPosition.X - 1.5f,
                    startPosition.Y - 1.5f,
                    this.MenuStyle.TextureHeroSize.X + 3,
                    this.MenuStyle.TextureHeroSize.Y + 3);

                if (abilityPosition.Contains(position))
                {
                    this.currentMousePosition = position;
                    this.mousePressPosition = position;
                    this.mousePressDiff = position - startPosition;
                    this.dragAbilityPosition = position - this.mousePressDiff;
                    this.dragItem = ability.Key;
                    this.drag = true;

                    this.InputManager.MouseMove += this.OnMouseMove;
                    this.InputManager.MouseKeyUp += this.OnMouseKeyUp;

                    return true;
                }

                startPosition -= new Vector2(this.MenuStyle.TextureHeroSize.X + 4, 0);
            }

            return false;
        }

        internal override bool OnMouseRelease(Vector2 position)
        {
            if (this.heroes.Count == 0)
            {
                return false;
            }

            if (this.drawDrag)
            {
                this.drag = false;
                this.drawDrag = false;

                if (string.IsNullOrEmpty(this.dragTargetAbility) || this.dragTargetAbility == this.dragItem)
                {
                    return false;
                }

                var currentPriority = this.heroPriority[this.dragItem];
                int setPriority;

                if (this.increasePriority)
                {
                    setPriority = this.heroPriority[this.dragTargetAbility] - 1;

                    foreach (var key in this.heroPriority.Where(x => x.Value <= setPriority).Select(x => x.Key).ToList())
                    {
                        this.heroPriority[key]--;
                    }

                    this.heroPriority[this.dragItem] = setPriority;
                    this.increasePriority = false;
                }
                else
                {
                    setPriority = this.heroPriority[this.dragTargetAbility] + 1;

                    foreach (var key in this.heroPriority.Where(x => x.Value >= setPriority).Select(x => x.Key).ToList())
                    {
                        this.heroPriority[key]++;
                    }

                    this.heroPriority[this.dragItem] = setPriority;
                }

                this.autoPriority = this.heroPriority.Values.Max() + 1;
                this.OrderChange?.Invoke(this, new HeroPriorityEventArgs(this.dragItem, setPriority, currentPriority));
                this.change?.Invoke(this, EventArgs.Empty);

                return true;
            }

            var startPosition = new Vector2(
                (this.Position.X + this.Size.X) - this.MenuStyle.TextureHeroSize.X - 4,
                this.Position.Y + ((this.Size.Y - this.MenuStyle.TextureHeroSize.Y) / 2.2f));

            foreach (var ability in this.heroes.OrderByDescending(x => this.heroPriority[x.Key]))
            {
                var abilityPosition = new RectangleF(
                    startPosition.X - 1.5f,
                    startPosition.Y - 1.5f,
                    this.MenuStyle.TextureHeroSize.X + 3,
                    this.MenuStyle.TextureHeroSize.Y + 3);

                if (abilityPosition.Contains(position))
                {
                    var value = this.heroes[ability.Key];
                    this.heroes[ability.Key] = !value;
                    this.ValueChange?.Invoke(this, new HeroEventArgs(ability.Key, !value, value));
                    this.change?.Invoke(this, EventArgs.Empty);

                    return true;
                }

                startPosition -= new Vector2(this.MenuStyle.TextureHeroSize.X + 4, 0);
            }

            return false;
        }

        internal override void Remove()
        {
            if (this.InputManager == null)
            {
                return;
            }

            this.InputManager.MouseKeyUp -= this.OnMouseKeyUp;
            this.InputManager.MouseMove -= this.OnMouseMove;
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

            //drag ability
            if (this.drawDrag)
            {
                renderer.DrawTexture(
                    this.dragItem,
                    new RectangleF(
                        this.dragAbilityPosition.X,
                        this.dragAbilityPosition.Y,
                        this.MenuStyle.TextureHeroSize.X,
                        this.MenuStyle.TextureHeroSize.Y));
                this.dragTargetAbility = null;
            }

            //heroes
            var startPosition = new Vector2(
                (this.Position.X + this.Size.X) - this.MenuStyle.TextureHeroSize.X - 4,
                this.Position.Y + ((this.Size.Y - this.MenuStyle.TextureHeroSize.Y) / 2.2f));

            var priority = this.heroes.Count(x => x.Value);
            var count = 0;
            foreach (var ability in this.heroes.OrderByDescending(x => this.heroPriority[x.Key]))
            {
                count++;

                if (this.drawDrag)
                {
                    var border = 3;
                    if (ability.Key == this.dragItem)
                    {
                        border = 0;
                    }

                    if ((count == 1 && this.currentMousePosition.X > this.Position.X + this.Size.X)
                        || (this.currentMousePosition.X >= startPosition.X - border && this.currentMousePosition.X
                            <= startPosition.X + this.MenuStyle.TextureHeroSize.X + border))
                    {
                        this.dragTargetAbility = ability.Key;
                        startPosition -= new Vector2(this.MenuStyle.TextureHeroSize.X + 4, 0);
                        this.increasePriority = false;
                    }

                    if (ability.Key == this.dragItem)
                    {
                        if (ability.Value)
                        {
                            priority--;
                        }

                        continue;
                    }
                }

                renderer.DrawRectangle(
                    new RectangleF(
                        startPosition.X - 1.5f,
                        startPosition.Y - 1.5f,
                        this.MenuStyle.TextureHeroSize.X + 3,
                        this.MenuStyle.TextureHeroSize.Y + 3),
                    ability.Value ? Color.LightGreen : Color.Red,
                    1.5f);
                renderer.DrawTexture(
                    ability.Key,
                    new RectangleF(startPosition.X, startPosition.Y, this.MenuStyle.TextureHeroSize.X, this.MenuStyle.TextureHeroSize.Y));

                if (ability.Value)
                {
                    //priority
                    renderer.DrawLine(
                        startPosition + new Vector2(0, this.MenuStyle.TextureHeroSize.Y - 6),
                        startPosition + new Vector2(6, this.MenuStyle.TextureHeroSize.Y - 6),
                        Color.Black,
                        12);
                    renderer.DrawText(
                        startPosition + new Vector2(0, this.MenuStyle.TextureHeroSize.Y - 12),
                        priority--.ToString(),
                        Color.White,
                        12,
                        this.MenuStyle.Font);
                }

                startPosition -= new Vector2(this.MenuStyle.TextureHeroSize.X + 4, 0);
            }

            if (this.drawDrag && this.dragTargetAbility == null)
            {
                this.dragTargetAbility = this.heroes.Select(x => x.Key).OrderBy(x => this.heroPriority[x]).FirstOrDefault();
                this.increasePriority = true;
            }
        }

        private void OnMouseKeyUp(object sender, MouseEventArgs e)
        {
            this.drag = false;
            this.drawDrag = false;
            this.InputManager.MouseKeyUp -= this.OnMouseKeyUp;
            this.InputManager.MouseMove -= this.OnMouseMove;
        }

        private void OnMouseMove(object sender, MouseMoveEventArgs e)
        {
            this.currentMousePosition = e.ScreenPosition;
            this.dragAbilityPosition = e.ScreenPosition - this.mousePressDiff;
            this.drawDrag = this.mousePressPosition.Distance2D(e.ScreenPosition) > 5;
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

        private int TryGetPriority(int priority)
        {
            while (this.heroPriority.Values.Any(x => x == priority) || this.savedHeroPriority.Values.Any(x => x == priority))
            {
                priority++;
            }

            if (priority >= this.autoPriority)
            {
                this.autoPriority = priority + 1;
            }

            return priority;
        }
    }
}