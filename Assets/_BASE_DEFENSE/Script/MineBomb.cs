using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MineBomb : MonoBehaviour
{
    Image fill;
    public int price;
    bool buyStop;
    GameObject deadzone;
    float waittime = 60;
    bool wait;
    TextMeshProUGUI timeCountDown;
    GameObject adsText;
    GameObject gemText;
    GameObject countdownText;
    public int idRewardAds;

    void Awake()
    {
        fill = transform.Find("Canvas/fill").GetComponent<Image>();
        deadzone = transform.Find("Canvas/Deadzone").gameObject;
        timeCountDown = transform.Find("Canvas/Price/countdown/Text").GetComponent<TextMeshProUGUI>();
        adsText = transform.Find("Canvas/Price/Ads").gameObject;
        gemText = transform.Find("Canvas/Price/Gem").gameObject;
        countdownText = transform.Find("Canvas/Price/countdown").gameObject;

    }

    private void Start()
    {
        DetectStatus();
    }

    private void Update()
    {
        if (wait)
        {
            adsText.SetActive(false);
            gemText.SetActive(false);
            countdownText.SetActive(true);
            if(waittime <= 0)
            {
                wait = false;
                waittime = 60;
            }
            else
            {
                waittime -= Time.deltaTime;
                timeCountDown.text = Mathf.RoundToInt(waittime).ToString() + "s";
            }
        }


        //if (AdsManager.intance.checkRewardComplete)
        //{

        //    if (idRewardAds == AdsManager.intance.rewardNumber)
        //    {
        //        AdsManager.intance.checkRewardComplete = false;
        //        GetReward();
                
        //    }


        //}

    }

    public void DetectStatus()
    {
        countdownText.SetActive(false);
        if (StringManager.GetGem() >= price)
        {
            gemText.SetActive(true);
            adsText.SetActive(false);
        }
        else
        {
            adsText.SetActive(true);
            gemText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RangeCheck")
        {
            DetectStatus();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !buyStop && !wait)
        {
            fill.fillAmount += Time.deltaTime * 0.7f;

            if (fill.fillAmount >= 1)
            {
                ActicveBomb();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fill.fillAmount = 0;
            buyStop = false;
        }
    }

    void ActicveBomb()
    {
        if (StringManager.GetGem() >= price)
        {
            GameManager.intance.gemText.DOCounter(StringManager.GetGem(), StringManager.GetGem() - price, 1);
            StringManager.AddGem(-price);
            GetReward();
            //AdsManager.intance.ShowInterstitial();

        }
        //else
        //{
        //    buyStop = true;
        //    AdsManager.intance.ShowRewardedAd(idRewardAds);
        //}

    }



    void GetReward()
    {
        SoundManager.ins.PlaySound(1);
        buyStop = true;
        StringManager.AddQuestActiveBomb(1);
        findAllObject();
        deadzone.SetActive(true);
        wait = true;
    }

    


    void findAllObject()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject potentialTarget in enemies)
        {
            potentialTarget.GetComponent<EnemyControler>().MoveToBomb(gameObject.transform);
        }

    }
}
