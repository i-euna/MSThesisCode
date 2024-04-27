using UnityEngine;
using UnityEngine.Pool;


[CreateAssetMenu(menuName = "Variable/ObjectPool")]
public class ObjectPoolVariable : ScriptableObject
{
    //Object Pool
    public ObjectPool<GameObject> ObjectPool;
}
