using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Camera : MonoBehaviour
{

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

}
