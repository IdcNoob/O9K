namespace O9K.AIO.Heroes.VengefulSpirit
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_vengefulspirit)]
    internal class VengefulSpiritBase : BaseHero
    {
        public VengefulSpiritBase(IContext9 context)
            : base(context)
        {
        }
    }
}