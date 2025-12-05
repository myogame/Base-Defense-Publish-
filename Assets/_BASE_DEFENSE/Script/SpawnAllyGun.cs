using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAllyGun : MonoBehaviour
{
    List<Transform> spawnPos;
    public float time;
    float timeDelay;
    ObjectPooler objectPooler;

    private void Awake()
    {
        spawnPos = new List<Transform>();
        foreach(Transform child in transform)
        {
            spawnPos.Add(child.gameObject.transform);
        }
    }
    private void Start()
    {
        objectPooler = ObjectPooler.instance;
    }
    void Update()
    {
        if (timeDelay <= 0)
        {
        
            objectPooler.SpawnFormPool("FreeSoldier", spawnPos[Random.Range(0,spawnPos.Count)].position, Quaternion.identity);
            timeDelay = time;
        }
        else timeDelay -= Time.deltaTime;

    }
}
