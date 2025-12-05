using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    Transform[] point;
    public float time;
    float timeDelay;
    int enemyCapSpawnValue;
    ObjectPooler objectPooler;
    private void Start()
    {
        objectPooler = ObjectPooler.instance;

        point = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            point[i] = transform.GetChild(i);
        }
    }
    void Update()
    {
        if (timeDelay <= 0)
        {
            objectPooler.SpawnFormPool("Enemy", point[Random.Range(1, point.Length)].position, Quaternion.identity);
            timeDelay = time - (GameManager.intance.actionPoint*0.1f);
            enemyCapSpawnValue++;

            if(enemyCapSpawnValue > 30)
            {
                objectPooler.SpawnFormPool("Enemy_Captain", point[Random.Range(1, point.Length)].position, Quaternion.identity);
                enemyCapSpawnValue = 0;
            }
        }
        else timeDelay -= Time.deltaTime;

    }

}
