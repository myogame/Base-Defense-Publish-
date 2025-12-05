using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeWorker : MonoBehaviour
{
    Button close_Upgrade_Pop;
    GameObject UI_upgrade_worker;

    Button BTN_up_money_worker;
    GameObject UI_up_money_nor;
    GameObject UI_up_money_ads;
    int price_up_money_worker = 100;
    TextMeshProUGUI TXT_price_Money_Worker;
    TextMeshProUGUI TXT_level_Money_Worker;

    Button BTN_up_ammo_worker;
    GameObject UI_up_ammo_nor;
    GameObject UI_up_ammo_ads;
    int price_up_ammo_worker = 100;
    TextMeshProUGUI TXT_price_Ammo_Worker;
    TextMeshProUGUI TXT_level_Ammo_Worker;



    private void Awake()
    {
        UI_upgrade_worker = transform.Find("/Canvas/Upgrade_Worker").gameObject;
        close_Upgrade_Pop = transform.Find("/Canvas/Upgrade_Worker/BG/CloseBtn").GetComponent<Button>();
        //MONEY
        BTN_up_money_worker = transform.Find("/Canvas/Upgrade_Worker/BG/MONEY/UpgradeBtn").GetComponent<Button>();
        UI_up_money_nor = transform.Find("/Canvas/Upgrade_Worker/BG/MONEY/UpgradeBtn/Normal").gameObject;
        UI_up_money_ads = transform.Find("/Canvas/Upgrade_Worker/BG/MONEY/UpgradeBtn/Ads").gameObject;
        TXT_price_Money_Worker = transform.Find("/Canvas/Upgrade_Worker/BG/MONEY/UpgradeBtn/Normal/ValueUnlockMoney").GetComponent<TextMeshProUGUI>();
        TXT_level_Money_Worker = transform.Find("/Canvas/Upgrade_Worker/BG/MONEY/level").GetComponent<TextMeshProUGUI>();
        //AMMOR
        BTN_up_ammo_worker = transform.Find("/Canvas/Upgrade_Worker/BG/AMMOR/UpgradeBtn").GetComponent<Button>();
        UI_up_ammo_nor = transform.Find("/Canvas/Upgrade_Worker/BG/AMMOR/UpgradeBtn/Normal").gameObject;
        UI_up_ammo_ads = transform.Find("/Canvas/Upgrade_Worker/BG/AMMOR/UpgradeBtn/Ads").gameObject;
        TXT_price_Ammo_Worker = transform.Find("/Canvas/Upgrade_Worker/BG/AMMOR/UpgradeBtn/Normal/ValueUnlockMoney").GetComponent<TextMeshProUGUI>();
        TXT_level_Ammo_Worker = transform.Find("/Canvas/Upgrade_Worker/BG/AMMOR/level").GetComponent<TextMeshProUGUI>();

        

        

       
    }

    private void Start()
    {
        BTN_up_money_worker.onClick.AddListener(() => UpgradeMoneyWorker());
        BTN_up_ammo_worker.onClick.AddListener(() => UpgradeAmmoWorker());
        close_Upgrade_Pop.onClick.AddListener(() => Close());
        UI_upgrade_worker.SetActive(false);
    }

    void Close()
    {
        SoundManager.ins.PlaySound(0);
        UI_upgrade_worker.SetActive(false);
    }

    void UpgradeMoneyWorker()
    {
        if (StringManager.GetMoney() >= (price_up_money_worker * PlayerPrefs.GetFloat(StringManager.SPEED_MONEY)))
        {
            StringManager.AddMoney(-Mathf.RoundToInt(price_up_money_worker * PlayerPrefs.GetFloat(StringManager.SPEED_MONEY)));
            GameManager.intance.UP_StatsCanvas();
            GetMoneyWoker();
            //AdsManager.intance.ShowInterstitial();

        }
        else
        {
            //AdsManager.intance.ShowRewardedAd(13);
        }



    }

    void GetMoneyWoker()
    {
        SoundManager.ins.PlaySound(1);
        GameManager.intance.actionPoint += 0.1f;
        PlayerPrefs.SetFloat(StringManager.SPEED_MONEY, PlayerPrefs.GetFloat(StringManager.SPEED_MONEY) + 0.5f);
        UpdateAdsBtn();
        foreach (GameObject allyMoney in GameManager.intance.allyMoney)
        {
            if (allyMoney.GetComponent<Ally_Money>())
                allyMoney.GetComponent<Ally_Money>().UpgradeSpeed();
        }
        NofityManager.ins.Nofity("Up Level Success!");
    }

    void UpgradeAmmoWorker()
    {


        if (StringManager.GetMoney() >= (price_up_ammo_worker * PlayerPrefs.GetFloat(StringManager.SPEED_AMMO)))
        {
            StringManager.AddMoney(-Mathf.RoundToInt(price_up_ammo_worker * PlayerPrefs.GetFloat(StringManager.SPEED_AMMO)));
            GameManager.intance.UP_StatsCanvas();
            GetAmmoWorker();
            //AdsManager.intance.ShowInterstitial();

        }
        else
        {
            //AdsManager.intance.ShowRewardedAd(14);
        }


    }

    void GetAmmoWorker()
    {
        SoundManager.ins.PlaySound(1);
        GameManager.intance.actionPoint += 0.1f;
        PlayerPrefs.SetFloat(StringManager.SPEED_AMMO, PlayerPrefs.GetFloat(StringManager.SPEED_AMMO) + 0.5f);
        UpdateAdsBtn();
        foreach (GameObject allyAmmo in GameManager.intance.allyAmor)
        {
            allyAmmo.GetComponent<Allay_Turret_GetAmor>().UpgradeSpeed();
        }
        NofityManager.ins.Nofity("Up Level Success!");
    }

    private void Update()
    {
        //if(AdsManager.intance.checkRewardComplete)
        //{

        //    if(AdsManager.intance.rewardNumber == 13)
        //    {
        //        AdsManager.intance.checkRewardComplete = false;
        //        GetMoneyWoker();
               
        //    }

        //    if (AdsManager.intance.rewardNumber == 14)
        //    {
        //        AdsManager.intance.checkRewardComplete = false;
        //        GetAmmoWorker();
                
        //    }


        //}
    }

    void UpdateAdsBtn()
    {
        TXT_price_Money_Worker.text = (price_up_money_worker * PlayerPrefs.GetFloat(StringManager.SPEED_MONEY)).ToString();
        TXT_level_Money_Worker.text = "LEVEL " + ((PlayerPrefs.GetFloat(StringManager.SPEED_MONEY) * 2) - 2).ToString();
        TXT_price_Ammo_Worker.text = (price_up_ammo_worker * PlayerPrefs.GetFloat(StringManager.SPEED_AMMO)).ToString();
        TXT_level_Ammo_Worker.text = "LEVEL " + ((PlayerPrefs.GetFloat(StringManager.SPEED_AMMO) * 2) - 2).ToString();

        if (StringManager.GetMoney() >= (price_up_money_worker * PlayerPrefs.GetFloat(StringManager.SPEED_MONEY)))
        {
            UI_up_money_nor.SetActive(true);
            UI_up_money_ads.SetActive(false);
        }
        else
        {
            UI_up_money_nor.SetActive(false);
            UI_up_money_ads.SetActive(true);
        }

        if (StringManager.GetMoney() >= (price_up_ammo_worker * PlayerPrefs.GetFloat(StringManager.SPEED_AMMO)))
        {
            UI_up_ammo_nor.SetActive(true);
            UI_up_ammo_ads.SetActive(false);
        }
        else
        {
            UI_up_ammo_nor.SetActive(false);
            UI_up_ammo_ads.SetActive(true);
        }
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SoundManager.ins.PlaySound(0);
            UI_upgrade_worker.SetActive(true);
            UpdateAdsBtn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UI_upgrade_worker.SetActive(false);
        }
    }
}
