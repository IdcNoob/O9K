namespace O9K.AIO.Heroes.Gyrocopter
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_gyrocopter)]
    internal class GyrocopterBase : BaseHero
    {
        public GyrocopterBase(IContext9 context)
            : base(context)
        {
        }
    }
}