namespace O9K.Evader.Abilities.Base.Evadable.Components
{
    using Ensage;

    internal interface IModifierObstacle
    {
        bool AllyModifierObstacle { get; }

        void AddModifierObstacle(Modifier modifier, Unit sender);
    }
}