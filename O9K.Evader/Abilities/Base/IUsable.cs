namespace O9K.Evader.Abilities.Base
{
    using Usable;

    internal interface IUsable<out T>
        where T : UsableAbility
    {
        T GetUsableAbility();
    }
}