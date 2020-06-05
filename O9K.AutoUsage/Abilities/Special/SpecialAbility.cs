namespace O9K.AutoUsage.Abilities.Special
{
    using Core.Entities.Abilities.Base.Components.Base;

    public abstract class SpecialAbility : UsableAbility
    {
        protected SpecialAbility(IActiveAbility ability)
            : base(ability)
        {
        }
    }
}