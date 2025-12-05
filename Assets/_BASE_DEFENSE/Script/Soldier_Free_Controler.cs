using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Soldier_Free_Controler : MonoBehaviour
{
    bool ishelp = false;
    bool isBase = false;
    Animator animator;
    NavMeshAgent agent;
    Transform player;
    Image fill;
    public GameObject canvasHelp;

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
        agent = GetComponent<NavMeshAgent>();
        fill = transform.Find("Canvas/Image").GetComponent<Image>();
    }


    private void Update()
    {
        if (!ishelp)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Help", true);
        }
        else
        {
            agent.enabled = true;
            animator.SetBool("Help", false);
            animator.SetBool("Run", true);

            if (isBase)
            {
                agent.destination = GameManager.intance.doorHospital.transform.position;
            }
            else
            {
                
                agent.destination = player.position;
            }

            

        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fill.fillAmount += Time.deltaTime * 0.7f;

            if (fill.fillAmount >= 1 && !ishelp )
            {
                SoundManager.ins.PlaySound(5);
                ishelp = true;
                player = other.gameObject.transform;
                fill.gameObject.SetActive(false);
                canvasHelp.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            fill.fillAmount = 0;
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "EnterBase")
        {
            isBase = true;
        

        }

        if(other.gameObject.tag == "DoorHospital")
        {
            MedicManager.ins.AddSoldier(+1);
            ResetSoldier();
            ObjectPooler.instance.EnQueueObject("FreeSoldier", gameObject);
           
        }
    }

    void ResetSoldier()
    {
        ishelp = false;
        isBase = false;
        transform.position = GameManager.intance.startMovePos.position;
        fill.gameObject.SetActive(true);
        canvasHelp.SetActive(true);
    }
}
