namespace O9K.AIO.Heroes.Tidehunter
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_tidehunter)]
    internal class TidehunterBase : BaseHero
    {
        public TidehunterBase(IContext9 context)
            : base(context)
        {
        }
    }
}