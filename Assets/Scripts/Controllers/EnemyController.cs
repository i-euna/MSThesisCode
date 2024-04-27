using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System;

public class EnemyController : MonoBehaviour
{
    [Tooltip("Walker Enemy pool")]
    [SerializeField]
    private ObjectPoolVariable WalkerPool;

    [Tooltip("Air Enemy pool")]
    [SerializeField]
    private ObjectPoolVariable AirEnemyPool;

    [Tooltip("Medium Speed Walker Enemy pool")]
    [SerializeField]
    private ObjectPoolVariable MediumSpeedWalkerPool;

    [Tooltip("High Speed Walker Enemy pool")]
    [SerializeField]
    private ObjectPoolVariable HighSpeedWalkerPool;

    [Tooltip("Walker")]
    [SerializeField]
    private GameObject WalkerEnemyPrefab;

    [Tooltip("Air")]
    [SerializeField]
    private GameObject AirEnemyPrefab;

    [Tooltip("Medium speed walker")]
    [SerializeField]
    private GameObject MediumSpeedWalkerPrefab;

    [Tooltip("High speed walker")]
    [SerializeField]
    private GameObject HighSpeedWalkerPrefab;

    [Tooltip("Max no of enemy in the pool")]
    [SerializeField]
    private IntVariable MaxNoOfEnemies;

    [Tooltip("Initial Wait Time for Enemy Invoker")]
    [SerializeField]
    private FloatVariable InitialWaitTime;

    [Tooltip("Repeat Rate for Enemy Invoker")]
    [SerializeField]
    private FloatVariable EnemyRepeatRate;

    [SerializeField]
    private IntVariable TotalSpawnedEnemy, TotalKilledEnemy;

    [SerializeField]
    private float MinSpawnInterval;

    [SerializeField]
    private float MaxSpawnInterval;

    private Vector3 InitialPosWalker, InitialPosAir;
    private Quaternion InitWalkerRotation, InitAirRotation;
    private int SpawnedWalkerCount, SlowSpeedWalkerCount, MediumSpeedWalkerCount, HighSpeedWalkerCount;
    private int AirEnemyCount;
    List<float> spawnIntervals;
    List<EnemyType> enemyTypes;

    [SerializeField]
    private FloatVariable SlowAirSpeed, MediumAirSpeed, FastAirSpeed;
    private void Start()
    {
        InitializeEnemyPools();
        InitialPosWalker = WalkerEnemyPrefab.transform.position;
        InitialPosAir = AirEnemyPrefab.transform.position;
        InitAirRotation = AirEnemyPrefab.transform.rotation;
        TotalKilledEnemy.Value = 0;
        TotalSpawnedEnemy.Value = 0;

        PrepareEnemySequence();
        SpawnedWalkerCount = 0;
        SpawnWithDelay();
    }

    void InitializeEnemyPools() {
        //Initialize the pool
        WalkerPool.ObjectPool = new ObjectPool<GameObject>(() =>
        { return Instantiate(WalkerEnemyPrefab); },
        enemy => { enemy.SetActive(true); },
        enemy => { enemy.SetActive(false); },
        enemy => { Destroy(enemy); },
        false,
        MaxNoOfEnemies.Value,
        MaxNoOfEnemies.Value
        );

        AirEnemyPool.ObjectPool = new ObjectPool<GameObject>(() =>
        { return Instantiate(AirEnemyPrefab); },
        enemy => { enemy.SetActive(true); },
        enemy => { enemy.SetActive(false); },
        enemy => { Destroy(enemy); },
        false,
        MaxNoOfEnemies.Value,
        MaxNoOfEnemies.Value
        );

        //Debug.Log("MediumSpeedWalkerPool " + MediumSpeedWalkerPool.ObjectPool);
        MediumSpeedWalkerPool.ObjectPool = new ObjectPool<GameObject>(() =>
        { return Instantiate(MediumSpeedWalkerPrefab); },
        enemy => { enemy.SetActive(true); },
        enemy => { enemy.SetActive(false); },
        enemy => { Destroy(enemy); },
        false,
        MaxNoOfEnemies.Value,
        MaxNoOfEnemies.Value
        );

        HighSpeedWalkerPool.ObjectPool = new ObjectPool<GameObject>(() =>
        { return Instantiate(HighSpeedWalkerPrefab); },
        enemy => { enemy.SetActive(true); },
        enemy => { enemy.SetActive(false); },
        enemy => { Destroy(enemy); },
        false,
        MaxNoOfEnemies.Value,
        MaxNoOfEnemies.Value
        );
    }

