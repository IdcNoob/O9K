namespace O9K.AIO.Heroes.Lion
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_lion)]
    internal class LionBase : BaseHero
    {
        public LionBase(IContext9 context)
            : base(context)
        {
        }
    }
}