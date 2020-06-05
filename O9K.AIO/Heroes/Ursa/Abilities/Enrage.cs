namespace O9K.AIO.Heroes.Ursa.Abilities
{
    using AIO.Abilities;
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;

    using Modes.Combo;

    using TargetManager;

    internal class Enrage : ShieldAbility
    {
        public Enrage(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
        {
            if (!base.CanBeCasted(targetManager, channelingCheck, comboMenu))
            {
                return false;
            }

            if (!this.Owner.CanAttack())
            {
                return false;
            }

            var menu = comboMenu.GetAbilitySettingsMenu<EnrageMenu>(this);
            if (menu.StacksCount <= 0)
            {
                return false;
            }

            if (menu.StacksCount > targetManager.Target.GetModifierStacks("modifier_ursa_fury_swipes_damage_increase"))
            {
                return false;
            }

            return true;
        }

        public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
        {
            return new EnrageMenu(this.Ability, simplifiedName);
        }
    }
}