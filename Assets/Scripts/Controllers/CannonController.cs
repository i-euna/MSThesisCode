using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CannonController : MonoBehaviour
{
    [Tooltip("Cannon pool")]
    [SerializeField]
    private ObjectPoolVariable CannonPool;

    [Tooltip("Original Cannon")]
    [SerializeField]
    private GameObject CannonPrefab;
    //List of cannons
    private Stack FiredCannons;
    [Tooltip("Max no of cannon in the pool")]
    [SerializeField]
    private IntVariable MaxNoOfCannon;

    [SerializeField]
    private IntVariable AmmoCount;

    [SerializeField]
    private GameObject LaunchPoint;

    private Vector3 InitialPos;
    private Quaternion InitialRot;
    private Vector3 CannonParentPos;
    private Quaternion CannonParentRot;

    public GameObject CannonBody;

    [SerializeField]
    private GameEvent CheckGameOverEvent;
    [SerializeField]
    private IntVariable LevelTotalAmmo;

    void Start()
    {
        CannonParentPos = CannonBody.transform.position;
        CannonParentRot = CannonBody.transform.rotation;

        FiredCannons = new Stack();
        //Initialize the pool
        CannonPool.ObjectPool = new ObjectPool<GameObject>(() =>
        { return Instantiate(CannonPrefab); },
        dice => { dice.SetActive(true); },
        dice => {
            dice.SetActive(false); 
        },
        dice => { Destroy(dice); },
        false,
        MaxNoOfCannon.Value,
        MaxNoOfCannon.Value
        );
        InitialPos = LaunchPoint.transform.position;
        InitialRot = Quaternion.identity;
        PrepareNextCannon();
    }

    public void PrepareNextCannon() {
        if (AmmoCount.Value != 0)
        {
            GameObject newCannon = Instantiate(CannonPrefab);//parent -- , CannonBody.transform

            //CannonPool.ObjectPool.Get();

            newCannon.transform.position = InitialPos;
            newCannon.transform.rotation = Quaternion.identity;
            //newCannon.GetComponent<Rigidbody2D>().centerOfMass =
            //  CannonPrefab.GetComponent<Rigidbody2D>().centerOfMass;
            //CannonParent.transform.position = CannonParentPos;
            //CannonParent.transform.rotation = Quaternion.identity;

            //newCannon.transform.parent = CannonParent.transform;
            newCannon.SetActive(true);
            LevelTotalAmmo.Value++;
        }
        else {
            CheckGameOverEvent.Raise();
        }
    }
}
