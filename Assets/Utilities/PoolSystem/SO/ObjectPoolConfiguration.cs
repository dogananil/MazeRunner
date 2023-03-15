using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPoolConfiguration", menuName = "Create/Pools/Object Pool Configuration")]
public class ObjectPoolConfiguration : ScriptableObject
{
    public enum ObjectType
    {
        Wall,
        PowerUp
    }

    public ObjectType objectType;
    public GameObject prefab;
    public int poolSize = 10;
}
