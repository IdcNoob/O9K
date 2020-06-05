namespace O9K.Core.Entities.Heroes.Unique
{
    using Abilities.Base;
    using Abilities.Heroes.Alchemist;

    using Ensage;

    using Metadata;

    [HeroId(HeroId.npc_dota_hero_alchemist)]
    internal class Alchemist : Hero9
    {
        private ChemicalRage chemicalRage;

        private bool useRageAttackTime;

        public Alchemist(Hero baseHero)
            : base(baseHero)
        {
        }

        protected override float BaseAttackTime
        {
            get
            {
                if (this.useRageAttackTime && this.chemicalRage != null)
                {
                    return this.chemicalRage.AttackTime;
                }

                return base.BaseAttackTime;
            }
        }

        internal override void Ability(Ability9 ability, bool added)
        {
            base.Ability(ability, added);

            if (added && ability.Id == AbilityId.alchemist_chemical_rage)
            {
                this.chemicalRage = (ChemicalRage)ability;
            }
        }

        internal void Raged(bool value)
        {
            this.useRageAttackTime = value;
        }
    }
}