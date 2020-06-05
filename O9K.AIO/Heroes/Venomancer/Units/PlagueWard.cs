namespace O9K.AIO.Heroes.Venomancer.Units
{
    using System.Linq;

    using Base;

    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Managers.Entity;

    using Modes.Combo;

    [UnitName("npc_dota_venomancer_plague_ward_1")]
    [UnitName("npc_dota_venomancer_plague_ward_2")]
    [UnitName("npc_dota_venomancer_plague_ward_3")]
    [UnitName("npc_dota_venomancer_plague_ward_4")]
    internal class PlagueWard : ControllableUnit
    {
        public PlagueWard(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
        }

        public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
        {
            if (target?.HasModifier("modifier_venomancer_poison_sting_ward") != false)
            {
                var enemy = EntityManager9.EnemyHeroes
                    .Where(x => this.Owner.CanAttack(x) && !x.HasModifier("modifier_venomancer_poison_sting_ward"))
                    .OrderBy(x => x.Distance(this.Owner))
                    .FirstOrDefault();

                if (enemy != null)
                {
                    target = enemy;
                }
            }

            return base.Orbwalk(target, attack, move, comboMenu);
        }
    }
}