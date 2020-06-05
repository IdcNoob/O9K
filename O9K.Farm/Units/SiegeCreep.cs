namespace O9K.Farm.Units
{
    using Base;

    using Damage;

    using O9K.Core.Entities.Metadata;
    using O9K.Core.Entities.Units;

    [UnitName("npc_dota_goodguys_siege")]
    [UnitName("npc_dota_badguys_siege")]
    internal class SiegeCreep : FarmCreep
    {
        public SiegeCreep(Unit9 unit)
            : base(unit)
        {
        }

        public override UnitDamage AddDamage(FarmUnit target, float attackStartTime, bool addNext, bool forceRanged)
        {
            var damage = new RangedDamage(this, target, attackStartTime, 15, 0.1f);
            target.IncomingDamage.Add(damage);

            return damage;
        }
    }
}