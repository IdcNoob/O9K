namespace O9K.AIO.Heroes.SpiritBreaker
{
    using AIO.Modes.KeyPress;

    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    using Modes;

    [HeroId(HeroId.npc_dota_hero_spirit_breaker)]
    internal class SpiritBreakerBase : BaseHero
    {
        private readonly ChargeAwayMode chargeAwayMode;

        public SpiritBreakerBase(IContext9 context)
            : base(context)
        {
            this.chargeAwayMode = new ChargeAwayMode(this, new KeyPressModeMenu(this.Menu.RootMenu, "Charge away"));
        }

        public override void Dispose()
        {
            base.Dispose();

            this.chargeAwayMode.Dispose();
        }

        protected override void DisableCustomModes()

        {
            this.chargeAwayMode.Disable();
        }

        protected override void EnableCustomModes()
        {
            this.chargeAwayMode.Enable();
        }
    }
}