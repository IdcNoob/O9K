namespace O9K.AIO.Heroes.Dazzle
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_dazzle)]
    internal class DazzleBase : BaseHero
    {
        public DazzleBase(IContext9 context)
            : base(context)
        {
        }
    }
}