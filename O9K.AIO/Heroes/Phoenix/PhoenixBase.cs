namespace O9K.AIO.Heroes.Phoenix
{
    using AIO.Modes.KeyPress;

    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    using Modes;

    [HeroId(HeroId.npc_dota_hero_phoenix)]
    internal class PhoenixBase : BaseHero
    {
        private readonly SunRayAllyMode sunRayAllyMode;

        public PhoenixBase(IContext9 context)
            : base(context)
        {
            this.sunRayAllyMode = new SunRayAllyMode(this, new KeyPressModeMenu(this.Menu.RootMenu, "Sun ray ally"));
        }

        public override void Dispose()
        {
            base.Dispose();
            this.sunRayAllyMode.Dispose();
        }

        protected override void DisableCustomModes()

        {
            this.sunRayAllyMode.Disable();
        }

        protected override void EnableCustomModes()
        {
            this.sunRayAllyMode.Enable();
        }
    }
}