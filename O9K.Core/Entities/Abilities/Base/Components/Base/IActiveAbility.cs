namespace O9K.Core.Entities.Abilities.Base.Components.Base
{
    using System.Collections.Generic;

    using Ensage;

    using Entities.Units;

    using Prediction.Collision;
    using Prediction.Data;

    using SharpDX;

    public interface IActiveAbility
    {
        Ability BaseAbility { get; }

        bool BreaksLinkens { get; }

        float CastPoint { get; }

        float CastRange { get; }

        CollisionTypes CollisionTypes { get; }

        string DefaultName { get; }

        string DisplayName { get; }

        uint Handle { get; }

        AbilityId Id { get; }

        bool IsItem { get; }

        bool IsValid { get; }

        string Name { get; }

        bool NoTargetCast { get; }

        Unit9 Owner { get; }

        bool PositionCast { get; }

        float Radius { get; }

        float Speed { get; }

        bool UnitTargetCast { get; }

        bool CanBeCasted(bool checkChanneling = true);

        bool CanHit(Unit9 mainTarget, List<Unit9> aoeTargets, int minCount);

        bool CanHit(Unit9 target);

        float GetCastDelay(Vector3 position);

        float GetCastDelay(Unit9 unit);

        float GetCastDelay();

        float GetHitTime(Vector3 position);

        float GetHitTime(Unit9 unit);

        PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null);

        PredictionOutput9 GetPredictionOutput(PredictionInput9 input);

        bool PiercesMagicImmunity(Unit9 target);

        bool UseAbility(
            Unit9 mainTarget,
            List<Unit9> aoeTargets,
            HitChance minimumChance,
            int minAOETargets = 0,
            bool queue = false,
            bool bypass = false);

        bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false);

        bool UseAbility(bool queue = false, bool bypass = false);

        bool UseAbility(Unit9 target, bool queue = false, bool bypass = false);

        bool UseAbility(Tree target, bool queue = false, bool bypass = false);

        bool UseAbility(Vector3 position, bool queue = false, bool bypass = false);

        bool UseAbility(Rune target, bool queue = false, bool bypass = false);
    }
}