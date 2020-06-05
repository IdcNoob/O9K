namespace O9K.AIO.Heroes.ChaosKnight
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_chaos_knight)]
    internal class ChaosKnightBase : BaseHero
    {
        public ChaosKnightBase(IContext9 context)
            : base(context)
        {
        }
    }
}