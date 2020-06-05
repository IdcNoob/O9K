namespace O9K.AIO.Heroes.DarkWillow
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_dark_willow)]
    internal class DarkWillowBase : BaseHero
    {
        public DarkWillowBase(IContext9 context)
            : base(context)
        {
        }
    }
}