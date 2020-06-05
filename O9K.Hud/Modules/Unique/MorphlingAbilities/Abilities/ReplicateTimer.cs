namespace O9K.Hud.Modules.Unique.MorphlingAbilities.Abilities
{
    using System;
    using System.Drawing;

    using Core.Logger;
    using Core.Managers.Renderer.Utils;

    using Ensage;
    using Ensage.SDK.Renderer;

    internal class ReplicateTimer : IMorphlingAbility
    {
        private readonly float cooldown;

        private readonly string texture;

        private readonly float updateTime;

        public ReplicateTimer(float cooldown)
        {
            this.AbilitySlot = (AbilitySlot)10; // force last
            this.Handle = uint.MaxValue;
            this.texture = nameof(AbilityId.morphling_morph_replicate);
            this.cooldown = cooldown;
            this.updateTime = Game.RawGameTime;
        }

        public AbilitySlot AbilitySlot { get; }

        public uint Handle { get; }

        private float RemainingCooldown
        {
            get
            {
                return (this.updateTime + this.cooldown) - Game.RawGameTime;
            }
        }

        public bool Display(bool isMorphed)
        {
            return true;
        }

        public void Draw(IRenderer renderer, Rectangle9 position, float textSize)
        {
            try
            {
                renderer.DrawTexture(this.texture, position);
                renderer.DrawRectangle(position - 1, Color.Black);
                renderer.DrawTexture("o9k.ability_cd_bg", position);
                renderer.DrawText(
                    position,
                    Math.Ceiling(this.RemainingCooldown).ToString("N0"),
                    Color.White,
                    RendererFontFlags.Center | RendererFontFlags.VerticalCenter,
                    textSize);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public bool Update(bool isMorphed)
        {
            return this.RemainingCooldown > 0;
        }
    }
}