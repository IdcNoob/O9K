namespace O9K.Core.Entities.Abilities.Base.Types
{
    using Components.Base;

    using Entities.Units;

    public interface INuke : IActiveAbility
    {
        int GetDamage(Unit9 unit);
    }
}