namespace O9K.Hud.Modules.Units.Ranges.Abilities
{
    using Core.Entities.Units;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    using SharpDX;

    internal class AttackRange : IRange
    {
        private readonly Unit9 unit;

        private MenuSlider blue;

        private MenuSlider green;

        private ParticleEffect particleEffect;

        private MenuSlider red;

        public AttackRange(Unit9 unit)
        {
            this.unit = unit;
            this.Handle = uint.MaxValue;
            this.Name = "o9k.attack";
        }

        public float DrawRange { get; private set; }

        public uint Handle { get; }

        public bool IsEnabled { get; private set; }

        public string Name { get; }

        public float Range { get; private set; }

        public void Dispose()
        {
            this.particleEffect?.Dispose();
            this.particleEffect = null;
            this.IsEnabled = false;
            this.Range = 0;
            this.DrawRange = 0;
        }

        public void Enable(Menu heroMenu)
        {
            var abilityMenu = heroMenu.GetOrAdd(new Menu("Attack", "o9k.attack")).SetTexture("o9k.attack");
            abilityMenu.AddTranslation(Lang.Ru, "Атака");
            abilityMenu.AddTranslation(Lang.Cn, "攻击");

            this.red = abilityMenu.GetOrAdd(new MenuSlider("Red", 255, 0, 255));
            this.red.AddTranslation(Lang.Ru, "Красный");
            this.red.AddTranslation(Lang.Cn, "红");

            this.green = abilityMenu.GetOrAdd(new MenuSlider("Green", 0, 0, 255));
            this.green.AddTranslation(Lang.Ru, "Зеленый");
            this.green.AddTranslation(Lang.Cn, "绿色");

            this.blue = abilityMenu.GetOrAdd(new MenuSlider("Blue", 0, 0, 255));
            this.blue.AddTranslation(Lang.Ru, "Синий");
            this.blue.AddTranslation(Lang.Cn, "蓝色");

            this.red.ValueChange += this.ColorOnValueChange;
            this.green.ValueChange += this.ColorOnValueChange;
            this.blue.ValueChange += this.ColorOnValueChange;

            this.IsEnabled = true;

            this.UpdateRange();
            this.DelayedRedraw();
        }

        public void UpdateRange()
        {
            var attackRange = this.unit.GetAttackRange();
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (attackRange == this.Range)
            {
                return;
            }

            this.Range = attackRange;
            this.DrawRange = attackRange * 1.15f;
            this.RedrawRange();
        }

        private void ColorOnValueChange(object sender, SliderEventArgs e)
        {
            if (e.NewValue == e.OldValue)
            {
                return;
            }

            this.DelayedRedraw();
        }

        private void DelayedRedraw()
        {
            UpdateManager.BeginInvoke(this.RedrawRange);
        }

        private void RedrawRange()
        {
            if (this.particleEffect == null)
            {
                this.particleEffect = new ParticleEffect(
                    @"particles\ui_mouseactions\drag_selected_ring.vpcf",
                    this.unit.BaseUnit,
                    ParticleAttachment.AbsOriginFollow);
            }

            this.particleEffect.SetControlPoint(1, new Vector3(this.red, this.green, this.blue));
            this.particleEffect.SetControlPoint(2, new Vector3(-this.DrawRange, 255, 0));
            this.particleEffect.FullRestart();
        }
    }
}