using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HireZone : MonoBehaviour
{
    Turren_Controller turren_Controller;
    GameObject allyTurret;
    Image fill;
    public int price;
    bool hired;
    public int ID;
    public int idRewardAds;


    void Awake()
    {
        allyTurret = transform.parent.Find("Gun/tower/Ally_Turret").gameObject;
        turren_Controller = transform.parent.Find("Gun").GetComponent<Turren_Controller>();
        fill = transform.Find("Canvas/fill").GetComponent<Image>();
       

    }

    private void Start()
    {
        if (PlayerPrefs.GetInt(StringManager.ACTIVE_AUTO_TORRET + ID) == 1)
            ActiveAutomatic();
        else
            allyTurret.SetActive(false);


    }

  

    

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            fill.fillAmount += Time.deltaTime*0.7f;

            if(fill.fillAmount >= 1 && !hired)
            {
                hired = true;
                HireAlly();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fill.fillAmount = 0;
            hired = false;
        }
    }

    void HireAlly()
    {
        if(StringManager.GetMoney() >= price)
        {
            GameManager.intance.moneyText.DOCounter(StringManager.GetMoney(), StringManager.GetMoney() - price, 1);
            StringManager.AddMoney(-price);
            GetReward();

        }
       
    }

    void GetReward()
    {
        SoundManager.ins.PlaySound(1);

        if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) == 8)
            TutorialManager.ins.CheckStageTutorial();

        if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) < 8)
        {
            TutorialManager.ins.UnlockAllyBefore();
        }

        PlayerPrefs.SetInt(StringManager.ACTIVE_AUTO_TORRET + ID, 1);
        ActiveAutomatic();
    }

    private void Update()
    {
        //if (AdsManager.intance.checkRewardComplete)
        //{

        //    if (idRewardAds == AdsManager.intance.rewardNumber)
        //    {
        //        AdsManager.intance.checkRewardComplete = false;
        //        GetReward();
                
        //    }


        //}
    }

    void ActiveAutomatic()
    {
        GameManager.intance.actionPoint += 0.1f;
        turren_Controller.automatic = true;
        allyTurret.SetActive(true);
        gameObject.SetActive(false);
    }
}
