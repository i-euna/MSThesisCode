using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField]
    private EnemyType Type;

    [Tooltip("Enemy pool")]
    [SerializeField]
    private ObjectPoolVariable EnemyPool;

    [SerializeField]
    private GameEvent EnemyKilledEvent;

    [SerializeField]
    private GameEventWithStr CastleBreachedEvent;

    [SerializeField]
    private GameEventWithStr EnemyDeathEvent;

    [SerializeField]
    private IntVariable LevelTotalKilled, LevelTotalDestroyed;

    private int NeededShots;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Ground" &&
            other.gameObject.tag != "Enemy")
        {
            if (other.gameObject.tag == "Castle")
            {
                LevelTotalDestroyed.Value++;
                CastleBreachedEvent.InvokeEvent(Type.ToString());
                Destroy(gameObject);
            }
            else {
                if (other.gameObject.GetComponent<CannonShooter>().life >= 0) {
                    if (NeededShots == 1)
                    {
                        LevelTotalKilled.Value++;
                        LevelTotalDestroyed.Value++;
                        EnemyKilledEvent.Raise();
                        EnemyDeathEvent.InvokeEvent(Type.ToString());
                        //EnemyPool.ObjectPool.Release(gameObject);
                        Destroy(gameObject);
                    }
                    else
                    {
                        EnemyKilledEvent.Raise();
                        NeededShots--;
                    }
                }
                 
                
            }
        }
    }

    public void SetType(EnemyType enemyType) {
        Type = enemyType;
    }

    public void SetNeededShots(int shotCount) {
        NeededShots = shotCount;
    }

    public void SetConfigs(EnemyType enemyType, int shotCount) {
        Type = enemyType;
        NeededShots = shotCount;
    }
}
