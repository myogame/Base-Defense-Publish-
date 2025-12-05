using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally_Gun_Trigger : MonoBehaviour
{
    public Ally_Gun_Controller ally_Gun_Controller;

    private void Awake()
    {
        ally_Gun_Controller = transform.parent.GetComponent<Ally_Gun_Controller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !ally_Gun_Controller.isOnBase)
        {

            EnemyControler enemy = other.gameObject.GetComponent<EnemyControler>();

            if (!enemy.mute)
            {
                enemy.currentTarget = ally_Gun_Controller.gameObject.transform;
                enemy.agent.speed = 2;
                enemy.animator.SetBool("Run", true);
                
            }

        }

     

    }

  
}
