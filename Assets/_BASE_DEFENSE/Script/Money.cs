using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Money : MonoBehaviour
{
    public GameObject moneyPrefabs;
    PlayerControler playerControler;

    private void Awake()
    {
        playerControler = PlayerControler.instance;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !playerControler.enter_Base)
        {

            if (playerControler.moneyCarry < PlayerPrefs.GetInt(StringManager.CAP_PLAYER))
            {
                SoundManager.ins.PlaySound(4);
                transform.DOMove(playerControler.money_list[playerControler.moneyCarry].transform.position, 0.2f).OnComplete(() =>
                {
                    playerControler.money_list[playerControler.moneyCarry].SetActive(true);
                        //playerControler.money_list[playerControler.moneyCarry].transform.DOShakeScale(1f, 1f);
                        playerControler.moneyCarry++;

                    ObjectPooler.instance.EnQueueObject("Money", gameObject);

                });

            }
            else
            {

                playerControler.maxOb.SetActive(true);
            }


        }


        if (other.gameObject.tag == "Ally_Money")
        {
            
            Ally_Money allymoney = other.gameObject.GetComponent<Ally_Money>();

            if (allymoney.moneyCarry < allymoney.maxMoneyCarry)
            {
                
                transform.DOMove(allymoney.moneyArray[allymoney.moneyCarry].transform.position, 0.2f).OnComplete(() =>
                {
                    allymoney.moneyCarry++;
                    allymoney.moneyArray[allymoney.moneyCarry].SetActive(true);
                    //allymoney.moneyArray[allymoney.moneyCarry].transform.DOShakeScale(1f, 1f);

                   
                    ObjectPooler.instance.EnQueueObject("Money", gameObject);

                });

            }
            else
            {
                
                allymoney.maxCarry = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag == "UnderFloor")
        {
         
            ObjectPooler.instance.EnQueueObject("Money", gameObject);
        }

    }


}
