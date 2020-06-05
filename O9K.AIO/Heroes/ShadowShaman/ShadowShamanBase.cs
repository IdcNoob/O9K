namespace O9K.AIO.Heroes.ShadowShaman
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_shadow_shaman)]
    internal class ShadowShamanBase : BaseHero
    {
        public ShadowShamanBase(IContext9 context)
            : base(context)
        {
        }
    }
}