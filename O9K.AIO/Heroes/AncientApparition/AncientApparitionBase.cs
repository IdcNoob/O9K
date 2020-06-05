namespace O9K.AIO.Heroes.AncientApparition
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_ancient_apparition)]
    internal class AncientApparitionBase : BaseHero
    {
        public AncientApparitionBase(IContext9 context)
            : base(context)
        {
        }
    }
}