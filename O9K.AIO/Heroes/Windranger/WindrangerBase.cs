namespace O9K.AIO.Heroes.Windranger
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_windrunner)]
    internal class WindrangerBase : BaseHero
    {
        public WindrangerBase(IContext9 context)
            : base(context)
        {
        }
    }
}