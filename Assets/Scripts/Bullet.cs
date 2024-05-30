using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameEventWithStr CastleBreachEvent;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Castle"))
        {
            CastleBreachEvent.Event.Invoke(EnemyType.FAST_WALKER_BULLET.ToString());
            Destroy(gameObject);
        }
        
    }
}
