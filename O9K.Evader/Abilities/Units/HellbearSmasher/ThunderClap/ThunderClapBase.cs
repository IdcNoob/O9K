namespace O9K.Evader.Abilities.Units.HellbearSmasher.ThunderClap
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.polar_furbolg_ursa_warrior_thunder_clap)]
    internal class ThunderClapBase : EvaderBaseAbility, IEvadable
    {
        public ThunderClapBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ThunderClapEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}