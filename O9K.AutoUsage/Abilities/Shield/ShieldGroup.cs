namespace O9K.AutoUsage.Abilities.Shield
{
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Helpers;
    using Core.Managers.Menu.EventArgs;

    using Glyph;

    using Settings;

    internal class ShieldGroup<TType, TAbility> : AutoUsageGroup<TType, TAbility>
        where TType : class, IActiveAbility where TAbility : ShieldAbility
    {
        private readonly GlyphAbility glyphAbility = new GlyphAbility();

        public ShieldGroup(MultiSleeper sleeper, GroupSettings settings)
            : base(sleeper, settings)
        {
            settings.AddSettingsMenu();
            settings.AbilityToggler.AddAbility("o9k.glyph");

            if (settings.GroupEnabled && settings.AbilityToggler.IsEnabled("o9k.glyph"))
            {
                this.Glyph(true);
            }
        }

        protected override void AbilityTogglerOnValueChange(object sender, AbilityEventArgs e)
        {
            if (e.Ability == "o9k.glyph")
            {
                this.Glyph(e.NewValue && this.Settings.GroupEnabled);
            }
            else
            {
                base.AbilityTogglerOnValueChange(sender, e);
            }
        }

        protected override void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            base.EnabledOnValueChange(sender, e);
            this.Glyph(e.NewValue && this.Settings.AbilityToggler.IsEnabled("o9k.glyph"));
        }

        private void Glyph(bool enable)
        {
            if (enable)
            {
                this.glyphAbility.Activate();
            }
            else
            {
                this.glyphAbility.Deactivate();
            }
        }
    }
}