namespace O9K.AIO.Heroes.Juggernaut
{
    using AIO.Modes.Permanent;

    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    using Modes;

    [HeroId(HeroId.npc_dota_hero_juggernaut)]
    internal class JuggernautBase : BaseHero
    {
        private readonly ControlWardMode controlWardMode;

        public JuggernautBase(IContext9 context)
            : base(context)
        {
            this.controlWardMode = new ControlWardMode(this, new PermanentModeMenu(this.Menu.RootMenu, "Healing ward control"));
        }

        public override void Dispose()
        {
            base.Dispose();
            this.controlWardMode.Dispose();
        }

        protected override void DisableCustomModes()
        {
            this.controlWardMode.Disable();
        }

        protected override void EnableCustomModes()
        {
            this.controlWardMode.Enable();
        }
    }
}