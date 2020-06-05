namespace O9K.Core.Prediction.Collision
{
    using System;

    [Flags]
    public enum CollisionTypes
    {
        None = 0,

        AllyCreeps = 1 << 1,

        EnemyCreeps = 1 << 2,

        AllyHeroes = 1 << 3,

        EnemyHeroes = 1 << 4,

        Runes = 1 << 5,

        Trees = 1 << 6,

        AllUnits = AllyCreeps | AllyHeroes | EnemyCreeps | EnemyHeroes,

        AlliedUnits = AllyCreeps | AllyHeroes,

        EnemyUnits = EnemyCreeps | EnemyHeroes,
    }
}