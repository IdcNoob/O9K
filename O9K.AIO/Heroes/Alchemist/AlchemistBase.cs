namespace O9K.AIO.Heroes.Alchemist
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_alchemist)]
    internal class AlchemistBase : BaseHero
    {
        public AlchemistBase(IContext9 context)
            : base(context)
        {
        }
    }
}