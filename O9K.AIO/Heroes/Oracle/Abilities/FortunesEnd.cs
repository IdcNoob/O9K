namespace O9K.AIO.Heroes.Oracle.Abilities
{
    using AIO.Abilities;
    using AIO.Abilities.Menus;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;

    internal class FortunesEnd : NukeAbility
    {
        public FortunesEnd(ActiveAbility ability)
            : base(ability)
        {
        }

        public bool FullChannelTime(ComboModeMenu comboModeMenu)
        {
            var menu = comboModeMenu.GetAbilitySettingsMenu<FortunesEndMenu>(this);
            return menu.FullChannelTime;
        }

        public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
        {
            return new FortunesEndMenu(this.Ability, simplifiedName);
        }
    }
}