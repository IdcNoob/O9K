namespace O9K.AIO.Heroes.Necrophos
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_necrolyte)]
    internal class NecrophosBase : BaseHero
    {
        public NecrophosBase(IContext9 context)
            : base(context)
        {
        }
    }
}