namespace O9K.Core.Entities.Units.Unique
{
    using System.Linq;

    using Abilities.Heroes.Invoker;

    using Ensage;

    using Managers.Entity;

    using Metadata;

    [UnitName("npc_dota_invoker_forged_spirit")]
    internal class ForgedSpirit : Unit9
    {
        public ForgedSpirit(Unit baseUnit)
            : base(baseUnit)
        {
            var forgeSpirit = (ForgeSpirit)EntityManager9.Abilities.FirstOrDefault(x => x.Id == AbilityId.invoker_forge_spirit);
            if (forgeSpirit == null)
            {
                return;
            }

            this.BaseAttackRange = forgeSpirit.ForgeSpiritAttackRange;
        }

        internal override float BaseAttackRange { get; } = 900;
    }
}