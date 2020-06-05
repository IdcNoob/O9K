namespace O9K.AIO.Heroes.Tiny
{
    using AIO.Modes.KeyPress;

    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    using Modes;

    [HeroId(HeroId.npc_dota_hero_tiny)]
    internal class TinyBase : BaseHero
    {
        private readonly TossUnderTowerMode tossToTower;

        public TinyBase(IContext9 context)
            : base(context)
        {
            this.tossToTower = new TossUnderTowerMode(
                this,
                new KeyPressModeMenu(this.Menu.RootMenu, "Toss to tower", "Toss enemy to ally tower"));
        }

        public override void Dispose()
        {
            base.Dispose();
            this.tossToTower.Dispose();
        }

        protected override void DisableCustomModes()
        {
            this.tossToTower.Disable();
        }

        protected override void EnableCustomModes()
        {
            this.tossToTower.Enable();
        }
    }
}