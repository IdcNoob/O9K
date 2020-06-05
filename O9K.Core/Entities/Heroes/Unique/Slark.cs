namespace O9K.Core.Entities.Heroes.Unique
{
    using System;
    using System.Linq;

    using Data;

    using Ensage;

    using Extensions;

    using Managers.Entity;

    using Metadata;

    using SharpDX;

    using Units;

    [HeroId(HeroId.npc_dota_hero_slark)]
    internal class Slark : Hero9
    {
        private Vector3 lastShadowDancePosition;

        private uint shadowDanceLevel;

        private Unit9 shadowDanceUnit;

        public Slark(Hero baseHero)
            : base(baseHero)
        {
        }

        public override Vector3 GetPredictedPosition(float delay = 0, bool forceMovement = false)
        {
            if (this.shadowDanceUnit?.IsValid == true)
            {
                var currentPosition = this.shadowDanceUnit.Position;
                var predictedPosition = this.lastShadowDancePosition.Extend2D(
                    currentPosition,
                    delay * Math.Min(this.Speed * (1.25f + (this.shadowDanceLevel * 0.05f)), GameData.MaxMovementSpeed));
                this.lastShadowDancePosition = currentPosition;

                return predictedPosition;
            }

            return base.GetPredictedPosition(delay);
        }

        internal void ShadowDanced(bool added)
        {
            if (added)
            {
                this.shadowDanceUnit =
                    EntityManager9.Units.FirstOrDefault(x => x.Name == "npc_dota_slark_visual" && x.Owner.Handle == this.Handle);
                this.lastShadowDancePosition = this.shadowDanceUnit?.Position ?? Vector3.Zero;
                this.shadowDanceLevel = this.Abilities.FirstOrDefault(x => x.Id == AbilityId.slark_shadow_dance)?.Level ?? 0;
            }
            else
            {
                this.shadowDanceUnit = null;
                this.lastShadowDancePosition = Vector3.Zero;
            }
        }
    }
}