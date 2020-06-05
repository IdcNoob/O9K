namespace O9K.AIO.Heroes.Grimstroke
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_grimstroke)]
    internal class GrimstrokeBase : BaseHero
    {
        public GrimstrokeBase(IContext9 context)
            : base(context)
        {
        }
    }
}