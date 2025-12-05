using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FxType
{
    Money,
    Gem
}

public class MoveFX : MonoBehaviour
{
    public GameObject[] moneys;
    Vector2[] oldPos;
    public FxType fxType;
    Transform posIcon;
    [HideInInspector] public int moneyCount;

    private void Awake()
    {
        SetIconTransForm();
        SetOriginPos();
    }

    void SetIconTransForm()
    {
        switch (fxType)
        {
            case FxType.Money:
                posIcon = FXManager.ins.moneyIcon;
                break;
            case FxType.Gem:
                posIcon = FXManager.ins.gemIcon;
                break;
        }
    }

    void SetOriginPos()
    {
        oldPos = new Vector2[moneys.Length];
        for (int i = 0; i < moneys.Length; i++)
            oldPos[i] = new Vector2(moneys[i].transform.position.x, moneys[i].transform.position.y);
    }

    void ResetPos()
    {
        if (moneyCount >= moneys.Length)
            moneyCount = moneys.Length;

        transform.localScale = Vector3.one;
        for (int i = 0; i < moneyCount; i++)
        {
            moneys[i].transform.position = oldPos[i];
            moneys[i].SetActive(true);
        }
            
    }

    private void OnEnable()
    {
        ResetPos();
        SoundManager.ins.PlaySound(3);
        transform.DOScale(2, 0.2f).OnComplete(() => {
            StartCoroutine(MoveCoins());
        });
    }


    IEnumerator MoveCoins()
    {
        foreach(GameObject money in moneys)
        {
            money.transform.DOMove(posIcon.position, 0.5f).OnComplete(()=> {
                money.SetActive(false);
            });
            yield return new WaitForSeconds(0.1f);
           
        }
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);

       
    }
}
