namespace O9K.Core.Entities.Units.Unique
{
    using Ensage;

    using Metadata;

    [UnitName("npc_dota_neutral_mud_golem_split")]
    internal class MudGolem : Unit9
    {
        public MudGolem(Unit baseUnit)
            : base(baseUnit)
        {
            this.Name = "npc_dota_neutral_mud_golem";
        }
    }
}