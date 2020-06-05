namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes.Unique
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.sniper_assassinate)]
    internal class AssassinateNuke : OldNukeAbility
    {
        public AssassinateNuke(INuke ability)
            : base(ability)
        {
        }

        //public override bool ShouldCast(Unit9 target)
        //{
        //    if (!base.ShouldCast(target))
        //    {
        //        return false;
        //    }

        //    return this.Owner.Distance(target) > 1000;
        //}
    }
}