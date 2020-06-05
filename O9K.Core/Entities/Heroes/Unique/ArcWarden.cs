namespace O9K.Core.Entities.Heroes.Unique
{
    using Ensage;

    using Metadata;

    [HeroId(HeroId.npc_dota_hero_arc_warden)]
    internal class ArcWarden : Hero9
    {
        public ArcWarden(Hero baseHero)
            : base(baseHero)
        {
            if (this.IsIllusion && this.HasModifier("modifier_arc_warden_tempest_double"))
            {
                this.CanUseAbilities = true;
                this.CanUseItems = true;
                this.IsImportant = true;
            }
        }
    }
}