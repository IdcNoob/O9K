namespace O9K.Core.Entities.Abilities.Heroes.DarkWillow
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.dark_willow_bedlam)]
    public class Bedlam : AreaOfEffectAbility, INuke
    {
        private readonly SpecialData attackRadius;

        public Bedlam(Ability baseAbility)
            : base(baseAbility)
        {
            this.attackRadius = new SpecialData(baseAbility, "attack_radius");
            this.RadiusData = new SpecialData(baseAbility, "roaming_radius");
            this.DamageData = new SpecialData(baseAbility, "attack_damage");
            this.IsUltimate = false;
        }

        public override float Radius
        {
            get
            {
                return this.RadiusData.GetValue(this.Level) + this.attackRadius.GetValue(this.Level);
            }
        }
    }
}