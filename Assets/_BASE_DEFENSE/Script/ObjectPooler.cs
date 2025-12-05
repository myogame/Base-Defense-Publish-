using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefabsPool;
        public GameObject group;
        public int size;
    }
    public List<Pool> listPool;
    public Dictionary<string, Queue<GameObject>> poolDic;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        poolDic = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in listPool)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i< pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefabsPool);
                obj.SetActive(false);
                obj.transform.parent = pool.group.transform;
                objectPool.Enqueue(obj);

            }

            poolDic.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFormPool (string tag, Vector3 position, Quaternion rotation)
    {
        if (poolDic[tag].Count > 0)
        {
            GameObject objectToSpawn = poolDic[tag].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            return objectToSpawn;
        }
        else return null;
        
    }

    public void EnQueueObject(string tag, GameObject objectToDestroy)
    {
        poolDic[tag].Enqueue(objectToDestroy);
        //objectToDestroy.transform.position = new Vector3(0, 20, 0);
        objectToDestroy.SetActive(false);
    }

   

}
