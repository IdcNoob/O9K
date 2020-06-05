namespace O9K.AIO.Heroes.AntiMage
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_antimage)]
    internal class AntiMageBase : BaseHero
    {
        public AntiMageBase(IContext9 context)
            : base(context)
        {
        }
    }
}