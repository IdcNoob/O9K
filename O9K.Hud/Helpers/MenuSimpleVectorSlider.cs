namespace O9K.Hud.Helpers
{
    using System;

    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using SharpDX;

    internal class MenuSimpleVectorSlider : IDisposable
    {
        private readonly MenuSlider slider;

        public MenuSimpleVectorSlider(Menu menu, string displayName, string name, int value, int minValue, int maxValue)
        {
            this.slider = menu.Add(new MenuSlider(displayName, name, value, minValue, maxValue));
            this.slider.ValueChange += this.SliderOnValueChange;
        }

        public MenuSimpleVectorSlider(Menu menu, string displayName, int value, int minValue, int maxValue)
            : this(menu, displayName, displayName, value, minValue, maxValue)
        {
        }

        public Vector2 Value { get; private set; }

        public static implicit operator Vector2(MenuSimpleVectorSlider item)
        {
            return item.Value;
        }

        public void Dispose()
        {
            this.slider.ValueChange -= this.SliderOnValueChange;
        }

        private void SliderOnValueChange(object sender, SliderEventArgs e)
        {
            this.Value = new Vector2(e.NewValue);
        }
    }
}