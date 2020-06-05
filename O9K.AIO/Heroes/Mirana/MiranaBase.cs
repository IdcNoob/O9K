namespace O9K.AIO.Heroes.Mirana
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_mirana)]
    internal class MiranaBase : BaseHero
    {
        public MiranaBase(IContext9 context)
            : base(context)
        {
        }
    }
}