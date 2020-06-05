namespace O9K.AIO.Heroes.Axe
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_axe)]
    internal class AxeBase : BaseHero
    {
        public AxeBase(IContext9 context)
            : base(context)
        {
        }
    }
}