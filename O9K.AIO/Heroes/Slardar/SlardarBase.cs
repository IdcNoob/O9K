namespace O9K.AIO.Heroes.Slardar
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_slardar)]
    internal class SlardarBase : BaseHero
    {
        public SlardarBase(IContext9 context)
            : base(context)
        {
        }
    }
}