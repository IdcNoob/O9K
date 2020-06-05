namespace O9K.AIO.Heroes.Mars
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_mars)]
    internal class MarsBase : BaseHero
    {
        public MarsBase(IContext9 context)
            : base(context)
        {
        }
    }
}