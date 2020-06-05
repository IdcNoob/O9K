namespace O9K.Hud.Modules.Particles.Units
{
    using System;
    using System.ComponentModel.Composition;

    using Core.Entities.Units;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;

    using MainMenu;

    using SharpDX;

    internal class Illusion : IHudModule
    {
        private readonly MenuSwitcher show;

        private Team ownerTeam;

        [ImportingConstructor]
        public Illusion(IHudMenu hudMenu)
        {
            this.show = hudMenu.ParticlesMenu.Add(new MenuSwitcher("Illusion", "illusion"));
            this.show.AddTranslation(Lang.Ru, "Иллюзии");
            this.show.AddTranslation(Lang.Cn, "幻象");
        }

        public void Activate()
        {
            this.ownerTeam = EntityManager9.Owner.Team;
            this.show.ValueChange += this.ShowOnValueChanging;
        }

        public void Dispose()
        {
            this.show.ValueChange -= this.ShowOnValueChanging;
            EntityManager9.UnitAdded -= this.OnUnitAdded;
        }

        private void OnUnitAdded(Unit9 entity)
        {
            try
            {
                if (!entity.IsIllusion || entity.Team == this.ownerTeam || entity.CanUseAbilities)
                {
                    return;
                }

                if ((entity.UnitState & (UnitState.Unselectable | UnitState.CommandRestricted))
                    == (UnitState.Unselectable | UnitState.CommandRestricted))
                {
                    return;
                }

                var effect = new ParticleEffect(
                    "materials/ensage_ui/particles/illusions_mod_v2.vpcf",
                    entity.BaseUnit,
                    ParticleAttachment.CenterFollow);

                effect.SetControlPoint(1, new Vector3(255));
                effect.SetControlPoint(2, new Vector3(65, 105, 255));

                effect.Release();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void ShowOnValueChanging(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                EntityManager9.UnitAdded += this.OnUnitAdded;
            }
            else
            {
                EntityManager9.UnitAdded -= this.OnUnitAdded;
            }
        }
    }
}