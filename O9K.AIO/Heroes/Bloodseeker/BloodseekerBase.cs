namespace O9K.AIO.Heroes.Bloodseeker
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_bloodseeker)]
    internal class BloodseekerBase : BaseHero
    {
        public BloodseekerBase(IContext9 context)
            : base(context)
        {
        }
    }
}