namespace O9K.AIO.Heroes.SandKing
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_sand_king)]
    internal class SandKingBase : BaseHero
    {
        public SandKingBase(IContext9 context)
            : base(context)
        {
        }
    }
}