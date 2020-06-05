namespace O9K.AIO.Heroes.TrollWarlord
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_troll_warlord)]
    internal class TrollWarlordBase : BaseHero
    {
        public TrollWarlordBase(IContext9 context)
            : base(context)
        {
        }
    }
}