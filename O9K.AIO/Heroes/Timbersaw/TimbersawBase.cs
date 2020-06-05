namespace O9K.AIO.Heroes.Timbersaw
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_shredder)]
    internal class TimbersawBase : BaseHero
    {
        public TimbersawBase(IContext9 context)
            : base(context)
        {
        }
    }
}