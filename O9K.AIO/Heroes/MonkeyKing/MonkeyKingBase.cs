namespace O9K.AIO.Heroes.MonkeyKing
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_monkey_king)]
    internal class MonkeyKingBase : BaseHero
    {
        public MonkeyKingBase(IContext9 context)
            : base(context)
        {
        }
    }
}