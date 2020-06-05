namespace O9K.Core.Entities.Heroes.Unique
{
    using Ensage;

    using Managers.Entity;

    using Metadata;

    using Units;

    [HeroId(HeroId.npc_dota_hero_morphling)]
    public class Morphling : Hero9
    {
        public Morphling(Hero baseHero)
            : base(baseHero)
        {
            if (baseHero.ReplicateFrom?.HeroId != this.Id)
            {
                this.IsIllusion = false;
                this.CanUseAbilities = true;
                this.IsImportant = true;
            }
        }

        public bool IsMorphed { get; private set; }

        public Unit9 MorphedHero { get; private set; }

        internal override float BaseAttackRange
        {
            get
            {
                if (this.IsMorphed && this.MorphedHero?.IsValid == true)
                {
                    return this.MorphedHero.BaseAttackRange;
                }

                return base.BaseAttackRange;
            }
        }

        internal override float HpBarOffset
        {
            get
            {
                if (this.IsMorphed && this.MorphedHero != null)
                {
                    return this.MorphedHero.HpBarOffset;
                }

                return base.HpBarOffset;
            }
        }

        internal void Morphed(bool added)
        {
            this.IsRanged = this.BaseUnit.IsRanged;
            this.IsMorphed = added;

            if (this.IsMorphed)
            {
                this.MorphedHero = EntityManager9.GetUnitFast(this.BaseHero.ReplicateFrom?.Handle);
            }
        }
    }
}