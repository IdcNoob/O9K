namespace O9K.Core.Entities.Abilities.Base.Components
{
    using Entities.Units;

    public interface IToggleable
    {
        bool Enabled { get; set; }

        bool IsValid { get; }

        Unit9 Owner { get; }

        bool CanBeCasted(bool checkChanneling = true);
    }
}