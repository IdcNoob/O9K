namespace O9K.Hud.Helpers
{
    using System;

    using Core.Helpers;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using SharpDX;

    internal class MenuVectorSlider : IDisposable
    {
        private readonly MenuSlider xItem;

        private readonly MenuSlider yItem;

        public MenuVectorSlider(Menu menu, MenuSlider slider1, MenuSlider slider2)
        {
            this.xItem = menu.Add(slider1);
            this.yItem = menu.Add(slider2);

            this.Value = new Vector2(this.xItem, this.yItem);

            this.xItem.ValueChange += this.XOnValueChange;
            this.yItem.ValueChange += this.YOnValueChange;
        }

        public MenuVectorSlider(Menu menu, Vector3 values1, Vector3 values2)
            : this(
                menu,
                new MenuSlider("X coordinate", "x", (int)values1.X, (int)values1.Y, (int)values1.Z),
                new MenuSlider("Y coordinate", "y", (int)values2.X, (int)values2.Y, (int)values2.Z))
        {
            this.AddTranslation(Lang.Ru, "X координата", "Y координата");
            this.AddTranslation(Lang.Cn, "X位置", "Y位置");
        }

        public MenuVectorSlider(Menu menu, Vector2 value)
            : this(menu, new Vector3((int)value.X, 0, (int)Hud.Info.ScreenSize.X), new Vector3((int)value.Y, 0, (int)Hud.Info.ScreenSize.Y))
        {
        }

        public Vector2 Value { get; private set; }

        public static implicit operator Vector2(MenuVectorSlider item)
        {
            return item.Value;
        }

        public void AddTranslation(Lang lang, string slider1Name, string slider2Name)
        {
            this.xItem.AddTranslation(lang, slider1Name);
            this.yItem.AddTranslation(lang, slider2Name);
        }

        public void Dispose()
        {
            this.xItem.ValueChange -= this.XOnValueChange;
            this.yItem.ValueChange -= this.YOnValueChange;
        }

        private void XOnValueChange(object sender, SliderEventArgs e)
        {
            this.Value = new Vector2(e.NewValue, this.Value.Y);
        }

        private void YOnValueChange(object sender, SliderEventArgs e)
        {
            this.Value = new Vector2(this.Value.X, e.NewValue);
        }
    }
}