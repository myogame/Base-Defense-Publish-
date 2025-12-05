using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageTrigger : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !PlayerControler.instance.enter_Base)
        {
            
            EnemyControler enemy = other.gameObject.GetComponent<EnemyControler>();

            if (!enemy.mute) 
            {
                enemy.attackTag = "Player";
                enemy.currentTarget = PlayerControler.instance.gameObject.transform;
                enemy.agent.speed = 2;
                enemy.animator.SetBool("Run", true);
                
            }

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !PlayerControler.instance.enter_Base)
        {

                PlayerControler.instance.target = PlayerControler.instance.findCurrentTarget();

        }

        if (other.gameObject.tag == "Boss" && !PlayerControler.instance.enter_Base)
        {

            PlayerControler.instance.target = other.gameObject.transform;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyControler enemy = other.gameObject.GetComponent<EnemyControler>();

            if (!enemy.mute)
            {
                enemy.FindTurret();
            }

            PlayerControler.instance.target = null;

        
        }

        if (other.gameObject.tag == "BossZone")
        {

            BossController.instance.Free();

        }

        if (other.gameObject.tag == "Boss" && !PlayerControler.instance.enter_Base)
        {

            PlayerControler.instance.target = null;

        }
    }
}
