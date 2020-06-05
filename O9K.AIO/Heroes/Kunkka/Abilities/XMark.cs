namespace O9K.AIO.Heroes.Kunkka.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using SharpDX;

    internal class XMark : TargetableAbility
    {
        public XMark(ActiveAbility ability)
            : base(ability)
        {
        }

        public Vector3 Position { get; set; }
    }
}