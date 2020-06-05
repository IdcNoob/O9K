namespace O9K.AIO.Heroes.Warlock
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_warlock)]
    internal class WarlockBase : BaseHero
    {
        public WarlockBase(IContext9 context)
            : base(context)
        {
        }
    }
}