namespace O9K.AIO.Heroes.ShadowFiend
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_nevermore)]
    internal class ShadowFiendBase : BaseHero
    {
        public ShadowFiendBase(IContext9 context)
            : base(context)
        {
        }
    }
}