namespace O9K.AIO.Heroes.SkywrathMage
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_skywrath_mage)]
    internal class SkywrathMageBase : BaseHero
    {
        public SkywrathMageBase(IContext9 context)
            : base(context)
        {
        }
    }
}