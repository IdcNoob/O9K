namespace O9K.Core.Entities.Abilities.Heroes.Weaver
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.weaver_the_swarm)]
    public class TheSwarm : LineAbility, IDebuff
    {
        public TheSwarm(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "spawn_radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
        }

        public string DebuffModifierName { get; } = "modifier_weaver_swarm_debuff";

        public override float Radius
        {
            get
            {
                return base.Radius + 50;
            }
        }
    }
}