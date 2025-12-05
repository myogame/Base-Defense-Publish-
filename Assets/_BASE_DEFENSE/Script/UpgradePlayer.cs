using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradePlayer : MonoBehaviour
{
    GameObject ui_Upgrade_Player;
    Button ui_Close_Up_Player;

    TextMeshProUGUI nextSpeedText;
    Button speedUpPlayerBtn;
    GameObject speedTextNorm;
    GameObject speedTextAds;
    TextMeshProUGUI valueMoneySpeed;

    TextMeshProUGUI nextCapText;
    Button capUpPlayerBtn;
    GameObject capTextNorm;
    GameObject capTextAds;
    TextMeshProUGUI valueMoneyCap;

    TextMeshProUGUI nextHeathText;
    Button heathUpPlayerBtn;
    GameObject heathTextNorm;
    GameObject heathTextAds;
    TextMeshProUGUI valueMoneyHeath;


    private void Awake()
    {
        ui_Upgrade_Player = transform.Find("/Canvas/Upgrade_Player").gameObject;
        ui_Close_Up_Player = transform.Find("/Canvas/Upgrade_Player/BG/CloseBtn").GetComponent<Button>();

        //SPEED
        nextSpeedText = transform.Find("/Canvas/Upgrade_Player/BG/SPEED/TextValueSpeed").GetComponent<TextMeshProUGUI>();
        speedUpPlayerBtn = transform.Find("/Canvas/Upgrade_Player/BG/SPEED/UpgradeBtn").GetComponent<Button>();
        speedTextNorm = transform.Find("/Canvas/Upgrade_Player/BG/SPEED/UpgradeBtn/Normal").gameObject;
        speedTextAds = transform.Find("/Canvas/Upgrade_Player/BG/SPEED/UpgradeBtn/Ads").gameObject;
        speedTextAds.SetActive(false);
        valueMoneySpeed = transform.Find("/Canvas/Upgrade_Player/BG/SPEED/UpgradeBtn/Normal/ValueUnlockMoney").GetComponent<TextMeshProUGUI>();
        //CAPACITY
        nextCapText = transform.Find("/Canvas/Upgrade_Player/BG/CAPACITY/TextValueSpeed").GetComponent<TextMeshProUGUI>();
        capUpPlayerBtn = transform.Find("/Canvas/Upgrade_Player/BG/CAPACITY/UpgradeBtn").GetComponent<Button>();
        capTextNorm = transform.Find("/Canvas/Upgrade_Player/BG/CAPACITY/UpgradeBtn/Normal").gameObject;
        capTextAds = transform.Find("/Canvas/Upgrade_Player/BG/CAPACITY/UpgradeBtn/Ads").gameObject;
        capTextAds.SetActive(false);
        valueMoneyCap = transform.Find("/Canvas/Upgrade_Player/BG/CAPACITY/UpgradeBtn/Normal/ValueUnlockMoney").GetComponent<TextMeshProUGUI>();
        //HEATH
        nextHeathText = transform.Find("/Canvas/Upgrade_Player/BG/HEATH/TextValueSpeed").GetComponent<TextMeshProUGUI>();
        heathUpPlayerBtn = transform.Find("/Canvas/Upgrade_Player/BG/HEATH/UpgradeBtn").GetComponent<Button>();
        heathTextNorm = transform.Find("/Canvas/Upgrade_Player/BG/HEATH/UpgradeBtn/Normal").gameObject;
        heathTextAds = transform.Find("/Canvas/Upgrade_Player/BG/HEATH/UpgradeBtn/Ads").gameObject;
        heathTextAds.SetActive(false);
        valueMoneyHeath = transform.Find("/Canvas/Upgrade_Player/BG/HEATH/UpgradeBtn/Normal/ValueUnlockMoney").GetComponent<TextMeshProUGUI>();
        ui_Upgrade_Player.SetActive(false);

        ui_Close_Up_Player.onClick.AddListener(() => ClosePopup());
        speedUpPlayerBtn.onClick.AddListener(() => SpeedUpgrade());
        heathUpPlayerBtn.onClick.AddListener(() => HeathUpgrade());
        capUpPlayerBtn.onClick.AddListener(() => CapUpgrade());
        

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SoundManager.ins.PlaySound(0);
            ui_Upgrade_Player.SetActive(true);
            UpdateStatsPlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ui_Upgrade_Player.SetActive(false);
        }
    }

    void ClosePopup()
    {
        SoundManager.ins.PlaySound(0);
        ui_Upgrade_Player.SetActive(false);
        //AdsManager.intance.ShowInterstitial();

    }

    void SpeedUpgrade()
    {
        if (StringManager.GetMoney() >= PlayerPrefs.GetInt(StringManager.SPEED_PLAYER_MONEY))
        {
            StringManager.AddMoney(-PlayerPrefs.GetInt(StringManager.SPEED_PLAYER_MONEY));
            GameManager.intance.UP_StatsCanvas();
            GetSpeed();
            //AdsManager.intance.ShowInterstitial();

        }
        else
        {
            //AdsManager.intance.ShowRewardedAd(15);
        }
    }

    void CapUpgrade()
    {
        if (StringManager.GetMoney() >= PlayerPrefs.GetInt(StringManager.CAP_PLAYER_MONEY))
        {
            StringManager.AddMoney(-PlayerPrefs.GetInt(StringManager.CAP_PLAYER_MONEY));
            GameManager.intance.UP_StatsCanvas();
            GetCapacity();
            //AdsManager.intance.ShowInterstitial();

        }
        else
        {
            //AdsManager.intance.ShowRewardedAd(16);
        }
    }


    void HeathUpgrade()
    {
        if (StringManager.GetMoney() >= PlayerPrefs.GetInt(StringManager.HEATH_PLAYER_MONEY))
        {
            StringManager.AddMoney(-PlayerPrefs.GetInt(StringManager.HEATH_PLAYER_MONEY));
            GameManager.intance.UP_StatsCanvas();
            GetHeath();
            //AdsManager.intance.ShowInterstitial();

        }
        else
        {
            //AdsManager.intance.ShowRewardedAd(17);
        }
    }

    void GetSpeed()
    {
        SoundManager.ins.PlaySound(1);
        GameManager.intance.actionPoint += 0.1f;
        
        PlayerPrefs.SetFloat(StringManager.SPEED_PLAYER, PlayerPrefs.GetFloat(StringManager.SPEED_PLAYER) + 0.5f);
        PlayerPrefs.SetInt(StringManager.SPEED_PLAYER_MONEY, PlayerPrefs.GetInt(StringManager.SPEED_PLAYER_MONEY) * 2);
        UpdateStatsPlayer();
        PlayerControler.instance.UpgradeSpeed();
        NofityManager.ins.Nofity("Up Speed Success!");
    }

    void GetCapacity()
    {
        SoundManager.ins.PlaySound(1);
        GameManager.intance.actionPoint += 0.1f;
        
        PlayerPrefs.SetInt(StringManager.CAP_PLAYER, PlayerPrefs.GetInt(StringManager.CAP_PLAYER) + 5);
        PlayerPrefs.SetInt(StringManager.CAP_PLAYER_MONEY, PlayerPrefs.GetInt(StringManager.CAP_PLAYER_MONEY) * 2);
        UpdateStatsPlayer();
        PlayerControler.instance.UpgradeCap();
       
        NofityManager.ins.Nofity("Up Capacity Success!");
    }

    void GetHeath()
    {
        SoundManager.ins.PlaySound(1);
        GameManager.intance.actionPoint += 0.1f;
        
        PlayerPrefs.SetInt(StringManager.HEATH_PLAYER, PlayerPrefs.GetInt(StringManager.HEATH_PLAYER) + 50);
        PlayerPrefs.SetInt(StringManager.HEATH_PLAYER_MONEY, PlayerPrefs.GetInt(StringManager.HEATH_PLAYER_MONEY) * 2);
        UpdateStatsPlayer();
        PlayerControler.instance.UpgradeHeath();
      
        NofityManager.ins.Nofity("Up Heath Success!");
    }


    private void Update()
    {
        //if (AdsManager.intance.checkRewardComplete)
        //{

        //    if (AdsManager.intance.rewardNumber == 15)
        //    {
        //        AdsManager.intance.checkRewardComplete = false;
        //        GetSpeed();
                
        //    }

        //    if (AdsManager.intance.rewardNumber == 16)
        //    {
        //        AdsManager.intance.checkRewardComplete = false;
        //        GetCapacity();
                
        //    }

        //    if (AdsManager.intance.rewardNumber == 17)
        //    {
        //        AdsManager.intance.checkRewardComplete = false;
        //        GetHeath();
                
        //    }


        //}
    }

    void UpdateStatsPlayer()
    {
        valueMoneyHeath.text = PlayerPrefs.GetInt(StringManager.HEATH_PLAYER_MONEY).ToString();
        nextHeathText.text = "Next: " + PlayerPrefs.GetInt(StringManager.HEATH_PLAYER).ToString();
        valueMoneyCap.text = PlayerPrefs.GetInt(StringManager.CAP_PLAYER_MONEY).ToString();
        nextCapText.text = "Next: " + PlayerPrefs.GetInt(StringManager.CAP_PLAYER).ToString();
        valueMoneySpeed.text = PlayerPrefs.GetInt(StringManager.SPEED_PLAYER_MONEY).ToString();
        nextSpeedText.text = "Next: " + PlayerPrefs.GetFloat(StringManager.SPEED_PLAYER).ToString();

        if (StringManager.GetMoney() >= PlayerPrefs.GetInt(StringManager.SPEED_PLAYER_MONEY))
        {
            speedTextNorm.SetActive(true);
            speedTextAds.SetActive(false);
        }
        else
        {
            speedTextNorm.SetActive(false);
            speedTextAds.SetActive(true);
        }

        if (StringManager.GetMoney() >= PlayerPrefs.GetInt(StringManager.CAP_PLAYER_MONEY))
        {
            capTextNorm.SetActive(true);
            capTextAds.SetActive(false);
        }
        else
        {
            capTextNorm.SetActive(false);
            capTextAds.SetActive(true);
        }

        if (StringManager.GetMoney() >= PlayerPrefs.GetInt(StringManager.HEATH_PLAYER_MONEY))
        {
            heathTextNorm.SetActive(true);
            heathTextAds.SetActive(false);
        }
        else
        {
            heathTextNorm.SetActive(false);
            heathTextAds.SetActive(true);
        }
    }
}
