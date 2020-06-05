namespace O9K.AIO.Heroes.Pangolier
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_pangolier)]
    internal class PangolierBase : BaseHero
    {
        public PangolierBase(IContext9 context)
            : base(context)
        {
        }
    }
}