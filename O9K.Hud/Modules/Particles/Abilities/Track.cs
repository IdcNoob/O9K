namespace O9K.Hud.Modules.Particles.Abilities
{
    using System;
    using System.Collections.Generic;

    using Core.Entities.Metadata;
    using Core.Logger;
    using Core.Managers.Context;

    using Ensage;

    using Helpers.Notificator;

    using MainMenu;

    [AbilityId(AbilityId.bounty_hunter_track)]
    internal class Track : AbilityModule
    {
        private readonly Dictionary<uint, ParticleEffect> effects = new Dictionary<uint, ParticleEffect>();

        public Track(IContext9 context, INotificator notificator, IHudMenu hudMenu)
            : base(context, notificator, hudMenu)
        {
        }

        protected override void Disable()
        {
            Unit.OnModifierAdded -= this.OnModifierAdded;
            Unit.OnModifierRemoved -= this.OnModifierRemoved;

            foreach (var effect in this.effects.Values)
            {
                effect.Dispose();
            }

            this.effects.Clear();
        }

        protected override void Enable()
        {
            Unit.OnModifierAdded += this.OnModifierAdded;
            Unit.OnModifierRemoved += this.OnModifierRemoved;
        }

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Team != this.OwnerTeam)
                {
                    return;
                }

                if (args.Modifier.Name != "modifier_bounty_hunter_track")
                {
                    return;
                }

                var effect = new ParticleEffect(
                    "particles/econ/items/bounty_hunter/bounty_hunter_hunters_hoard/bounty_hunter_hoard_shield.vpcf",
                    sender,
                    ParticleAttachment.OverheadFollow);

                this.effects.Add(sender.Handle, effect);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Team != this.OwnerTeam)
                {
                    return;
                }

                if (args.Modifier.Name != "modifier_bounty_hunter_track")
                {
                    return;
                }

                if (!this.effects.TryGetValue(sender.Handle, out var effect))
                {
                    return;
                }

                effect.Dispose();
                this.effects.Remove(sender.Handle);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}