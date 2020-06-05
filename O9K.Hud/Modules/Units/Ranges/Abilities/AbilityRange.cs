namespace O9K.Hud.Modules.Units.Ranges.Abilities
{
    using System;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    using SharpDX;

    internal class AbilityRange : IRange
    {
        private readonly Ability9 ability;

        private readonly bool forceCastRange;

        private MenuSlider blue;

        private bool enabled;

        private MenuSlider green;

        private ParticleEffect particleEffect;

        private MenuSlider red;

        public AbilityRange(Ability9 ability)
        {
            this.ability = ability;
            this.Name = ability.Name;
            this.Handle = ability.Handle;
            this.forceCastRange = ability.Id == AbilityId.nevermore_shadowraze1 || ability.Id == AbilityId.nevermore_shadowraze2
                                                                                || ability.Id == AbilityId.nevermore_shadowraze3;
        }

        public float DrawRange { get; private set; }

        public uint Handle { get; }

        public bool IsEnabled
        {
            get
            {
                return this.enabled && this.ability.IsValid && this.ability.IsUsable;
            }
        }

        public string Name { get; }

        public float Range { get; private set; }

        public void Dispose()
        {
            this.particleEffect?.Dispose();
            this.particleEffect = null;
            this.enabled = false;
            this.Range = 0;
            this.DrawRange = 0;
        }

        public void Enable(Menu heroMenu)
        {
            var abilityMenu = heroMenu.GetOrAdd(new Menu(this.ability.DisplayName, this.ability.DefaultName)).SetTexture(this.ability.Name);

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

            this.enabled = true;

            this.UpdateRange();
            this.DelayedRedraw();
        }

        public void UpdateRange()
        {
            if (!this.forceCastRange && (this.ability.AbilityBehavior & (AbilityBehavior.Point | AbilityBehavior.UnitTarget)) == 0)
            {
                var castRange = this.ability.Radius;
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (castRange == this.Range)
                {
                    return;
                }

                this.Range = castRange;
                this.DrawRange = castRange + Math.Max(castRange / 7, 40);
                this.RedrawRange();
            }
            else
            {
                var castRange = this.ability.CastRange;
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (castRange == this.Range)
                {
                    return;
                }

                this.Range = castRange;
                this.DrawRange = castRange + Math.Max(castRange / 9, 80);
                this.RedrawRange();
            }
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
                    this.ability.Owner.BaseUnit,
                    ParticleAttachment.AbsOriginFollow);
            }

            this.particleEffect.SetControlPoint(1, new Vector3(this.red, this.green, this.blue));
            this.particleEffect.SetControlPoint(2, new Vector3(-this.DrawRange, 255, 0));
            this.particleEffect.FullRestart();
        }
    }
}