using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class PlayerControler : MonoBehaviour
{
    public static PlayerControler instance;

    [Header("PlayerInfo")]
    public int hearth_Player;
    public float speed;

    [Header("PlayerLink")]
    public GameObject gun;
    public GameObject guitar;
    GameObject inventory;
    GameObject inventory_money;
    public bool isTrailer = true;
    public List<GameObject> bullet_list;
    [HideInInspector] public List<GameObject> money_list;
    [HideInInspector] public GameObject maxOb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool enter_Torren;
    [HideInInspector] public bool enter_Base = true;
    [HideInInspector] public bool bullet_Carry;
    [HideInInspector] public int moneyCarry;
    [HideInInspector] public GunControler gunControler;
    GameObject health;
    Slider healthbar;
    DynamicJoystick leftJoyStick;
    Rigidbody rg;
    float cameraAngleY;
    GameManager gameManager;
    int startLives;
    bool healthSpawn;
    float delayTime;
    [HideInInspector] public bool dead;

    //GameObject reSpawnPop;
    


    private void Awake()
    {
        instance = this;
        rg = GetComponent<Rigidbody>();
        leftJoyStick = transform.Find("/Canvas/Dynamic Joystick").GetComponent<DynamicJoystick>();
        //reSpawnPop = transform.Find("/Canvas/ReSpawn").gameObject;
        //reSpawnPop.SetActive(false);
        inventory = transform.Find("Inventory").gameObject;
        inventory_money = transform.Find("InventoryMoney").gameObject;
        maxOb = transform.Find("InventoryMoney/Max").gameObject;
        animator = GetComponent<Animator>();
        gunControler = GetComponent<GunControler>();
        health = transform.Find("Health").gameObject;
        healthbar = transform.Find("Health/Healthbar").GetComponent<Slider>();



    }

   
    void Start()
    {
        gameManager = GameManager.intance;
        SetMoneyCarryMax();
        UpgradeSpeed();
        UpgradeHeath();

        if(isTrailer)
        Trailer();


    }

    void Trailer()
    {
        animator.SetBool("Walk", true);
        transform.DOMoveZ(2, 6f).SetEase(Ease.Flash).OnComplete(() => { isTrailer = false; animator.SetBool("Walk", false); });
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!isTrailer && !dead)
        {
            if (!enter_Torren)
            {

                MoverControler();
                GetOutTurret();

                if (target != null)
                {
                    if (target.gameObject.activeSelf)
                    {
                        animator.SetBool("Shoot", true);
                        gunControler.shootStart = true;

                        if (rg.linearVelocity.magnitude > 0.01f) animator.SetBool("ShootIdle", false);
                        else animator.SetBool("ShootIdle", true);
                        LookAtTarget();
                    }
                    else
                    {
                        target = null;
                    }

                }
                else
                {
                    animator.SetBool("Shoot", false);
                    gunControler.shootStart = false;

                    if (rg.linearVelocity.magnitude > 0.01f) animator.SetBool("Walk", true);
                    else animator.SetBool("Walk", false);
                }

            }

            HeathManager();
        }

       


    }

    void GetOutTurret()
    {
        if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) == 5)
            TutorialManager.ins.CheckStageTutorial();

        if(PlayerPrefs.GetInt(StringManager.TUT_STAGE) < 16)
        {
            if (TutorialManager.ins.turretTut_1.activeSelf || TutorialManager.ins.turretTut_2.activeSelf)
            {
                TutorialManager.ins.turretTut_1.SetActive(false);
                TutorialManager.ins.turretTut_2.SetActive(false);
            }
        }
      
    }

    void MoverControler()
    {
        var input = new Vector3(leftJoyStick.input.x, 0, leftJoyStick.input.y);
        var vel = Quaternion.AngleAxis(cameraAngleY + 180, Vector3.up) * input * speed;
        rg.linearVelocity = new Vector3(vel.x, rg.linearVelocity.y, vel.z);
    
        if (leftJoyStick.input.x != 0 || leftJoyStick.input.y != 0) transform.rotation = Quaternion.LookRotation(rg.linearVelocity);
    }

    void HeathManager()
    {
        if (hearth_Player < startLives && !healthSpawn)
        {
            if (!health.activeSelf)
                health.SetActive(true);

            health.transform.LookAt(2 * transform.position - Camera.main.transform.position);
            healthbar.value = hearth_Player;
        }
        else if (hearth_Player < startLives && healthSpawn)
        {
            if (delayTime >= 0.1f)
            {
                delayTime = 0;
                hearth_Player += 2;
                if (!health.activeSelf)
                    health.SetActive(true);
                health.transform.LookAt(2 * transform.position - Camera.main.transform.position);
                healthbar.value = hearth_Player;
            }
            else
            {
                delayTime += Time.deltaTime;
            }
        }
        else health.SetActive(false);

        if (hearth_Player < 1 && !dead)
        {
            
            hearth_Player = 0;
            StartCoroutine(die());
        }
    }
    public IEnumerator die()
    {
        SoundManager.ins.PlaySound(6);
        BeforeDie();
        yield return new WaitForSeconds(3);
        //AdsManager.intance.ShowInterstitial();
        ResetPlayer();
    }

    void ResetPlayer()
    {
        //reSpawnPop.SetActive(false);
        dead = false;
        animator.SetBool("Dead", false);
        rg.isKinematic = false;
        hearth_Player = startLives;
        transform.position = new Vector3(0, -0.3f, 0);
        SetGoBase();

    }

    void BeforeDie()
    {
        //reSpawnPop.SetActive(true);
        dead = true;
        animator.SetBool("Dead", true);
        enter_Base = true;
        RemoveAllMoney();
        health.SetActive(false);
        rg.isKinematic = true;
        maxOb.SetActive(false);
    }

    void RemoveAllMoney()
    {
       
       
        if (moneyCarry > 0)
        {
            foreach (GameObject money in money_list)
            {
                money.SetActive(false);
            }
        }
        moneyCarry = 0;
    }

    public void UpgradeSpeed()
    {
        speed = PlayerPrefs.GetFloat(StringManager.SPEED_PLAYER);
    }
    public void UpgradeHeath()
    {
        hearth_Player = PlayerPrefs.GetInt(StringManager.HEATH_PLAYER);
        health.SetActive(false);
        healthbar.maxValue = PlayerPrefs.GetInt(StringManager.HEATH_PLAYER);
        startLives = PlayerPrefs.GetInt(StringManager.HEATH_PLAYER);
    }

    public void UpgradeCap()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject money = Instantiate(gameManager.moneyInV);
            money.transform.parent = inventory_money.transform;
            money.transform.rotation = inventory_money.transform.rotation;

            money.SetActive(false);
            money_list.Add(money);
        }
        maxOb.transform.SetAsLastSibling();
    }


    void SetMoneyCarryMax()
    {
        for (int i = 0; i < PlayerPrefs.GetInt(StringManager.CAP_PLAYER); i++)
        {
           GameObject money = Instantiate(gameManager.moneyInV);
            money.transform.parent = inventory_money.transform;
            money.transform.rotation = inventory_money.transform.rotation;
            money.transform.localPosition = new Vector3(0, 0, 0);
            money.transform.localScale = Vector3.one;


            money.SetActive(false);
            money_list.Add(money);
        }
        maxOb.transform.SetAsLastSibling();
    }

 

    void LookAtTarget()
    {
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EnterBase")
        {


            SetGoBase();


            GameManager.intance.moneyText.DOCounter(StringManager.GetMoney(), StringManager.GetMoney() + (moneyCarry), 1);
            StringManager.AddMoney(moneyCarry);


            StartCoroutine(ImportMoneyToBase());
            setMoneyInBase();

            
        }


        if (other.gameObject.tag == "Gate")
        {
            CameraFollow.instance.offset = new Vector3(0, 20, 12);
            healthSpawn = false;
            gun.SetActive(true);
            enter_Base = false;
        }
    }

    public void SetGoBase()
    {
        CameraFollow.instance.offset = new Vector3(0, 12, 7);
        enter_Base = true;
        gun.SetActive(false);
        target = null;
        healthSpawn = true;
        animator.SetBool("Shoot", false);
    }

    IEnumerator ImportMoneyToBase()
    {
        if (bullet_Carry)
        {
            foreach (GameObject bullet in bullet_list)
            {
                bullet.SetActive(false);
                yield return new WaitForSeconds(0.1f);
            }

            bullet_Carry = false;
        }


    }

    void setMoneyInBase()
    {
        if (moneyCarry > 0)
        {
            maxOb.SetActive(false);
            FXManager.ins.ShowMoneyFX(moneyCarry);
            foreach(GameObject money in money_list)
            {
                if (money.activeSelf)
                {
                    money.SetActive(false);
                }
            }

           
            moneyCarry = 0;

            //AdsManager.intance.ShowInterstitial();
        }
    }

    public Transform findCurrentTarget()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform target = null;

        float closestDistance = Mathf.Infinity;

        foreach (GameObject potentialTarget in enemies)
        {
            if (Vector3.Distance(transform.position, potentialTarget.transform.position) < closestDistance && potentialTarget != null)
            {
                closestDistance = Vector3.Distance(transform.position, potentialTarget.transform.position);
                target = potentialTarget.transform;
            }
        }

        if (target)
            return target;


        return null;
    }

    public void PlayGuitar()
    {

        guitar.SetActive(true);
        animator.SetBool("Walk", true);
        animator.SetBool("PlayMusic", true);

    }

    public void StopGuitar()
    {
        guitar.SetActive(false);
        animator.SetBool("Walk", true);
        animator.SetBool("PlayMusic", false);


    }
}


