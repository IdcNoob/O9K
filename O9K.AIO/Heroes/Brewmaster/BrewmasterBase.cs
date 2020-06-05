namespace O9K.AIO.Heroes.Brewmaster
{
    using AIO.Modes.KeyPress;

    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    using Modes;

    [HeroId(HeroId.npc_dota_hero_brewmaster)]
    internal class BrewmasterBase : BaseHero
    {
        private readonly CycloneMode cycloneMode;

        public BrewmasterBase(IContext9 context)
            : base(context)
        {
            this.cycloneMode = new CycloneMode(
                this,
                new KeyPressModeMenu(this.Menu.RootMenu, "Cyclone enemy", "Use storm's cyclone on the target"));
        }

        public override void Dispose()
        {
            base.Dispose();
            this.cycloneMode.Dispose();
        }

        protected override void DisableCustomModes()
        {
            this.cycloneMode.Disable();
        }

        protected override void EnableCustomModes()
        {
            this.cycloneMode.Enable();
        }
    }
}