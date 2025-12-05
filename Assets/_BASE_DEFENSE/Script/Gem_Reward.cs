using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gem_Reward : MonoBehaviour
{

    Image fill;
    public int price;
    public int idRewardAds;
    bool buyStop;

    void Awake()
    {
        fill = transform.Find("Canvas/fill").GetComponent<Image>();
    }
    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fill.fillAmount += Time.deltaTime * 0.7f;

            if (fill.fillAmount >= 1 && !buyStop)
            {
                buyStop = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fill.fillAmount = 0;
           
        }
    }

    private void Update()
    {
        //if (AdsManager.intance.checkRewardComplete)
        //{

        //    if (idRewardAds == AdsManager.intance.rewardNumber)
        //    {
        //        AdsManager.intance.checkRewardComplete = false;
        //        ActicveBomb();
                
        //    }

        //}
    }

    void ActicveBomb()
    {
        SoundManager.ins.PlaySound(1);
        GameManager.intance.gemText.DOCounter(StringManager.GetGem(), StringManager.GetGem() + price, 1);
        StringManager.AddGem(price);
        transform.parent.gameObject.SetActive(false);
    }


}
