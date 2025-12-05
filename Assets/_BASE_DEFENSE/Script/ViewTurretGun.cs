using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTurretGun : MonoBehaviour
{
    Turren_Controller turren_Controller;

    void Awake()
    {
        turren_Controller = transform.parent.Find("Gun").GetComponent<Turren_Controller>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (turren_Controller.automatic)
            {
                if(turren_Controller.target == null)
                {
                    turren_Controller.target = other.gameObject.transform;
                }
                else
                {
                    
                    if (!turren_Controller.target.gameObject.activeSelf)
                    {
                        turren_Controller.target = null;
                    }
                       
                }
                
            }
        }
    }
}
