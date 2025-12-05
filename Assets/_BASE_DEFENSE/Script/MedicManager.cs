using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class MedicManager : MonoBehaviour
{
    public static MedicManager ins;

    public TextMeshProUGUI amountSoldier;
    public Image loadingBar;
    public int currentSoldier;
    public bool isHeal;
    public GameObject[] soldierPrefabs;
    public string classSoldier;
    ObjectPooler objectPooler;


    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        objectPooler = ObjectPooler.instance;
    }

    public void AddSoldier(int value)
    {
        currentSoldier += value;
        amountSoldier.text = "X" + currentSoldier.ToString();

        if(!isHeal)
        LoadingBar();

    }


    void LoadingBar()
    {
        isHeal = true;
        loadingBar.fillAmount = 0;
        loadingBar.DOFillAmount(1, 10).OnComplete(IntSoldier);
    }

    void IntSoldier()
    {
        isHeal = false;
        ClassSoldierRandom();
        Debug.Log(classSoldier);

        if (objectPooler.poolDic[classSoldier].Count > 0)
        {
            Debug.Log("2");
            objectPooler.SpawnFormPool(classSoldier, transform.position, Quaternion.identity);
        }

       

        if (currentSoldier > 1)
            AddSoldier(-1);
        else
        {
            currentSoldier--;
            amountSoldier.text = "X" + currentSoldier.ToString();
        }
    }

    void ClassSoldierRandom()
    {
        int random = Random.Range(0, 100);
        if(random < 90)
            classSoldier = "Ally_Ak";
        else
        {
            int random2 = Random.Range(0, 100);
            if (random2 < 90)
                classSoldier = "Ally_Mgun";
            else
            {
                int random3 = Random.Range(0, 100);
                if (random3 < 90)
                    classSoldier = "Ally_Sniper";
                else
                {
                    classSoldier = "Ally_Rocket";
                }
            }
        }

    }

    
  

}
