namespace O9K.AIO.Heroes.Magnus
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    using Modes;

    [HeroId(HeroId.npc_dota_hero_magnataur)]
    internal class MagnusBase : BaseHero
    {
        private readonly BlinkSkewerMode blinkSkewerMode;

        public MagnusBase(IContext9 context)
            : base(context)
        {
            this.blinkSkewerMode = new BlinkSkewerMode(this, new BlinkSkewerModeMenu(this.Menu.RootMenu, "Blink+Skewer combo"));
        }

        public override void Dispose()
        {
            base.Dispose();
            this.blinkSkewerMode.Dispose();
        }

        protected override void DisableCustomModes()

        {
            this.blinkSkewerMode.Disable();
        }

        protected override void EnableCustomModes()
        {
            this.blinkSkewerMode.Enable();
        }
    }
}