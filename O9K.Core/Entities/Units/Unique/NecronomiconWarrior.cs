namespace O9K.Core.Entities.Units.Unique
{
    using Ensage;

    using Metadata;

    [UnitName("npc_dota_necronomicon_warrior_1")]
    [UnitName("npc_dota_necronomicon_warrior_2")]
    [UnitName("npc_dota_necronomicon_warrior_3")]
    internal class NecronomiconWarrior : Unit9
    {
        public NecronomiconWarrior(Unit baseUnit)
            : base(baseUnit)
        {
            this.IsAncient = true; // block midas
        }
    }
}