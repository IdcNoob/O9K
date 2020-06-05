namespace O9K.Evader.AbilityManager.Monitors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Abilities.Base.Evadable;
    using Abilities.Base.Evadable.Components;

    using Core.Data;
    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;

    internal class UnitMonitor : IDisposable
    {
        private readonly Dictionary<string, AbilityId> abilityUnitNames = new Dictionary<string, AbilityId>
        {
            { "npc_dota_gyrocopter_homing_missile", AbilityId.gyrocopter_homing_missile },
            { "npc_dota_stormspirit_remnant", AbilityId.storm_spirit_static_remnant },
            { "npc_dota_metamorphosis_fear", AbilityId.terrorblade_metamorphosis } //todo add
        };

        private readonly List<EvadableAbility> evadableAbilities;

        private readonly Team heroTeam;

        public UnitMonitor(List<EvadableAbility> evadableAbilities)
        {
            this.evadableAbilities = evadableAbilities;
            this.heroTeam = EntityManager9.Owner.Team;

            ObjectManager.OnAddEntity += this.OnAddEntity;
        }

        public void Dispose()
        {
            ObjectManager.OnAddEntity -= this.OnAddEntity;
        }

        private void OnAddEntity(EntityEventArgs args)
        {
            try
            {
                var unit = args.Entity as Unit;
                if (unit?.IsValid != true || unit.UnitType != 0 || unit.DayVision == 0)
                {
                    return;
                }

                if (unit.Team == this.heroTeam)
                {
                    return;
                }

                if (this.abilityUnitNames.TryGetValue(unit.Name, out var id))
                {
                    var ability = this.evadableAbilities.Find(x => x.Ability.Id == id) as IUnit;
                    ability?.AddUnit(unit);
                    return;
                }

                if (unit.NetworkName != "CDOTA_BaseNPC")
                {
                    return;
                }

                var ids = GameData.AbilityVision.Where(x => x.Value == unit.DayVision).Select(x => x.Key).ToList();

                if (ids.Count == 0)
                {
                    //// new unit test
                    //if (unit.IsVisible)
                    //{
                    //    var summoner = ObjectManager.GetEntitiesFast<Unit>()
                    //        .Where(x => x != unit && x.Team == unit.Team && x.Distance2D(unit) < 600)
                    //        .OrderBy(x => x.Distance2D(unit))
                    //        .FirstOrDefault();

                    //    if (summoner != null)
                    //    {
                    //        Logger.Error(
                    //            new O9KException("Unknown unit 1"),
                    //            unit,
                    //            summoner.Name + " " + unit.DayVision + " " + unit.NightVision);
                    //    }
                    //    else
                    //    {
                    //        Logger.Error(new O9KException("Unknown unit 2"), unit, unit.DayVision + " " + unit.NightVision);
                    //    }
                    //}
                    //else
                    //{
                    //    Logger.Error(new O9KException("Unknown unit 3"), unit, unit.DayVision + " " + unit.NightVision);
                    //}

                    return;
                }

                var abilities = this.evadableAbilities.Where(
                        x => ids.Contains(x.Ability.Id)
                             && (!x.Owner.IsVisible || x.Ability.TimeSinceCasted < 0.5f + x.Ability.ActivationDelay))
                    .ToList();

                if (abilities.Count != 1)
                {
                    return;
                }

                if (!(abilities[0] is IUnit unitAbility))
                {
                    return;
                }

                unitAbility.AddUnit(unit);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}