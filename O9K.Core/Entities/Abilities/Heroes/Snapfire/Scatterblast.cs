namespace O9K.Core.Entities.Abilities.Heroes.Snapfire
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.snapfire_scatterblast)]
    public class Scatterblast : ConeAbility, INuke
    {
        private bool talentLearned;

        public Scatterblast(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "blast_width_initial");
            this.EndRadiusData = new SpecialData(baseAbility, "blast_width_end");
            this.SpeedData = new SpecialData(baseAbility, "blast_speed");
            this.DamageData = new SpecialData(baseAbility, "damage");
        }

        public bool AppliesDisarm
        {
            get
            {
                if (this.talentLearned)
                {
                    return true;
                }

                return this.talentLearned = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_snapfire_3)?.Level > 0;
            }
        }
    }
}