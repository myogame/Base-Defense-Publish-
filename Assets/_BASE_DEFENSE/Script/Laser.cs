using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    public int lenght = 100;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
       
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + transform.forward*lenght);

    }
}
