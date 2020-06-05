namespace O9K.AIO.Heroes.Razor
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_razor)]
    internal class RazorBase : BaseHero
    {
        public RazorBase(IContext9 context)
            : base(context)
        {
        }
    }
}