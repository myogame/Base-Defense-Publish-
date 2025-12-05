using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GemZone : MonoBehaviour
{
    int gemCurrent = 1;
    List<GameObject> gemList;

    void Awake()
    {
        gemList = new List<GameObject>();
        foreach (Transform child in transform)
        {
            gemList.Add(child.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ally_Mine" && other.gameObject.GetComponent<Mine_Ally_Controller>().carry)
        {
            Mine_Ally_Controller mineAlly = other.gameObject.GetComponent<Mine_Ally_Controller>();

            mineAlly.GemCarry.SetActive(false);

            if (gemCurrent < gemList.Count)
                gemList[gemCurrent].gameObject.SetActive(true);

            gemCurrent++;
            mineAlly.carry = false;

          

            if (Random.Range(0, 2) == 0)
            {
                mineAlly.rockTarget = Random.Range(0, 3);
                mineAlly.miner = true;
            }              
            else
                mineAlly.miner = false;
           
        }

        if(other.gameObject.tag == "Player")
        {
            if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) == 15)
                TutorialManager.ins.CheckStageTutorial();
            if(gemCurrent > 0)
            {
                getGem();
                GameManager.intance.gemText.DOCounter(StringManager.GetGem(), StringManager.GetGem() + gemCurrent, gemCurrent * 0.1f);
                StringManager.AddGem(gemCurrent);
                gemCurrent = 0;
            }
            
        }
    }

    void getGem()
    {
        FXManager.ins.ShowGemFX(gemCurrent);
        foreach (GameObject gem in gemList)
        {
            if (gem.activeSelf)
            {
                
                gem.gameObject.SetActive(false);

            }
            else
            {
                break;
            }

        }
        //AdsManager.intance.ShowInterstitial();

    }
}
