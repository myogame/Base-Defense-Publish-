using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class Ally_Money : MonoBehaviour
{

    public Transform moneyPos;
    public Transform basePos;
    NavMeshAgent agent;
    Animator animator;
    public GameObject[] moneyArray;
    public int moneyCarry;
    public int maxMoneyCarry;
    public bool maxCarry;
    public float targetOnTime;
    bool isStart = true;
    public GameObject[] starLevel;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        basePos = GameManager.intance.gate_Money;
        UpgradeSpeed();
    }

    private void Update()
    {
        agent.enabled = true;

        if (isStart)
        {
            
            agent.destination = basePos.position;
           

            if (Vector3.Distance(basePos.position, transform.position) >= agent.stoppingDistance + 1)
            {
                agent.isStopped = false;
                animator.SetBool("Run", true);
                //transform.LookAt(basePos.position);
            }
            else
            {
                isStart = false;

            }
        }
        else
        {
            if (!maxCarry)
            {
                if (moneyPos == null)
                {

                    moneyPos = findCurrentTarget();
                    animator.SetBool("Run", false);
                    agent.isStopped = true;
                  

                }
                else
                {
                    if (moneyPos.gameObject.activeSelf)
                    {
                        agent.destination = moneyPos.position;
                        animator.SetBool("Run", true);
                        agent.isStopped = false;

                        if (targetOnTime > 60)
                        {
                            ObjectPooler.instance.EnQueueObject("Money", moneyPos.gameObject);
                            targetOnTime = 0;
                        }
                        else targetOnTime += Time.deltaTime;

                    }
                    else
                    {
                        moneyPos = null;
                    }

                }
            }
            else
            {
                agent.isStopped = false;
                agent.destination = basePos.position;
                animator.SetBool("Run", true);
                targetOnTime = 0;
            }

        }

        


    }

    public void UpgradeSpeed()
    {
        agent.speed = PlayerPrefs.GetFloat(StringManager.SPEED_MONEY);
        float star = (PlayerPrefs.GetFloat(StringManager.SPEED_MONEY) / 0.5f) -3;

        if (star > 6) star = 6;

        for(int i = 0; i < star; i++)
        {
            starLevel[i].SetActive(true);
        }
    }
    public Transform findCurrentTarget()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Money");
       

        if(enemies != null)
        {
            Transform target = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject potentialTarget in enemies)
            {
                if (Vector3.Distance(transform.position, potentialTarget.transform.position) < closestDistance && potentialTarget != null && potentialTarget.activeSelf)
                {
                    closestDistance = Vector3.Distance(transform.position, potentialTarget.transform.position);
                    target = potentialTarget.transform;
                }
            }

            if (target)
                return target;

        }

        


        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnterBase")
        {
            GameManager.intance.moneyText.DOCounter(StringManager.GetMoney(), StringManager.GetMoney() + (moneyCarry), 1);
            StringManager.AddMoney(moneyCarry);
            setMoneyInBase();
        }
      
    }



    void setMoneyInBase()
    {
        if (moneyCarry > 0)
        {


            foreach (GameObject money in moneyArray)
            {
                if (money.activeSelf)
                {
                    money.transform.DOShakeScale(0.5f, 1).OnComplete(() =>
                    {
                        money.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => {
                            money.SetActive(false);
                            money.transform.localScale = Vector3.one;
                        }).SetEase(Ease.InBounce);
                    }); ;
                }
            }


            moneyCarry = 0;

        }
        maxCarry = false;
    }

}
