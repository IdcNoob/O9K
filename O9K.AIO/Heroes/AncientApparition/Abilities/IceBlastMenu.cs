namespace O9K.AIO.Heroes.AncientApparition.Abilities
{
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class IceBlastMenu : UsableAbilityMenu
    {
        public IceBlastMenu(Ability9 ability, string simplifiedName)
            : base(ability, simplifiedName)
        {
            this.StunOnly = this.Menu.Add(
                new MenuSwitcher("Stun only", "stunOnly" + simplifiedName, false).SetTooltip(
                    "Use ability only when enemy is stunned/rooted"));
            this.StunOnly.AddTranslation(Lang.Ru, "Только стан");
            this.StunOnly.AddTooltipTranslation(Lang.Ru, "Использовать способность, только если цель в стане/руте");
            this.StunOnly.AddTranslation(Lang.Cn, "眩晕只");
            this.StunOnly.AddTooltipTranslation(Lang.Cn, "仅当目标为 眩晕/缠绕 时才使用");
        }

        public MenuSwitcher StunOnly { get; }
    }
}