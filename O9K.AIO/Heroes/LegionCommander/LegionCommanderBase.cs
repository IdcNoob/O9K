namespace O9K.AIO.Heroes.LegionCommander
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_legion_commander)]
    internal class LegionCommanderBase : BaseHero
    {
        public LegionCommanderBase(IContext9 context)
            : base(context)
        {
        }
    }
}