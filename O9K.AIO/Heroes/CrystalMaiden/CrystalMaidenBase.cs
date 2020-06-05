namespace O9K.AIO.Heroes.CrystalMaiden
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_crystal_maiden)]
    internal class CrystalMaidenBase : BaseHero
    {
        public CrystalMaidenBase(IContext9 context)
            : base(context)
        {
        }
    }
}