using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public Button add1kMoney;
    public Button add1kGem;
    public Button add1Miner;
    public Button superShot;
    public Button Immortal;
    public Button HideAllUI;
    public Button AddM16;
    public Button AddMachine;
    public Button AddSniper;
    public Button AddRocket;

    public GameObject[] allUI;

    public GameObject[] soldier;

    private void Awake()
    {
        add1kMoney.onClick.AddListener(() => Add1KMoney());
        add1kGem.onClick.AddListener(() => Add1KGem());
        add1Miner.onClick.AddListener(() => AddMinner());
        superShot.onClick.AddListener(() => SuperShot());
        Immortal.onClick.AddListener(() => GetImmortal());
        HideAllUI.onClick.AddListener(() => GetHideAllUI());
        AddM16.onClick.AddListener(() => Add1SolM16());
        AddMachine.onClick.AddListener(() => Add1SolMachine());
        AddSniper.onClick.AddListener(() => Add1SolSniper());
        AddRocket.onClick.AddListener(() => Add1SolRocket());

    }


    void Add1KMoney()
    {
        StringManager.AddMoney(1000);
        GameManager.intance.UP_StatsCanvas();
    }

    void Add1KGem()
    {

        StringManager.AddGem(1000);
        GameManager.intance.UP_StatsCanvas();
    }

    void AddMinner()
    {
        GameObject miner = Instantiate(GameManager.intance.minerPrefabs, PlayerControler.instance.transform.position, Quaternion.identity);
        miner.GetComponent<Mine_Ally_Controller>().Work();

    }

    void SuperShot()
    {
        PlayerControler.instance.gunControler.timeDelay = 0.1f;
    }

    void GetImmortal()
    {
        PlayerControler.instance.hearth_Player = 999999999;
    }

    void GetHideAllUI()
    {
        foreach(GameObject ui in allUI)
        {
            ui.SetActive(false);
        }
    }

    void Add1SolM16()
    {
        GameObject miner = Instantiate(soldier[0], PlayerControler.instance.transform.position, Quaternion.identity);

    }

    void Add1SolMachine()
    {
        GameObject miner = Instantiate(soldier[1], PlayerControler.instance.transform.position, Quaternion.identity);

    }

    void Add1SolSniper()
    {
        GameObject miner = Instantiate(soldier[2], PlayerControler.instance.transform.position, Quaternion.identity);

    }

    void Add1SolRocket()
    {
        GameObject miner = Instantiate(soldier[3], PlayerControler.instance.transform.position, Quaternion.identity);

    }
}
