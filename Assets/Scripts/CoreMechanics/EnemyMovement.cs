using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private FloatVariable Speed;

    private bool IsPredictable = true;

    public float ForwardDuration = 5f; // Time to move forward
    public float ReverseDuration = 1f; // Time to move in reverse
    private float moveTimer = 5f;//same as ForwardDuration
    private bool isReversing = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0)
        {
            if (isReversing)
            {
                // If currently reversing, switch to forward movement
                isReversing = false;
                moveTimer = ForwardDuration;
            }
            else
            {
                // If currently moving forward, switch to reverse movement
                isReversing = true;
                moveTimer = ReverseDuration;
            }
        }

        float direction = isReversing ? 1 : -1;
        rb.velocity = new Vector3(direction * Speed.Value, 0, 0);
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
    }
}
