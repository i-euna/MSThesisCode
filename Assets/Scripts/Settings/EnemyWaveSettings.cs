using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyWaveSettings
{
    public static readonly Dictionary<Levels, Dictionary<EnemyType, int>>
        LevelRequirement;

    static EnemyWaveSettings()
    {
        LevelRequirement = new Dictionary<Levels, Dictionary<EnemyType, int>>();

        LevelRequirement[Levels.Level0] = new Dictionary<EnemyType, int>
        {
            { EnemyType.SLOW_WALKER, 5 }
        };

        LevelRequirement[Levels.Level1] = new Dictionary<EnemyType, int>
        {
            { EnemyType.SLOW_WALKER, 5 },
            { EnemyType.MEDIUM_WALKER, 3 }
        };

        LevelRequirement[Levels.Level2] = new Dictionary<EnemyType, int>
        {
            { EnemyType.MEDIUM_WALKER, 6 },
            { EnemyType.FAST_WALKER, 3 },
            { EnemyType.SLOW_AIR, 3 }
        };
        LevelRequirement[Levels.Level3E] = new Dictionary<EnemyType, int>
        {
            { EnemyType.SLOW_WALKER, 3 },
            { EnemyType.MEDIUM_WALKER, 5 },
            { EnemyType.FAST_WALKER, 3 },
            { EnemyType.MEDIUM_AIR, 4 }
        };
        LevelRequirement[Levels.Level3H] = new Dictionary<EnemyType, int>
        {
            { EnemyType.MEDIUM_WALKER, 3 },
            { EnemyType.FAST_WALKER, 8 },
            { EnemyType.FAST_AIR, 4 }
        };
        LevelRequirement[Levels.Level4E] = new Dictionary<EnemyType, int>
        {
            { EnemyType.SLOW_WALKER_2SHOTS, 3 },
            { EnemyType.MEDIUM_WALKER, 5 },
            { EnemyType.FAST_WALKER, 4 },
            { EnemyType.FAST_AIR, 3 },
            { EnemyType.SLOW_WALKER_1SHOTS_UNPREDICTABLE, 5 }
        };
        LevelRequirement[Levels.Level4M] = new Dictionary<EnemyType, int>
        {
            { EnemyType.MEDIUM_WALKER_2SHOTS, 3 },
            { EnemyType.MEDIUM_WALKER_1SHOTS_UNPREDICTABLE, 5 },
            { EnemyType.FAST_WALKER, 5 },
            { EnemyType.FAST_AIR, 4 },
            { EnemyType.FAST_AIR_1SHOTS_UNPREDICTABLE, 3 }
        };
        LevelRequirement[Levels.Level4H] = new Dictionary<EnemyType, int>
        {
            { EnemyType.FAST_WALKER, 5 },
            { EnemyType.FAST_WALKER_2SHOTS, 3 },
            { EnemyType.FAST_WALKER_1SHOTS_UNPREDICTABLE, 5 },
            { EnemyType.FAST_AIR, 3 },
            { EnemyType.FAST_AIR_1SHOTS_UNPREDICTABLE, 4 },
        };
    }
}
