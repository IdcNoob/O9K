namespace O9K.AIO.Heroes.Spectre
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_spectre)]
    internal class SpectreBase : BaseHero
    {
        public SpectreBase(IContext9 context)
            : base(context)
        {
        }
    }
}