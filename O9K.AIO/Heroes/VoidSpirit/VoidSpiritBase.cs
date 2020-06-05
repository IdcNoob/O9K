namespace O9K.AIO.Heroes.VoidSpirit
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_void_spirit)]
    internal class VoidSpiritBase : BaseHero
    {
        public VoidSpiritBase(IContext9 context)
            : base(context)
        {
        }
    }
}