namespace O9K.Evader.Abilities.Heroes.DarkSeer.IonShell
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.dark_seer_ion_shell)]
    internal class IonShellBase : EvaderBaseAbility, IEvadable
    {
        public IonShellBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new IonShellEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}