namespace O9K.AIO.Heroes.Dynamic.Abilities.Specials
{
    using Core.Entities.Abilities.Base.Components.Base;

    internal abstract class OldSpecialAbility : OldUsableAbility
    {
        protected OldSpecialAbility(IActiveAbility ability)
            : base(ability)
        {
        }
    }
}