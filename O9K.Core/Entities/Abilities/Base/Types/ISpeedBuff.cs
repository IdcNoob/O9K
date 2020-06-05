namespace O9K.Core.Entities.Abilities.Base.Types
{
    using Entities.Units;

    public interface ISpeedBuff : IBuff
    {
        float GetSpeedBuff(Unit9 unit);
    }
}