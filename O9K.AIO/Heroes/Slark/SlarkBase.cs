namespace O9K.AIO.Heroes.Slark
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_slark)]
    internal class SlarkBase : BaseHero
    {
        public SlarkBase(IContext9 context)
            : base(context)
        {
        }
    }
}