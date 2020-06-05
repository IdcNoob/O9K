namespace O9K.AIO.Heroes.Earthshaker
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    using Modes;

    [HeroId(HeroId.npc_dota_hero_earthshaker)]
    internal class EarthshakerBase : BaseHero
    {
        public EarthshakerBase(IContext9 context)
            : base(context)
        {
        }

        protected override void CreateComboMenus()
        {
            this.ComboMenus.Add(new EarthshakerComboModeMenu(this.Menu.RootMenu, "Combo"));
            this.ComboMenus.Add(new EarthshakerComboModeMenu(this.Menu.RootMenu, "Alternative combo"));
            this.ComboMenus.Add(new EarthshakerEchoSlamComboModeMenu(this.Menu.RootMenu, "Echo slam combo"));
        }
    }
}