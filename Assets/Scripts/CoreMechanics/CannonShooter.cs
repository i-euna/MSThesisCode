using System.Collections.Generic;
using UnityEngine;

public class CannonShooter : MonoBehaviour
{
    [Tooltip("Cannon pool")]
    [SerializeField]
    private ObjectPoolVariable CannonPool;

    [Tooltip("Speed of bullet")]
    [SerializeField]
    private FloatVariable CannonSpeed;

    [Tooltip("Last tap position of tap")]
    [SerializeField]
    private Vector3Variable MouseTapPos;

    [SerializeField]
    private GameEvent CannonMissEvent;

    private bool IsFired;

    [Tooltip("Default launch angle")]
    [SerializeField]
    private FloatVariable LaunchAngle;
    [Tooltip("Default initial velocity")]
    [SerializeField]
    private FloatVariable InitialVelocity;

    private Rigidbody2D Rb;

    private Vector3 TargetPosition;
    private float ElapsedTime = 0f;
    Vector3 velocity = Vector3.zero;

    [SerializeField]
    private Vector2List PathToFollow;
    private List<Vector2> Path;

    private int currentPointIndex = 0;

    [SerializeField]
    private BoolVariable isFiringCannon;
    public Vector3 com;
    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        IsFired = false;
    }

    public void Shoot() {
        
        //Vector3 cannonPosition = transform.position;
        //Vector3 shootDirection = (targetPosition - cannonPosition).normalized;
    }

    private void Update()
    {
        if(IsFired)
            Move();
    }
    void Move()
    {
         //float speed = 5f;
        // Check if the object has reached the current point
        if (Vector2.Distance(transform.position, Path[currentPointIndex]) <= 0.1f)
        {
            // Move to the next point on the path
            currentPointIndex = (currentPointIndex + 1) % Path.Count;
        }

        // Move the object towards the current point on the path
        Vector2 direction = (Path[currentPointIndex] - (Vector2)transform.position).normalized;
        transform.Translate(direction * CannonSpeed.Value * Time.deltaTime);
        //    Vector3 TargetPosition = Camera.main.ScreenToWorldPoint(MouseTapPos.Value);
        //    TargetPosition.z = transform.position.z;
        //    transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref velocity, 0.5f);
    }

    public void FireCannon() {
        Path = PathToFollow.points;
        Vector3 TargetPosition = Camera.main.ScreenToWorldPoint(MouseTapPos.Value);
        //Rb.constraints = RigidbodyConstraints2D.None;
        //Rb.freezeRotation = true;

        TargetPosition.z = transform.position.z;

        isFiringCannon.value = true;
        IsFired = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (IsFired)
        {
            isFiringCannon.value = false;
            Path.Clear();
            PathToFollow.points.Clear();
            IsFired = false;


            if (other.gameObject.tag == "Ground" ||
                other.gameObject.tag == "Castle") {
                CannonMissEvent.Raise();
            }

            /*
             * Destroying the GO temporarily,
             * have to release to pool
             */
            //gameObject.SetActive(false);
            //CannonPool.ObjectPool.Release(gameObject);
            Destroy(gameObject);
        }

    }

}
