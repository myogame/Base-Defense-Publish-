using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mine_Ally_Controller : MonoBehaviour
{
    Transform workMine;
    [HideInInspector] public Transform[] rocks;
    Transform invenMine;
    NavMeshAgent agent;
    [HideInInspector] public Animator animator;

    public GameObject GemCarry;
    public GameObject helpCanavs;
    public GameObject pickAxe;

    bool help;
    Transform player;
    bool work;
    [HideInInspector] public bool carry;
    [HideInInspector] public bool miner;
    [HideInInspector] public int rockTarget;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

      

        
            
    }
    private void Start()
    {
        workMine = GameManager.intance.cartMine;
        invenMine = GameManager.intance.ivenMine;

        for (int i = 0; i < 3; i++)
        {
            rocks[i] = GameManager.intance.rockMine[i];
        }
    }

    private void Update()
    {
        agent.enabled = true;
       
        if (!help)
        {
            animator.SetBool("Help", true);
        }
        else
        {
            

            if (work)
            {

                if (carry)
                {

                    animator.SetBool("Walk", false);
                    animator.SetBool("Carry", true);
                    agent.destination = invenMine.position;
                }
                else
                {
                    if (miner)
                    {
                        agent.destination = rocks[rockTarget].position;
                        animator.SetBool("Walk", true);
                        animator.SetBool("Carry", false);
                    }
                    else
                    {
                        if (!MineCart.instance.open)
                        {
                            
                            miner = true;
                        }
                        else
                        {
                            animator.SetBool("Walk", true);
                            animator.SetBool("Carry", false);
                            agent.isStopped = false;
                            agent.destination = workMine.position;
                        }
                    }

                    

                }
            }
            else
            {
                animator.SetBool("Help", false);
                animator.SetBool("Run", true);
                agent.destination = player.position;
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && !help)
        {
            if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) == 11)
                TutorialManager.ins.CheckStageTutorial();

            SoundManager.ins.PlaySound(5);
            gameObject.layer = 8;
            helpCanavs.SetActive(false);
            help = true;
            player = collision.gameObject.transform;
            agent.speed = 3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "EnterBase")
        {

            if (!work)
            {
                if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) == 12)
                    TutorialManager.ins.CheckStageTutorial();

                PlayerPrefs.SetInt(StringManager.MINE_WORKER, PlayerPrefs.GetInt(StringManager.MINE_WORKER) + 1);
                StringManager.AddQuestRescue(1);
                work = true;
                animator.SetBool("Run", false);
                agent.speed = 1f;
            }

        }
    }



    public void Work()
    {
        gameObject.layer = 8;
        helpCanavs.SetActive(false);
        help = true;
        work = true;
        animator.SetBool("Walk", true);

        
    }
}
