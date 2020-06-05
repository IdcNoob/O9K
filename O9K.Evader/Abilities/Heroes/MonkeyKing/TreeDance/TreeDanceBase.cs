namespace O9K.Evader.Abilities.Heroes.MonkeyKing.TreeDance
{
    using Base;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.monkey_king_tree_dance)]
    internal class TreeDanceBase : EvaderBaseAbility, IUsable<BlinkAbility>
    {
        public TreeDanceBase(Ability9 ability)
            : base(ability)
        {
        }

        public BlinkAbility GetUsableAbility()
        {
            return new TreeDanceUsable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}