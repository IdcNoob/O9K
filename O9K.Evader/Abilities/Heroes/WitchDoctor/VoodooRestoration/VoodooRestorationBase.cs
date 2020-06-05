namespace O9K.Evader.Abilities.Heroes.WitchDoctor.VoodooRestoration
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.witch_doctor_voodoo_restoration)]
    internal class VoodooRestorationBase : EvaderBaseAbility //, IUsable<CounterAbility>
    {
        public VoodooRestorationBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}