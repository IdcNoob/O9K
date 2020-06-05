namespace O9K.AIO.Heroes.Morphling
{
    using Base;

    using Core.Entities.Metadata;
    using Core.Managers.Context;

    using Ensage;

    [HeroId(HeroId.npc_dota_hero_morphling)]
    internal class MorphlingBase : BaseHero
    {
        public MorphlingBase(IContext9 context)
            : base(context)
        {
        }

        public override void CreateUnitManager()
        {
            this.UnitManager = new MorphlingUnitManager(this);
        }
    }
}