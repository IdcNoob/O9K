namespace O9K.AIO.Heroes.FacelessVoid
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_faceless_void)]
    internal class FacelessVoidBase : BaseHero
    {
        public FacelessVoidBase(IContext9 context)
            : base(context)
        {
        }
    }
}