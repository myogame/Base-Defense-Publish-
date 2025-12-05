using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    Transform target;
    public float smoothSpeed;
    public Vector3 offset;
     

    private void Awake()
    {
        instance = this;
        target = transform.Find("/Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);
    }
}
