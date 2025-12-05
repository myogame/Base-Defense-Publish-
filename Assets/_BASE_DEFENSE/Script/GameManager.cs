using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager intance;

    [Header("Canvas")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI gemText;

    [Header("Miner")]
    public Transform spawnMiner;
    public GameObject minerPrefabs;
    public Transform cartMine;
    public Transform ivenMine;
    public Transform[] rockMine;

    [Header("Ally_Turret")]
    public Transform bullet_desk;

    [Header("Ally_Money")]
    public Transform gate_Money;

    [Header("Prefabs")]
    public GameObject moneyInV;
   

    [Header("Ally_Gun")]
    public Transform startMovePos;

    [Header("GamePlay")]
    public float actionPoint;
    public bool isBossDead;

    [Header("Military")]
    public GameObject doorHospital;

    [Header("Boss")]
    public GameObject wallFinish;


    public GameObject[] mineRescue;

    public List<GameObject> allyAmor;
    public List<GameObject> allyMoney;

    private void Awake()
    {
        intance = this;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

    }

    private void Start()
    {
        UP_StatsCanvas();


        //test intance Miner
        for(int i = 0; i < PlayerPrefs.GetInt(StringManager.MINE_WORKER); i++)
        {
            GameObject miner = Instantiate(minerPrefabs, spawnMiner.position, Quaternion.identity);
            miner.GetComponent<Mine_Ally_Controller>().Work();
            mineRescue[i].SetActive(false);
        }
    }

    public void UP_StatsCanvas()
    {
        moneyText.text = StringManager.GetMoney().ToString();
        gemText.text = StringManager.GetGem().ToString();

       
    }
}
