namespace O9K.Hud.Modules.TopPanel.Status
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Heroes;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Managers.Renderer.Utils;

    using Ensage;
    using Ensage.SDK.Renderer;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal class TopPanelUnit
    {
        private readonly Hero9 hero;

        private readonly List<Ability9> items = new List<Ability9>();

        private readonly Dictionary<string, Modifier> modifiers = new Dictionary<string, Modifier>();

        private readonly Dictionary<string, float> modifiersTime = new Dictionary<string, float>();

        private Ability9 ultimate;

        public TopPanelUnit(Unit9 hero)
        {
            this.hero = (Hero9)hero;
            this.Handle = hero.Handle;
            this.IsAlly = hero.IsAlly();
        }

        public Sleeper BuybackSleeper { get; } = new Sleeper();

        public uint Handle { get; }

        public bool IsAlly { get; }

        public bool IsValid
        {
            get
            {
                return this.hero.IsValid;
            }
        }

        public void AddItem(Ability9 ability)
        {
            this.items.Add(ability);
        }

        public void AddModifier(Modifier modifier)
        {
            this.modifiers[modifier.TextureName] = modifier;
            this.modifiersTime[modifier.TextureName] = Game.RawGameTime + modifier.RemainingTime;
        }

        public void DrawAllyHealth(IRenderer renderer, bool dim, RectangleF position)
        {
            if (!this.hero.IsAlive)
            {
                return;
            }

            renderer.DrawTexture("o9k.health_ally_bg", position);
            position.Width *= this.hero.HealthPercentageBase;
            renderer.DrawTexture(!dim || this.hero.IsVisibleToEnemies ? "o9k.health_ally_visible" : "o9k.health_ally", position);
        }

        public void DrawAllyMana(IRenderer renderer, bool dim, RectangleF position)
        {
            if (!this.hero.IsAlive)
            {
                return;
            }

            renderer.DrawTexture("o9k.mana_bg", position);
            position.Width *= this.hero.ManaPercentageBase;
            renderer.DrawTexture(!dim || this.hero.IsVisibleToEnemies ? "o9k.mana" : "o9k.mana_invis", position);
        }

        public void DrawBuyback(IRenderer renderer, RectangleF position)
        {
            if (this.BuybackSleeper.IsSleeping || this.hero.IsAlive)
            {
                return;
            }

            renderer.DrawTexture("o9k.buyback", position);
        }

        public void DrawEnemyHealth(IRenderer renderer, bool dim, RectangleF position)
        {
            if (!this.hero.IsAlive)
            {
                return;
            }

            renderer.DrawTexture("o9k.health_enemy_bg", position);
            position.Width *= this.hero.HealthPercentageBase;
            renderer.DrawTexture(!dim || this.hero.IsVisible ? "o9k.health_enemy" : "o9k.health_enemy_invis", position);
        }

        public void DrawEnemyMana(IRenderer renderer, bool dim, RectangleF position)
        {
            if (!this.hero.IsAlive)
            {
                return;
            }

            renderer.DrawTexture("o9k.mana_bg", position);
            position.Width *= this.hero.ManaPercentageBase;
            renderer.DrawTexture(!dim || this.hero.IsVisible ? "o9k.mana" : "o9k.mana_invis", position);
        }

        public void DrawItems(IRenderer renderer, RectangleF position)
        {
            if (this.items.Count == 0 || !this.hero.IsAlive)
            {
                return;
            }

            var start = new Vector2(position.X, position.Y);
            var size = position.Width * 0.3f;

            foreach (var item in this.items)
            {
                if (!item.IsValid)
                {
                    continue;
                }

                renderer.DrawTexture(item.Id + "_rounded", start, new Vector2(size));
                start += new Vector2(size + 2, 0);

                if (start.X + size > position.Right)
                {
                    start = new Vector2(position.X, position.Y + size + 2);
                }
            }
        }

        public void DrawRunes(IRenderer renderer, RectangleF position)
        {
            var size = position.Width * 0.35f;
            var start = new Vector2(position.X, position.Y - size);

            if (!this.hero.IsVisible)
            {
                foreach (var pair in this.modifiersTime.ToList())
                {
                    var name = pair.Key;
                    var endTime = pair.Value;

                    if (Game.RawGameTime > endTime)
                    {
                        this.modifiers.Remove(name);
                        this.modifiersTime.Remove(name);
                        continue;
                    }

                    renderer.DrawTexture(name + "_rounded", start, new Vector2(size));
                    renderer.DrawTexture("o9k.outline", start, new Vector2(size));
                    start += new Vector2(size + 2, 0);
                }

                return;
            }

            foreach (var pair in this.modifiers.ToList())
            {
                var name = pair.Key;
                var modifier = pair.Value;

                if (!modifier.IsValid)
                {
                    this.modifiers.Remove(name);
                    this.modifiersTime.Remove(name);
                    continue;
                }

                renderer.DrawTexture(name + "_rounded", start, new Vector2(size));
                renderer.DrawTexture("o9k.outline", start, new Vector2(size));
                start += new Vector2(size + 2, 0);
            }
        }

        public bool DrawUltimate(IRenderer renderer, RectangleF position, Rectangle9 cdPosition, bool cdTime)
        {
            if (this.ultimate?.IsValid != true)
            {
                return false;
            }

            var cooldown = this.ultimate.RemainingCooldown;
            if (cooldown > 0)
            {
                renderer.DrawTexture("o9k.ult_cd", position);

                if (!cdPosition.IsZero)
                {
                    if (!this.hero.IsAlive)
                    {
                        return false;
                    }

                    var pct = (int)(100 - ((cooldown / this.ultimate.Cooldown) * 100));
                    var outlinePosition = cdPosition * 1.1f;

                    renderer.DrawTexture(this.ultimate.Name + "_rounded", cdPosition);
                    renderer.DrawTexture("o9k.outline_black100", outlinePosition);
                    renderer.DrawTexture("o9k.outline_green_pct" + pct, outlinePosition);

                    if (cdTime)
                    {
                        renderer.DrawTexture("o9k.top_ult_cd_bg", cdPosition);
                        renderer.DrawText(
                            cdPosition,
                            cooldown.ToString("N0"),
                            Color.White,
                            RendererFontFlags.Center | RendererFontFlags.VerticalCenter,
                            20 * Hud.Info.ScreenRatio);
                    }

                    return true;
                }
            }
            else if (this.ultimate.ManaCost > this.hero.Mana)
            {
                renderer.DrawTexture("o9k.ult_mp", position);

                if (!cdPosition.IsZero)
                {
                    if (!this.hero.IsAlive)
                    {
                        return false;
                    }

                    var pct = (int)((this.hero.Mana / this.ultimate.ManaCost) * 100);
                    var outlinePosition = cdPosition * 1.1f;

                    renderer.DrawTexture(this.ultimate.Name + "_rounded", cdPosition);
                    renderer.DrawTexture("o9k.outline_black100", outlinePosition);
                    renderer.DrawTexture("o9k.outline_blue_pct" + pct, outlinePosition);

                    if (cdTime)
                    {
                        var mpCd = Math.Ceiling((this.ultimate.ManaCost - this.hero.Mana) / this.hero.ManaRegeneration).ToString("N0");
                        renderer.DrawTexture("o9k.top_ult_cd_bg", cdPosition);
                        renderer.DrawText(cdPosition, mpCd, Color.White, RendererFontFlags.Center | RendererFontFlags.VerticalCenter, 20);
                    }

                    return true;
                }
            }
            else if (this.ultimate.IsUsable && this.ultimate.Level > 0)
            {
                renderer.DrawTexture("o9k.ult_rdy", position);
            }

            return false;
        }

        public void ForceDrawBuyback(IRenderer renderer, Rectangle9 position, Rectangle9 deadPosition)
        {
            if (this.BuybackSleeper.IsSleeping)
            {
                return;
            }

            if (this.hero.IsAlive)
            {
                renderer.DrawTexture("o9k.buyback_alive", position);
            }
            else
            {
                renderer.DrawTexture("o9k.buyback", deadPosition);
            }
        }

        public void RemoveItem(Ability9 ability)
        {
            this.items.Remove(ability);
        }

        public void SetUltimate(Ability9 ability)
        {
            if (this.ultimate?.IsValid == true)
            {
                return;
            }

            if (!this.hero.Equals(ability.Owner))
            {
                return;
            }

            this.ultimate = ability;
        }
    }
}