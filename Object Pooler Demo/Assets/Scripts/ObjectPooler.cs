using System.Collections.Generic;
using UnityEngine;
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;

    public List<PoolableObject> pooledObjects;
    private Dictionary<string, PoolableObject> keyedPoolableObjects = new Dictionary<string, PoolableObject>();

    private void Awake()
    {
        SharedInstance = this;
        SetUpPooler();
    }

    public void SetUpPooler()
    {
        foreach (PoolableObject op in pooledObjects)
        {
            op.objects = new List<GameObject>();

            GameObject parent = new GameObject(name = op.objectName);
            parent.transform.parent = transform;

            GameObject temp;

            for (int i = 0; i < op.poolQuantity; i++)
            {
                temp = Instantiate(op.objectToPool, parent.transform);
                temp.SetActive(false);
                op.objects.Add(temp);
            }

            keyedPoolableObjects.Add(op.objectToPool.name, op);
        }

        foreach (var v in keyedPoolableObjects)
            Debug.Log(v.Key);
        pooledObjects = null; //we don't need these now that we have converted them to a dictionary.

        gameObject.name = "Object Pooler";
    }

    private PoolableObject GetObject(string name)
    {
        PoolableObject pool = keyedPoolableObjects[name];
        return pool ?? null;
    }

    public GameObject GetPooledObject(string name) //this is called when we want to spawn a new object. I.E bullet,
    {
        PoolableObject pool = GetObject(name);
        if (pool == null) { Debug.Log(name + " is null"); return null; }

        for (int i = 0; i < pool.poolQuantity; i++)
        {
            if (!pool.objects[i].activeInHierarchy)
            {
                return pool.objects[i];
            }
        }
        return null;
    }

    public GameObject GetPooledObjectParent(string name)
    {
        PoolableObject pool = GetObject(name);
        return pool.objects[0].transform.parent.gameObject ?? null;
    }

    public void ResetSceneObjects()
    {
        //we call this when the player dies and we want to "destroy" other objects in the scene
        foreach (KeyValuePair<string, PoolableObject> kv in keyedPoolableObjects)
        {
            foreach (var op in kv.Value.objects)
            {
                op.SetActive(false);
            }
        }
    }
}
