using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private Dictionary<string, List<GameObject>> _pools = new Dictionary<string, List<GameObject>>();

    public GameObject GetObjectFromPool(string objectName)
    {
        if (_pools.ContainsKey(objectName))
        {
            foreach (GameObject obj in _pools[objectName])
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }
        }

        // If no inactive objects are found in the pool, create a new one and add it to the pool
        GameObject newObj = Instantiate(Resources.Load<GameObject>(objectName), transform);
        newObj.name = objectName;

        if (!_pools.ContainsKey(objectName))
        {
            _pools.Add(objectName, new List<GameObject>());
        }

        _pools[objectName].Add(newObj);

        return newObj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
