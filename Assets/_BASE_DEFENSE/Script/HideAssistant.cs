using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Worker
{
    Money,
    Amor
}
public class HideAssistant : MonoBehaviour
{
    public GameObject assistantObject;
    public GameObject alllyDemo;
    Image fill;
    public int price;
    bool buyStop;
    public Worker wokerType;
    public int idRewardAds;


    void Awake()
    {
        fill = transform.Find("Canvas/fill").GetComponent<Image>();
    }

    private void Start()
    {

        switch (wokerType)
        {
            case Worker.Amor:
                for (int i = 0; i < PlayerPrefs.GetInt(StringManager.BULLET_WORKER); i++)
                {
                    IntAssistantAmor();
                }
                break;
            case Worker.Money:
                for (int i = 0; i < PlayerPrefs.GetInt(StringManager.MONEY_WORKER); i++)
                {
                    IntAssistantMoney();
                }
                break;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !buyStop )
        {
            fill.fillAmount += Time.deltaTime * 0.7f;

            if (fill.fillAmount >= 1)
            {
                HireAlly();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fill.fillAmount = 0;
            buyStop = false;
            alllyDemo.SetActive(true);
        }
    }

    void HireAlly()
    {
        if (StringManager.GetMoney() >= price)
        {
            GameManager.intance.moneyText.DOCounter(StringManager.GetMoney(), StringManager.GetMoney() - price, 1);
            StringManager.AddMoney(-price);


            switch (wokerType)
            {
                case Worker.Amor:
                    GetRewardAmor();
                    break;
                case Worker.Money:
                    GetRewardMoney();
                    break;
            }
            
            //AdsManager.intance.ShowInterstitial();

        }
        //else
        //{
        //    buyStop = true;
        //    AdsManager.intance.ShowRewardedAd(idRewardAds);
        //}

    }

    private void Update()
    {
        //if (AdsManager.intance.checkRewardComplete)
        //{
        //    if(idRewardAds == AdsManager.intance.rewardNumber)
        //    {
        //        AdsManager.intance.checkRewardComplete = false;
        //        switch (wokerType)
        //        {
        //            case Worker.Amor:
        //                GetRewardAmor();
        //                break;
        //            case Worker.Money:
        //                GetRewardMoney();
        //                break;
        //        }
        //    }

        //}
    }

    void GetRewardMoney()
    {
        SoundManager.ins.PlaySound(1);
        if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) == 14)
            TutorialManager.ins.CheckStageTutorial();

        StringManager.AddQuestHire(1);
        buyStop = true;
        alllyDemo.SetActive(false);
        PlayerPrefs.SetInt(StringManager.MONEY_WORKER, PlayerPrefs.GetInt(StringManager.MONEY_WORKER) + 1);


        IntAssistantMoney();
    }

    void GetRewardAmor()
    {
        SoundManager.ins.PlaySound(1);
        if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) == 14)
            TutorialManager.ins.CheckStageTutorial();

        StringManager.AddQuestHire(1);
        buyStop = true;
        alllyDemo.SetActive(false);
        PlayerPrefs.SetInt(StringManager.BULLET_WORKER, PlayerPrefs.GetInt(StringManager.BULLET_WORKER) + 1);

        IntAssistantAmor();
    }



    void IntAssistantMoney()
    {
        
        GameManager.intance.actionPoint += 0.1f;
        GameObject woker = Instantiate(assistantObject, alllyDemo.transform.position, Quaternion.identity);
        GameManager.intance.allyMoney.Add(woker);


    }

    void IntAssistantAmor()
    {

        GameManager.intance.actionPoint += 0.1f;
        GameObject woker = Instantiate(assistantObject, alllyDemo.transform.position, Quaternion.identity);
        GameManager.intance.allyAmor.Add(woker);

    }


}
