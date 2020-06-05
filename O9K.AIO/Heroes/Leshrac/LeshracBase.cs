namespace O9K.AIO.Heroes.Leshrac
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_leshrac)]
    internal class LeshracBase : BaseHero
    {
        public LeshracBase(IContext9 context)
            : base(context)
        {
        }
    }
}