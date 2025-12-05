using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSound : MonoBehaviour
{
    public float timelife = 1f;
    float existTime;
    public string tags;
    ObjectPooler objectPooler;

    private void Awake()
    {
        objectPooler = ObjectPooler.instance;
    }

    private void OnEnable()
    {
        existTime = timelife;

    }

    // Update is called once per frame
    void Update()
    {
        if (existTime <= 0)
        {
           objectPooler.EnQueueObject(tags, gameObject);
        }
        else existTime -= Time.deltaTime;

    }
}
