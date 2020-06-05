namespace O9K.AIO.Heroes.Oracle
{
    using AIO.Modes.KeyPress;

    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    using Modes;

    [HeroId(HeroId.npc_dota_hero_oracle)]
    internal class OracleBase : BaseHero
    {
        private readonly HealAllyMode healAllyMode;

        public OracleBase(IContext9 context)
            : base(context)
        {
            this.healAllyMode = new HealAllyMode(this, new KeyPressModeMenu(this.Menu.RootMenu, "Heal ally"));
        }

        public override void Dispose()
        {
            base.Dispose();
            this.healAllyMode.Dispose();
        }

        protected override void DisableCustomModes()
        {
            this.healAllyMode.Disable();
        }

        protected override void EnableCustomModes()
        {
            this.healAllyMode.Enable();
        }
    }
}