    void PrepareEnemySequence() {
        Dictionary<EnemyType, int> req = EnemyWaveSettings.LevelRequirement[LevelManager.GetCurrentLevel()];
        TotalSpawnedEnemy.Value = 0;

        spawnIntervals = new List<float>();
        enemyTypes = new List<EnemyType>();
        //calculating total enemies to be spawned
        foreach (KeyValuePair<EnemyType, int> r in req)
        {
            switch (r.Key) {
                case EnemyType.SLOW_WALKER:
                    SpawnedWalkerCount = 0;
                    SlowSpeedWalkerCount = r.Value;
                    TotalSpawnedEnemy.Value += SlowSpeedWalkerCount;
                    ListManipulator.AddMultipleTimes(
                            enemyTypes, 
                            EnemyType.SLOW_WALKER, 
                            r.Value
                        );
                    break;
                case EnemyType.MEDIUM_WALKER:
                    MediumSpeedWalkerCount = r.Value;
                    TotalSpawnedEnemy.Value += MediumSpeedWalkerCount;

                    ListManipulator
                        .AddMultipleTimes(
                            enemyTypes,
                            EnemyType.MEDIUM_WALKER,
                            r.Value
                        );
                    break;
                case EnemyType.FAST_WALKER:
                    HighSpeedWalkerCount = r.Value;
                    TotalSpawnedEnemy.Value += HighSpeedWalkerCount;

                    ListManipulator
                        .AddMultipleTimes(
                            enemyTypes, 
                            EnemyType.FAST_WALKER, 
                            r.Value
                        );
                    break;
                case EnemyType.SLOW_WALKER_2SHOTS:
                    int count = r.Value;
                    TotalSpawnedEnemy.Value += count;

                    ListManipulator
                        .AddMultipleTimes(
                            enemyTypes,
                            EnemyType.SLOW_WALKER_2SHOTS,
                            r.Value
                        );
                    break;
                case EnemyType.MEDIUM_WALKER_2SHOTS:
                    count = r.Value;
                    TotalSpawnedEnemy.Value += count;

                    ListManipulator
                        .AddMultipleTimes(
                            enemyTypes,
                            EnemyType.MEDIUM_WALKER_2SHOTS,
                            r.Value
                        );
                    break;
                case EnemyType.FAST_WALKER_2SHOTS:
                    count = r.Value;
                    TotalSpawnedEnemy.Value += count;

                    ListManipulator
                        .AddMultipleTimes(
                            enemyTypes,
                            EnemyType.MEDIUM_WALKER_2SHOTS,
                            r.Value
                        );
                    break;
                case EnemyType.SLOW_WALKER_1SHOTS_UNPREDICTABLE:
                    count = r.Value;
                    TotalSpawnedEnemy.Value += count;

                    ListManipulator
                        .AddMultipleTimes(
                            enemyTypes,
                            EnemyType.SLOW_WALKER_1SHOTS_UNPREDICTABLE,
                            r.Value
                        );
                    break;
                case EnemyType.MEDIUM_WALKER_1SHOTS_UNPREDICTABLE:
                    count = r.Value;
                    TotalSpawnedEnemy.Value += count;

                    ListManipulator
                        .AddMultipleTimes(
                            enemyTypes,
                            EnemyType.MEDIUM_WALKER_1SHOTS_UNPREDICTABLE,
                            r.Value
                        );
                    break;
                case EnemyType.FAST_WALKER_1SHOTS_UNPREDICTABLE:
                    count = r.Value;
                    TotalSpawnedEnemy.Value += count;

                    ListManipulator
                        .AddMultipleTimes(
                            enemyTypes,
                            EnemyType.FAST_WALKER_1SHOTS_UNPREDICTABLE,
                            r.Value
                        );
                    break;
                case EnemyType.SLOW_AIR:
                    count = r.Value;
                    TotalSpawnedEnemy.Value += count;
                    ListManipulator
                        .AddMultipleTimes(
                            enemyTypes,
                            EnemyType.SLOW_AIR,
                            r.Value
                        );
                    break;
                case EnemyType.MEDIUM_AIR:
                    count = r.Value;
                    TotalSpawnedEnemy.Value += count;
                    ListManipulator
                        .AddMultipleTimes(
                            enemyTypes,
                            EnemyType.MEDIUM_AIR,
                            r.Value
                        );
                    break;
                case EnemyType.FAST_AIR:
                    count = r.Value;
                    TotalSpawnedEnemy.Value += count;
                    ListManipulator
                        .AddMultipleTimes(
                            enemyTypes,
                            EnemyType.FAST_AIR,
                            r.Value
                        );
                    break;
                case EnemyType.FAST_AIR_1SHOTS_UNPREDICTABLE:
                    count = r.Value;
                    TotalSpawnedEnemy.Value += count;
                    ListManipulator
                        .AddMultipleTimes(
                            enemyTypes,
                            EnemyType.FAST_AIR_1SHOTS_UNPREDICTABLE,
                            r.Value
                        );
                    break;
                default:
                    break;
            }
        }
        enemyTypes = ListManipulator.ShuffleList(enemyTypes);

        //prepare spawn intervals
        spawnIntervals.Add(0);
        for (int i = 1; i < TotalSpawnedEnemy.Value; i++) {
            float spawnDelay = UnityEngine.Random.Range(MinSpawnInterval, MaxSpawnInterval);
            spawnIntervals.Add(spawnDelay);
        }

        //Debug.Log("total spawned enemy " + TotalSpawnedEnemy.Value);
}

