namespace O9K.AIO.Heroes.QueenOfPain
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_queenofpain)]
    internal class QueenOfPainBase : BaseHero
    {
        public QueenOfPainBase(IContext9 context)
            : base(context)
        {
        }
    }
}