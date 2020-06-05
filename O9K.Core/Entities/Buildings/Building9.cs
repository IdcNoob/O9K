namespace O9K.Core.Entities.Buildings
{
    using Ensage;

    using Units;

    public class Building9 : Unit9
    {
        public Building9(Unit baseUnit)
            : base(baseUnit)
        {
            this.IsBuilding = true;
            this.IsUnit = false;
            this.IsVisible = true;
        }
    }
}