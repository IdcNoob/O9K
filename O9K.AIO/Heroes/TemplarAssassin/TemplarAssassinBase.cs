namespace O9K.AIO.Heroes.TemplarAssassin
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_templar_assassin)]
    internal class TemplarAssassinBase : BaseHero
    {
        public TemplarAssassinBase(IContext9 context)
            : base(context)
        {
        }
    }
}