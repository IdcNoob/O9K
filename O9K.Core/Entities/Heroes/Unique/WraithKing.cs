namespace O9K.Core.Entities.Heroes.Unique
{
    using Abilities.Base;
    using Abilities.Heroes.WraithKing;

    using Ensage;

    using Metadata;

    [HeroId(HeroId.npc_dota_hero_skeleton_king)]
    internal class WraithKing : Hero9
    {
        private Reincarnate reincarnate;

        public WraithKing(Hero baseHero)
            : base(baseHero)
        {
        }

        public override bool CanReincarnate
        {
            get
            {
                return base.CanReincarnate || this.reincarnate?.CanBeCasted() == true;
            }
        }

        internal override void Ability(Ability9 ability, bool added)
        {
            base.Ability(ability, added);

            if (added && ability.Id == AbilityId.skeleton_king_reincarnation)
            {
                this.reincarnate = (Reincarnate)ability;
            }
        }
    }
}