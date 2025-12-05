using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAdsReady : MonoBehaviour
{
    GameObject moneyPrice;
    GameObject adsPrice;
    public int price;

    public void Awake()
    {
        adsPrice = transform.Find("Canvas/Price/Image Ads").gameObject;
        moneyPrice = transform.Find("Canvas/Price/Image Money").gameObject;
    }

    void CheckAdsOrMoney()
    {
        if (StringManager.GetMoney() >= price)
        {
            moneyPrice.SetActive(true);
            adsPrice.SetActive(false);
        }
        else
        {
            moneyPrice.SetActive(false);
            adsPrice.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "RangeCheck")
        {
            CheckAdsOrMoney();
        }
    }
}
