namespace O9K.AIO.Heroes.Riki
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_riki)]
    internal class RikiBase : BaseHero
    {
        public RikiBase(IContext9 context)
            : base(context)
        {
        }
    }
}