using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class Allay_Turret_GetAmor : MonoBehaviour
{
    public Transform turretGun;
    Transform deskAmor;
    public float restTime;
    NavMeshAgent agent;
    public bool reload;
    Animator animator;
    public bool bullet_Carry;
    public List<GameObject> bullet_list;
    public GameObject[] starLevel;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        deskAmor = GameManager.intance.bullet_desk;
        UpgradeSpeed();

    }

    private void Update()
    {
        agent.enabled = true;

        if(restTime <= 0)
        {
            if (!reload)
            {
                agent.destination = turretGun.position;
            }
                
            else
                agent.destination = deskAmor.position;

            animator.SetBool("Run", true);
            agent.isStopped = false;
        }
        else
        {
            restTime -= Time.deltaTime;
            animator.SetBool("Run", false);
            agent.isStopped = true;

        }

        
    }

    public void UpgradeSpeed()
    {
        agent.speed = PlayerPrefs.GetFloat(StringManager.SPEED_AMMO);

        float star = (PlayerPrefs.GetFloat(StringManager.SPEED_AMMO) / 0.5f)-3;

        if (star > 6) star = 6;

        for (int i = 0; i < star; i++)
        {
            starLevel[i].SetActive(true);
        }
    }

    public Transform findCurrentTarget_Random()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Turret_Bullet_Zone");
        Transform target = enemies[Random.Range(0, enemies.Length)].transform;

        if (target)
            return target;


        return null;
    }

}
