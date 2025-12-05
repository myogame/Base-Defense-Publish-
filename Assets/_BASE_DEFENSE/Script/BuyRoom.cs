using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Room
{
    Left,
    Right
}

public class BuyRoom : MonoBehaviour
{
    public GameObject showObject;
    
    Image fillImage;
    public int money;
    public Room roomType;
    public int idRewardAds;
    bool buyStop;

    void Awake()
    {
        fillImage = transform.Find("Canvas/fill").GetComponent<Image>();
    }


    private void Start()
    {
        switch (roomType)
        {
            case Room.Left:
                if (PlayerPrefs.GetInt(StringManager.ACTIVE_ROOM + "LEFT") == 1)
                    ActiveRoom();
                break;
            case Room.Right:
                if (PlayerPrefs.GetInt(StringManager.ACTIVE_ROOM + "RIGHT") == 1)
                    ActiveRoom();
                break;
        }

        
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
            buyStop = false;
        }
    }

    void HireAlly()
    {
        if (StringManager.GetMoney() >= money)
        {

            GameManager.intance.moneyText.DOCounter(StringManager.GetMoney(), StringManager.GetMoney() - money, 1);
            StringManager.AddMoney(-money);
            GetReward();
        }
        else
        {
            if (!buyStop)
            {
                buyStop = true;
            }
           
        }

    }

    void GetReward()
    {
        SoundManager.ins.PlaySound(1);
       

        switch (roomType)
        {
            case Room.Left: PlayerPrefs.SetInt(StringManager.ACTIVE_ROOM + "LEFT", 1);  break;
            case Room.Right: PlayerPrefs.SetInt(StringManager.ACTIVE_ROOM + "RIGHT", 1); break;
        }

        
        ActiveRoom();
    }

    private void Update()
    {
        //if (AdsManager.intance.checkRewardComplete)
        //{

        //    if (idRewardAds == AdsManager.intance.rewardNumber)
        //    {
        //        AdsManager.intance.checkRewardComplete = false;
        //        Debug.Log("test");
        //        GetReward();
        //    }

        
        //}
    }

    void ActiveRoom()
    {
        GameManager.intance.actionPoint += 0.1f;
        showObject.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
