using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMilitary : MonoBehaviour
{
    public static UpgradeMilitary ins;

    public GameObject military_Upgrade_Popup;
    public Button military_Popup_Close;
    public Button military_Upgrade_Btn;
    public TextMeshProUGUI military_Level;
    public TextMeshProUGUI priceText;
    int priceRank = 250;


    private void Awake()
    {
        ins = this;
        military_Popup_Close.onClick.AddListener(() => Close());
        military_Upgrade_Btn.onClick.AddListener(() => UpgradeRank());
    }

    private void Start()
    {
        UpdateLevelRankInfo();
    }

    void Close()
    {
        military_Upgrade_Popup.SetActive(false);
    }

    void UpdateLevelRankInfo()
    {
        military_Level.text = "Level: " + PlayerPrefs.GetInt(StringManager.RANK_LEVEL).ToString();
        priceText.text = (priceRank * (PlayerPrefs.GetInt(StringManager.RANK_LEVEL) + 1)).ToString();
    }

    void UpgradeRank()
    {
        if(StringManager.GetMoney() >= (priceRank* (PlayerPrefs.GetInt(StringManager.RANK_LEVEL)+1)))
        {
            StringManager.AddMoney(-(priceRank * (PlayerPrefs.GetInt(StringManager.RANK_LEVEL) + 1)));
            PlayerPrefs.SetInt(StringManager.RANK_LEVEL, PlayerPrefs.GetInt(StringManager.RANK_LEVEL) + 1);
            UpdateLevelRankInfo();
            GameManager.intance.UP_StatsCanvas();
        }
        else
        {
            //ads
        }
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            military_Upgrade_Popup.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            military_Upgrade_Popup.SetActive(false);
        }
    }
}
