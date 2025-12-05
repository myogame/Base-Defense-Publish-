using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedLine : MonoBehaviour
{
    Image fillImage;
    public int money;
    public int ID;

    void Awake()
    {
        fillImage = transform.Find("Canvas/fill").GetComponent<Image>();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt(StringManager.RED_WALL + ID) == 1)
            DeActiveRedWall();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fillImage.fillAmount += Time.deltaTime * 0.7f;

            if (fillImage.fillAmount >= 1)
            {
                HireAlly();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fillImage.fillAmount = 0;
        }
    }

    void HireAlly()
    {
        if (StringManager.GetGem() >= money)
        {

            GameManager.intance.gemText.DOCounter(StringManager.GetGem(), StringManager.GetGem() - money, 1);
            StringManager.AddGem(-money);
            GetReward();
            //AdsManager.intance.ShowInterstitial();

            DeActiveRedWall();

        }
        else
        {
            NofityManager.ins.Nofity("Not Enough Gem");
        }
    

    }

    void GetReward()
    {
        SoundManager.ins.PlaySound(1);
       
        StringManager.AddQuestRedWall(1);
        PlayerPrefs.SetInt(StringManager.RED_WALL + ID, 1);
    }

    

    void DeActiveRedWall()
    {
        GameManager.intance.actionPoint += 0.1f;
        transform.parent.gameObject.SetActive(false);
    }
}
