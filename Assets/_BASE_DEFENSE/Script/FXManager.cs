using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager: MonoBehaviour
{
    public static FXManager ins; 
    GameObject moneyfx;
    GameObject gemfx;
    [HideInInspector] public Transform moneyIcon;
    [HideInInspector] public Transform gemIcon;


    private void Awake()
    {
        ins = this;
        moneyfx = transform.Find("/Canvas/FX/MoneyClaimFx").gameObject;
        gemfx = transform.Find("/Canvas/FX/GemClaimFx").gameObject;
        moneyIcon = transform.Find("/Canvas/MoneyStats/MoneyIcon").transform;
        gemIcon = transform.Find("/Canvas/GemStats/GemIcon").transform;
    }
    public void ShowMoneyFX(int count)
    {
        moneyfx.GetComponent<MoveFX>().moneyCount = count;
        moneyfx.SetActive(true);
    }

    public void ShowGemFX(int count)
    {
        gemfx.GetComponent<MoveFX>().moneyCount = count;
        gemfx.SetActive(true);
    }
}