    void SpawnWithDelay()
    {
        if (SpawnedWalkerCount >= TotalSpawnedEnemy.Value)
        {
            Debug.Log("Nothing more to spawn " + TotalSpawnedEnemy.Value);
            return;
        }

        float spawnDelay = spawnIntervals[SpawnedWalkerCount];
        //Debug.Log(" spawnDelay " + spawnDelay);
        Invoke("SpawnEnemy", spawnDelay);
    }

    void SpawnEnemy()
    {
        EnemyType enemyType = enemyTypes[SpawnedWalkerCount];

        GameObject newEnemy;
        switch (enemyType) {
            case EnemyType.SLOW_WALKER:
                newEnemy = WalkerPool.ObjectPool.Get();
                newEnemy.GetComponent<EnemyMovement>()
                        .SetConfigs(SlowAirSpeed, true);
                newEnemy.GetComponent<EnemyDeath>()
                        .SetConfigs(EnemyType.SLOW_WALKER, 1);
                newEnemy.transform.position = InitialPosWalker;
                break;
            case EnemyType.MEDIUM_WALKER:
                newEnemy = MediumSpeedWalkerPool.ObjectPool.Get();
                newEnemy.GetComponent<EnemyMovement>()
                        .SetConfigs(MediumAirSpeed, true);
                newEnemy.GetComponent<EnemyDeath>()
                        .SetConfigs(EnemyType.MEDIUM_WALKER, 1);
                newEnemy.transform.position = InitialPosWalker;
                break;
            case EnemyType.FAST_WALKER:
                newEnemy = HighSpeedWalkerPool.ObjectPool.Get();
                newEnemy.GetComponent<EnemyMovement>()
                        .SetConfigs(FastAirSpeed, true);
                newEnemy.GetComponent<EnemyDeath>()
                        .SetConfigs(EnemyType.FAST_WALKER, 1);
                newEnemy.transform.position = InitialPosWalker;
                break;
            case EnemyType.SLOW_WALKER_2SHOTS:
                newEnemy = WalkerPool.ObjectPool.Get();
                newEnemy.GetComponent<EnemyMovement>()
                        .SetConfigs(SlowAirSpeed, true);
                newEnemy.GetComponent<EnemyDeath>()
                        .SetConfigs(EnemyType.SLOW_WALKER_2SHOTS, 2);
                newEnemy.GetComponent<SpriteRenderer>().color = Color.cyan;
                newEnemy.transform.position = InitialPosWalker;
                break;
            case EnemyType.MEDIUM_WALKER_2SHOTS:
                newEnemy = WalkerPool.ObjectPool.Get();
                newEnemy.GetComponent<EnemyMovement>()
                        .SetConfigs(MediumAirSpeed, true);
                newEnemy.GetComponent<EnemyDeath>()
                        .SetConfigs(EnemyType.MEDIUM_WALKER_2SHOTS, 2);
                newEnemy.GetComponent<SpriteRenderer>().color = Color.gray;
                newEnemy.transform.position = InitialPosWalker;
                break;
            case EnemyType.FAST_WALKER_2SHOTS:
                newEnemy = WalkerPool.ObjectPool.Get();
                newEnemy.GetComponent<EnemyMovement>()
                        .SetConfigs(FastAirSpeed, true);
                newEnemy.GetComponent<EnemyDeath>()
                        .SetConfigs(EnemyType.FAST_WALKER_2SHOTS, 2);
                newEnemy.GetComponent<SpriteRenderer>().color = Color.black;
                newEnemy.transform.position = InitialPosWalker;
                break;
            case EnemyType.SLOW_WALKER_1SHOTS_UNPREDICTABLE:
                newEnemy = WalkerPool.ObjectPool.Get();
                newEnemy.GetComponent<EnemyMovement>()
                        .SetConfigs(SlowAirSpeed, false);
                newEnemy.GetComponent<EnemyDeath>()
                        .SetConfigs(EnemyType.SLOW_WALKER_1SHOTS_UNPREDICTABLE, 1);
                newEnemy.GetComponent<EnemyDeath>().SetNeededShots(1);
                newEnemy.GetComponent<SpriteRenderer>().color = Color.green;
                newEnemy.transform.position = InitialPosWalker;
                break;
            case EnemyType.MEDIUM_WALKER_1SHOTS_UNPREDICTABLE:
                newEnemy = WalkerPool.ObjectPool.Get();
                newEnemy.GetComponent<EnemyMovement>()
                        .SetConfigs(MediumAirSpeed, false);
                newEnemy.GetComponent<EnemyDeath>()
                        .SetConfigs(EnemyType.MEDIUM_WALKER_1SHOTS_UNPREDICTABLE, 1);
                newEnemy.GetComponent<SpriteRenderer>().color = Color.green;
                newEnemy.transform.position = InitialPosWalker;
                break;
            case EnemyType.FAST_WALKER_1SHOTS_UNPREDICTABLE:
                newEnemy = WalkerPool.ObjectPool.Get();
                newEnemy.GetComponent<EnemyMovement>()
                        .SetConfigs(FastAirSpeed, false);
                newEnemy.GetComponent<EnemyDeath>()
                        .SetConfigs(EnemyType.FAST_WALKER_1SHOTS_UNPREDICTABLE, 1);
                newEnemy.GetComponent<SpriteRenderer>().color = Color.green;
                newEnemy.transform.position = InitialPosWalker;
                break;
            case EnemyType.SLOW_AIR:
                newEnemy = AirEnemyPool.ObjectPool.Get();
                newEnemy.GetComponent<EnemyMovement>()
                        .SetConfigs(SlowAirSpeed, false);
                newEnemy.GetComponent<EnemyDeath>()
                        .SetConfigs(EnemyType.SLOW_AIR, 1);
                newEnemy.transform.position = InitialPosAir;
                newEnemy.transform.rotation = InitAirRotation;
                break;
            case EnemyType.MEDIUM_AIR:
                newEnemy = AirEnemyPool.ObjectPool.Get();
                newEnemy.GetComponent<EnemyMovement>()
                        .SetConfigs(MediumAirSpeed, false);
                newEnemy.GetComponent<EnemyDeath>()
                        .SetConfigs(EnemyType.MEDIUM_AIR, 1);
                newEnemy.GetComponent<SpriteRenderer>().color = Color.green;
                newEnemy.transform.position = InitialPosAir;
                newEnemy.transform.rotation = InitAirRotation;
                break;
            case EnemyType.FAST_AIR:
                newEnemy = AirEnemyPool.ObjectPool.Get();
                newEnemy.GetComponent<EnemyMovement>()
                        .SetConfigs(FastAirSpeed, false);
                newEnemy.GetComponent<EnemyDeath>()
                        .SetConfigs(EnemyType.FAST_AIR, 1);
                newEnemy.GetComponent<SpriteRenderer>().color = Color.green;
                newEnemy.transform.position = InitialPosAir;
                newEnemy.transform.rotation = InitAirRotation;
                break;
            case EnemyType.FAST_AIR_1SHOTS_UNPREDICTABLE:
                newEnemy = AirEnemyPool.ObjectPool.Get();
                newEnemy.GetComponent<EnemyMovement>()
                        .SetConfigs(FastAirSpeed, true);
                newEnemy.GetComponent<EnemyDeath>()
                        .SetConfigs(EnemyType.FAST_AIR, 1);
                newEnemy.GetComponent<SpriteRenderer>().color = Color.green;
                newEnemy.transform.position = InitialPosAir;
                newEnemy.transform.rotation = InitAirRotation;
                break;
            default:
                newEnemy = WalkerPool.ObjectPool.Get();
                newEnemy.GetComponent<EnemyMovement>()
                        .SetConfigs(SlowAirSpeed, false);
                newEnemy.GetComponent<EnemyDeath>()
                        .SetConfigs(EnemyType.SLOW_WALKER, 1);
                newEnemy.GetComponent<SpriteRenderer>().color = Color.red;
                newEnemy.transform.position = InitialPosAir;
                newEnemy.transform.rotation = InitAirRotation;
                break;
        }
        
        newEnemy.SetActive(true);

        SpawnedWalkerCount++;

        SpawnWithDelay();
    }

}
