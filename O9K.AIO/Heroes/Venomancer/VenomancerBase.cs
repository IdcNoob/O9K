namespace O9K.AIO.Heroes.Venomancer
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_venomancer)]
    internal class VenomancerBase : BaseHero
    {
        public VenomancerBase(IContext9 context)
            : base(context)
        {
        }
    }
}