using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private FloatVariable Speed;

    private bool IsPredictable;
    private float BackAndForthDist = 5f;
    private Vector3 originalPosition;
    private bool movingLeft = true;
    private int BackAndForthCount = 2, backAndForthCounter = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        IsPredictable = true;
        movingLeft = true;
        originalPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (IsPredictable)
            MoveForward();
        else MoveBackAndForth();
    }

    void MoveForward() {
        rb.velocity = new Vector3(-Speed.Value, 0, 0);
    }

    void MoveBackAndForth() {
        

        // Move the enemy based on its direction
        if (movingLeft)
        {
            rb.velocity = new Vector3(-Speed.Value, 0);
        }
        else
        {
            rb.velocity = new Vector3(Speed.Value, 0);
        }
    }

    public void SetSpeed(FloatVariable speed) {
        Speed = speed;
    }

    public void SetMovementType(bool isPredictable) {
        IsPredictable = isPredictable;
    }

    public void SetConfigs(FloatVariable speed, bool isPredictable) {
        Speed = speed;
        IsPredictable = isPredictable;

        movingLeft = true;
        originalPosition = transform.position;
        backAndForthCounter = 0;
    }
}
