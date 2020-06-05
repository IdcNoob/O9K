namespace O9K.Hud.Modules.Particles.Abilities
{
    using System;

    using Core.Entities.Metadata;
    using Core.Logger;
    using Core.Managers.Context;

    using Ensage;

    using Helpers.Notificator;

    using MainMenu;

    [AbilityId(AbilityId.kunkka_torrent)]
    internal class Torrent : AbilityModule
    {
        public Torrent(IContext9 context, INotificator notificator, IHudMenu hudMenu)
            : base(context, notificator, hudMenu)
        {
        }

        protected override void Disable()
        {
            Unit.OnModifierAdded -= this.OnModifierAdded;
        }

        protected override void Enable()
        {
            Unit.OnModifierAdded += this.OnModifierAdded;
        }

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Team == this.OwnerTeam)
                {
                    return;
                }

                if (args.Modifier.Name != "modifier_kunkka_torrent_thinker")
                {
                    return;
                }

                var effect = new ParticleEffect(
                    "particles/econ/items/kunkka/divine_anchor/hero_kunkka_dafx_skills/kunkka_spell_torrent_bubbles_fxset.vpcf",
                    sender.Position);
                effect.Release();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}