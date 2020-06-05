namespace O9K.Hud.Modules.Units.Modifiers
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Units;

    using Ensage;

    using SharpDX;

    internal class ModifierUnit
    {
        private readonly bool isMyHero;

        private readonly List<DrawableModifier> modifiers = new List<DrawableModifier>();

        public ModifierUnit(Unit9 unit)
        {
            this.Unit = unit;
            this.IsAlly = unit.IsAlly();
            this.isMyHero = unit.IsMyHero;
        }

        public Vector2 HealthBarPosition
        {
            get
            {
                return this.Unit.HealthBarPosition;
            }
        }

        public bool IsAlly { get; }

        public IEnumerable<DrawableModifier> Modifiers
        {
            get
            {
                var distinct = new HashSet<string>();
                foreach (var modifier in this.modifiers.OrderByDescending(x => x.CreateTime))
                {
                    if (modifier.ShouldDraw && distinct.Add(modifier.TextureName))
                    {
                        yield return modifier;
                    }
                }
            }
        }

        public Unit9 Unit { get; }

        public void AddModifier(DrawableModifier modifier)
        {
            if (this.isMyHero && !modifier.IsHiddenAura)
            {
                return;
            }

            this.modifiers.Add(modifier);
        }

        public void CheckModifiers()
        {
            for (var i = this.modifiers.Count - 1; i > -1; i--)
            {
                var modifier = this.modifiers[i];

                if (!modifier.Modifier.IsValid)
                {
                    this.modifiers.RemoveAt(i);
                    continue;
                }

                modifier.UpdateTimings();
            }
        }

        public bool IsValid(bool showAlly)
        {
            if (this.IsAlly)
            {
                return (showAlly || this.isMyHero) && this.Unit.IsValid && this.Unit.IsAlive;
            }

            return this.Unit.IsValid && this.Unit.IsVisible && this.Unit.IsAlive;
        }

        public void RemoveModifier(Modifier modifier)
        {
            var find = this.modifiers.Find(x => x.Handle == modifier.Handle);
            if (find == null)
            {
                return;
            }

            this.modifiers.Remove(find);
        }
    }
}