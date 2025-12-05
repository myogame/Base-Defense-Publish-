using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DemoQuest
{
    public GameObject questLine;
    public TextMeshProUGUI processValue;
    public Slider sliderProcess;
    public GameObject completeOb;
}


public class TutorialManager : MonoBehaviour
{
    public static TutorialManager ins;

    
    public GameObject[] arrows;
    [HideInInspector] public Transform currentTarget;
    GameObject arrowPlayer;
    public DemoQuest demoQuest;
    bool isCompleteTutorial;

    //CharacterMessesge
    [Header("CharacterMess")]
    public GameObject tutorialMess;
    public Button next;
    public GameObject[] characterPic;
    public TextMeshProUGUI textMess;
    [SerializeField] private int messStep;





    //-----//
    [Header("PositonArrow")]
    public Transform bulletDesk;
    public Transform bulletTurretZone;
    public Transform turretGun;
    public GameObject turretTut_1;
    public GameObject turretTut_2;
    public Transform questNPC;
    public Transform turretHireZone;
    public Transform rescuePerson;
    public Transform gateBase;
    public Transform hireWorker;
    public Transform gemZone;
    


    private void Awake()
    {
        ins = this;

        arrowPlayer = transform.Find("/Player/Arrow").gameObject;
        next.onClick.AddListener(() => CloseMess());
    }


    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) <= 5)
        {
            PlayerPrefs.SetInt(StringManager.TUT_STAGE, 0);
            questNPC.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) > 5 & PlayerPrefs.GetInt(StringManager.TUT_STAGE) < 16)
            PlayerPrefs.SetInt(StringManager.TUT_STAGE, PlayerPrefs.GetInt(StringManager.TUT_STAGE) - 1);
        else
            isCompleteTutorial = true;

        if (!isCompleteTutorial)
            CheckStageTutorial();
        
    }

    private void Update()
    {
        if (!isCompleteTutorial)
        {
            if (currentTarget != null)
            {
                arrowPlayer.transform.LookAt(currentTarget);

            }

            if (demoQuest.questLine.activeSelf && PlayerPrefs.GetInt(StringManager.TUT_STAGE) < 6)
                CheckProcessDemoQuest();
        }
        else
            arrowPlayer.SetActive(false);




    }

    void CheckProcessDemoQuest()
    {
        demoQuest.processValue.text = StringManager.GetQuestZombie().ToString() + "/10";
        demoQuest.sliderProcess.value = StringManager.GetQuestZombie();

        if (StringManager.GetQuestZombie() >= demoQuest.sliderProcess.maxValue)
        {
            demoQuest.completeOb.SetActive(true);
            NofityManager.ins.Nofity("Quest Complete!");
            PlayerPrefs.SetInt(StringManager.TUT_STAGE, 6);
            CheckStageTutorial();
        }
            
    }

    public void CheckStageTutorial()
    {
        arrowPlayer.SetActive(true);
    

        switch (PlayerPrefs.GetInt(StringManager.TUT_STAGE))
        {
            case 0:
                StartCoroutine(ShowMess( 0, "Welcome to Defense Zombie!\nGet ready for a fierce battle.", 0,1));
                
                arrows[0].SetActive(true);
                currentTarget = bulletDesk;
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 1);
                break;
            case 1:

                if(messStep == 1)
                    StartCoroutine(ShowMess( 1, "Very good! Now put it in the right place and use the turret to kill the zombies.", 2,1));

                arrows[0].SetActive(false);
                arrows[1].SetActive(true);
                currentTarget = bulletTurretZone;
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 2);
                break;
            case 2:
                arrows[1].SetActive(false);
                arrows[2].SetActive(true);
                currentTarget = turretGun;
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 3);
                break;
            case 3:
                //EnterTurret
                arrows[2].SetActive(false);
                turretTut_1.SetActive(true);
                arrowPlayer.SetActive(false);
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 4);
                demoQuest.questLine.SetActive(true);
                break;
            case 4:
                //bullet Out
                turretTut_1.SetActive(false);
                turretTut_2.SetActive(true);
                arrowPlayer.SetActive(false);
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 5);
                break;
            case 5:
                turretTut_2.SetActive(false);
           
                arrows[0].SetActive(true);
                currentTarget = bulletDesk;
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 1);
                break;
            case 6:
                StartCoroutine(ShowMess( 2, "Congratulations! You’ve successfully completed the first mission.", 3,3));
                //goi khi hoan thanh quest
                questNPC.gameObject.SetActive(true);
                turretTut_1.SetActive(false);
                turretTut_2.SetActive(true);
                arrows[0].SetActive(false);
                arrows[3].SetActive(true);// nhiem vu
                currentTarget = questNPC; // nhiem vu
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 7);
                break;
            case 7:
                //unlock Turret Char
                StartCoroutine(ShowMess( 3, "You are doing great! Let's try to unlock a support soldier.\nFollow the arrow.", 4,3));

                demoQuest.questLine.SetActive(false);
                arrows[3].SetActive(false);
                arrows[4].SetActive(true);// turret char
                currentTarget = turretHireZone; // nhiem vu
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 8);
                break;
            case 8:
                //qua bullet desk
                StartCoroutine(ShowMess(1, "Reload the weapon!", 5,1));
                arrows[4].SetActive(false);
                arrows[0].SetActive(true);// turret char
                currentTarget = bulletDesk; // nhiem vu
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 9);
                break;
            case 9:
                //move bulletzone
                arrows[0].SetActive(false);
                arrows[1].SetActive(true);
                currentTarget = bulletTurretZone;
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 10);
                break;
            case 10:
                //Rescue 1 mine
                //Move to Minner
                StartCoroutine(ShowMess(2, "Wonderful! I know you can do it. Now, get out of the base and rescue a Miner!", 6,1));
                rescuePerson.gameObject.SetActive(true);
                arrows[1].SetActive(false);
                arrows[5].SetActive(true);
                currentTarget = rescuePerson;
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 11);
                break;
            case 11:

                NofityManager.ins.Nofity("Back to Base!");
                arrows[5].SetActive(false);
                arrows[6].SetActive(true);
                currentTarget = gateBase;
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 12);
              
                break;
            case 12:
                //den nhiem vu
                StartCoroutine(ShowMess(3, "You are doing well! Go to the quest area to get the reward.", 7,1));

                arrows[6].SetActive(false);
                arrows[3].SetActive(true);
                currentTarget = questNPC;
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 13);
                break;
            case 13:
                //Nhan thuong
                StartCoroutine(ShowMess(3, "Next, unlock an Ammor worker.", 8,3));

                arrows[3].SetActive(false);
                arrows[7].SetActive(true);
                currentTarget = hireWorker;
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 14);
                break;
            case 14:
                //Unlock 1 woker
                StartCoroutine(ShowMess(1, "Ammor Worker will be in charge of reloading the turrets. And Money Worker will help you to pick up money.", 9,1));

                arrows[7].SetActive(false);
                arrows[8].SetActive(true);
                currentTarget = gemZone;
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 15);
                break;
            case 15:
                //Di chuyen ve gem

                arrows[8].SetActive(false);
               
                currentTarget = null;
                arrowPlayer.SetActive(false);
                PlayerPrefs.SetInt(StringManager.TUT_STAGE, 16);
                goto case 16;
            case 16:
                //Total Complete
                StartCoroutine(ShowMess(2, "Congratulations! You have completed the entire tutorial.", 11,1));
                isCompleteTutorial = true;
                //AdsManager.intance.ShowBanner();
                break;


        }

        
    }


    public void UnlockAllyBefore()
    {
        PlayerPrefs.SetInt(StringManager.TUT_STAGE, 4);
        demoQuest.questLine.SetActive(true);
    }

    void CloseMess()
    {
        tutorialMess.SetActive(false);

        switch (messStep)
        {
            case 0: StartCoroutine(ShowMess( 0, "Firstly move towards the ammo box.", 1,0.1f)); break;
            case 3: StartCoroutine(ShowMess( 0, "Move towards the arrow to get the reward.", 4,0.1f)); break;
            case 9: StartCoroutine(ShowMess(1, "Now, go to Gem Mine to collect gems from Miner", 10,0.1f)); break;
            case 11: StartCoroutine(ShowMess(1, "Now, you can explore the world on your own.", 12,0.1f)); break;
            case 12: StartCoroutine(ShowMess(1, "Let's start building a wide base!", 14,0.1f)); break;




        }

    }

    IEnumerator ShowMess(int charPic, string content, int step, float time)
    {
        yield return new WaitForSeconds(time);
        messStep = step;
        tutorialMess.SetActive(true);
        foreach (GameObject character in characterPic)
            character.SetActive(false);

        characterPic[charPic].SetActive(true);
        textMess.text = content;
    }
    
}
