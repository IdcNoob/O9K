namespace O9K.AIO.Heroes.Lina
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_lina)]
    internal class LinaBase : BaseHero
    {
        public LinaBase(IContext9 context)
            : base(context)
        {
        }
    }
}