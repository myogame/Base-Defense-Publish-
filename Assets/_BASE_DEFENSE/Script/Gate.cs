using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gate : MonoBehaviour
{
    GameObject gate;

    void Awake()
    {
        gate = GameObject.Find("Base/Gate/Gate_Stick");
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag =="Ally_Money" || other.gameObject.tag == "Ally_Gun" || other.gameObject.tag == "SodierFree")
        {
            gate.transform.DORotate(new Vector3(0, 0, -100), 0.2f);
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ally_Money" || other.gameObject.tag == "Ally_Gun" || other.gameObject.tag == "SodierFree")
        {
            gate.transform.DORotate(new Vector3(0, 0, -180), 0.5f);
        }
    }
}
