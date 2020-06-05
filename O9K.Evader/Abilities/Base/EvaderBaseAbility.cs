namespace O9K.Evader.Abilities.Base
{
    using Core.Entities.Abilities.Base;

    using Metadata;

    internal abstract class EvaderBaseAbility
    {
        protected EvaderBaseAbility(Ability9 ability)
        {
            this.Ability = ability;
        }

        public Ability9 Ability { get; }

        public IActionManager ActionManager { get; set; }

        public IMainMenu Menu { get; set; }

        public IPathfinder Pathfinder { get; set; }
    }
}