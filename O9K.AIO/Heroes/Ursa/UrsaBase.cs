namespace O9K.AIO.Heroes.Ursa
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_ursa)]
    internal class UrsaBase : BaseHero
    {
        public UrsaBase(IContext9 context)
            : base(context)
        {
        }
    }
}