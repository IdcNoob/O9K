namespace O9K.Core.Entities.Mines
{
    using Ensage;

    using Metadata;

    using Units;

    [UnitName("npc_dota_techies_land_mine")]
    [UnitName("npc_dota_techies_stasis_trap")]
    public class Mine : Unit9
    {
        public Mine(Unit baseUnit)
            : base(baseUnit)
        {
            this.IsUnit = false;
        }
    }
}