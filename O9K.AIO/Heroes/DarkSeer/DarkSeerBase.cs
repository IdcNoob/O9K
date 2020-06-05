namespace O9K.AIO.Heroes.DarkSeer
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_dark_seer)]
    internal class DarkSeerBase : BaseHero
    {
        public DarkSeerBase(IContext9 context)
            : base(context)
        {
        }
    }
}