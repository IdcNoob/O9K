namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes.Unique
{
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;

    [AbilityId(AbilityId.nyx_assassin_mana_burn)]
    internal class ManaBurnNuke : OldNukeAbility
    {
        public ManaBurnNuke(INuke ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted(Unit9 target, ComboModeMenu menu, bool canHitCheck = true)
        {
            if (!base.CanBeCasted(target, menu, canHitCheck))
            {
                return false;
            }

            if (target.Mana < 100)
            {
                return false;
            }

            return true;
        }
    }
}