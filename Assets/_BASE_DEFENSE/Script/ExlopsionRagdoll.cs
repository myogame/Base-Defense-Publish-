using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExlopsionRagdoll : MonoBehaviour
{
    Rigidbody rg;
    public Transform head;
    public int power;

    private void Awake()
    {
        rg = GetComponent<Rigidbody>();

    }
    private void OnEnable()
    {
        rg.AddExplosionForce(power, head.position,50,0f, ForceMode.Impulse);
        //rg.AddForceAtPosition(head.position, transform.position, ForceMode.Impulse);
    }


}
