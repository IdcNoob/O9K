namespace O9K.Hud.Modules.Unique.MorphlingAbilities.Abilities
{
    using System;
    using System.Drawing;
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Logger;
    using Core.Managers.Renderer.Utils;

    using Ensage;
    using Ensage.SDK.Renderer;

    internal class MorphlingAbility : IMorphlingAbility
    {
        private readonly Ability9 ability;

        private readonly bool isAbilityReplicated;

        private readonly uint level;

        private readonly string texture;

        private float cooldown;

        private float updateTime;

        public MorphlingAbility(Ability9 ability)
        {
            this.ability = ability;
            this.texture = ability.Name;
            this.Handle = ability.Handle;
            this.AbilitySlot = this.ability.AbilitySlot;
            this.level = ability.Level;
            this.isAbilityReplicated = ability.BaseAbility.IsReplicated;
        }

        public AbilitySlot AbilitySlot { get; }

        public uint Handle { get; }

        private float RemainingCooldown
        {
            get
            {
                var cd = (this.updateTime + this.cooldown) - Game.RawGameTime;

                if (cd <= 0)
                {
                    this.cooldown = 0;
                }

                return cd;
            }
        }

        public bool Display(bool isMorphed)
        {
            return isMorphed != this.isAbilityReplicated;
        }

        public void Draw(IRenderer renderer, Rectangle9 position, float textSize)
        {
            try
            {
                renderer.DrawTexture(this.texture, position);
                renderer.DrawRectangle(position - 1, Color.Black);

                if (this.level == 0)
                {
                    renderer.DrawTexture("o9k.ability_0lvl_bg", position);
                    return;
                }

                if (this.cooldown <= 0)
                {
                    return;
                }

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
            if (isMorphed != this.isAbilityReplicated)
            {
                return true;
            }

            if (!this.ability.IsValid)
            {
                return false;
            }

            if (this.ability.AbilitySlot < 0 && this.ability.Owner.BaseSpellbook.Spells.All(x => x.Handle != this.Handle))
            {
                return false;
            }

            if (this.level > 0)
            {
                this.updateTime = Game.RawGameTime;
                this.cooldown = this.ability.BaseAbility.Cooldown;
            }

            return true;
        }
    }
}