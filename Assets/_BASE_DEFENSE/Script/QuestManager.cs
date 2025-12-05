using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class QuestType
{
    public string typeTitle;
    public GameObject questLine;
    public TextMeshProUGUI nameQuest;
    public TextMeshProUGUI processValue;
    public Slider sliderProcess;
    public Button buttonReward;
    public GameObject demoReward;
   
}



public class QuestManager : MonoBehaviour
{
    public QuestType[] questTyles;
    GameObject questPanel;
    Button closeBtn;
    GameObject wainingfx; 




    private void Awake()
    {
        wainingfx = transform.Find("QuestionMark").gameObject;
      
        questPanel = transform.Find("/Canvas/Quest").gameObject;
        closeBtn = transform.Find("/Canvas/Quest/BG/CloseBtn").GetComponent<Button>();
        questPanel.SetActive(false);

        for (byte i = 0; i < questTyles.Length; i++)
        {
            byte j = i;
            questTyles[j].buttonReward.onClick.AddListener(() => ClaimRewardBtn(j));
        }
        closeBtn.onClick.AddListener(() => Close());
    }


    void GetInfoQuestZombie()
    {
        switch (PlayerPrefs.GetInt(StringManager.QUEST_LEVEL_ZOMBIE))
        {
            case 0: InfoQuest("KILL 10 ZOMBIES", StringManager.GetQuestZombie() + "/10", 10, StringManager.GetQuestZombie(), 0); break;
            case 1: InfoQuest("KILL 50 ZOMBIES", StringManager.GetQuestZombie() + "/50", 50, StringManager.GetQuestZombie(),0); break;
            case 2: InfoQuest("KILL 100 ZOMBIES", StringManager.GetQuestZombie() + "/100", 100, StringManager.GetQuestZombie(),0); break;
            case 3: InfoQuest("KILL 200 ZOMBIES", StringManager.GetQuestZombie() + "/200", 200, StringManager.GetQuestZombie(),0); break;
            case 4: InfoQuest("KILL 300 ZOMBIES", StringManager.GetQuestZombie() + "/300", 300, StringManager.GetQuestZombie(), 0); break;
            case 5: InfoQuest("KILL 400 ZOMBIES", StringManager.GetQuestZombie() + "/400", 400, StringManager.GetQuestZombie(), 0); break;
            case 6: InfoQuest("KILL 500 ZOMBIES", StringManager.GetQuestZombie() + "/500", 500, StringManager.GetQuestZombie(),0); break;
            case 7: InfoQuest("KILL 600 ZOMBIES", StringManager.GetQuestZombie() + "/600", 600, StringManager.GetQuestZombie(), 0); break;
            case 8: InfoQuest("KILL 700 ZOMBIES", StringManager.GetQuestZombie() + "/700", 700, StringManager.GetQuestZombie(), 0); break;
            case 9: InfoQuest("KILL 800 ZOMBIES", StringManager.GetQuestZombie() + "/800", 800, StringManager.GetQuestZombie(), 0); break;
            case 10: InfoQuest("KILL 900 ZOMBIES", StringManager.GetQuestZombie() + "/900", 900, StringManager.GetQuestZombie(), 0); break;
            case 11: InfoQuest("KILL 1000 ZOMBIES", StringManager.GetQuestZombie() + "/1000", 1000, StringManager.GetQuestZombie(),0); break;
            case 12: questTyles[0].questLine.SetActive(false); break;
        }
    }

    void GetInfoQuestRecure()
    {
        switch (PlayerPrefs.GetInt(StringManager.QUEST_LEVEL_RESCUE))
        {
            case 0: InfoQuest("RESCUE 1 PERSON", StringManager.GetQuestRescue() + "/1", 1, StringManager.GetQuestRescue(),1); break;
            case 1: InfoQuest("RESCUE 10 PERSONS", StringManager.GetQuestRescue() + "/10", 10, StringManager.GetQuestRescue(),1); break;
            case 2: InfoQuest("RESCUE 30 PERSONS", StringManager.GetQuestRescue() + "/30", 30, StringManager.GetQuestRescue(), 1); break;
            case 3: InfoQuest("RESCUE 50 PERSONS", StringManager.GetQuestRescue() + "/50", 50, StringManager.GetQuestRescue(), 1); break;
            case 4: InfoQuest("RESCUE 100 PERSONS", StringManager.GetQuestRescue() + "/100", 100, StringManager.GetQuestRescue(),1); break;
            case 5: InfoQuest("RESCUE 150 PERSONS", StringManager.GetQuestRescue() + "/150", 150, StringManager.GetQuestRescue(),1); break;
            case 6: InfoQuest("RESCUE 200 PERSONS", StringManager.GetQuestRescue() + "/200", 200, StringManager.GetQuestRescue(), 1); break;
            case 7: InfoQuest("RESCUE 250 PERSONS", StringManager.GetQuestRescue() + "/250", 250, StringManager.GetQuestRescue(), 1); break;
            case 8: InfoQuest("RESCUE 300 PERSONS", StringManager.GetQuestRescue() + "/300", 300, StringManager.GetQuestRescue(), 1); break;
            case 9: InfoQuest("RESCUE 350 PERSONS", StringManager.GetQuestRescue() + "/350", 350, StringManager.GetQuestRescue(), 1); break;
            case 10: InfoQuest("RESCUE 400 PERSONS", StringManager.GetQuestRescue() + "/400", 400, StringManager.GetQuestRescue(), 1); break;
            case 11: InfoQuest("RESCUE 450 PERSONS", StringManager.GetQuestRescue() + "/450", 450, StringManager.GetQuestRescue(), 1); break;
            case 12: InfoQuest("RESCUE 500 PERSONS", StringManager.GetQuestRescue() + "/500", 500, StringManager.GetQuestRescue(), 1); break;
            case 13: InfoQuest("RESCUE 1000 PERSONS", StringManager.GetQuestRescue() + "/1000", 1000, StringManager.GetQuestRescue(), 1); break;
            case 14: questTyles[1].questLine.SetActive(false); break;
        }
    }

    void GetInfoQuestHire()
    {
        switch (PlayerPrefs.GetInt(StringManager.QUEST_LEVEL_HIRE))
        {
            case 0: InfoQuest("HIRE 1 WOKER", StringManager.GetQuestHire() + "/1", 1, StringManager.GetQuestHire(), 2); break;
            case 1: InfoQuest("HIRE 5 WOKERS", StringManager.GetQuestHire() + "/5", 5, StringManager.GetQuestHire(), 2); break;
            case 2: InfoQuest("HIRE 10 WOKERS", StringManager.GetQuestHire() + "/10", 10, StringManager.GetQuestHire(), 2); break;
            case 3: InfoQuest("HIRE 15 WOKERS", StringManager.GetQuestHire() + "/15", 15, StringManager.GetQuestHire(), 2); break;
            case 4: InfoQuest("HIRE 20 WOKERS", StringManager.GetQuestHire() + "/20", 20, StringManager.GetQuestHire(), 2); break;
            case 5: InfoQuest("HIRE 25 WOKERS", StringManager.GetQuestHire() + "/25", 25, StringManager.GetQuestHire(), 2); break;
            case 6: InfoQuest("HIRE 30 WOKERS", StringManager.GetQuestHire() + "/30", 30, StringManager.GetQuestHire(), 2); break;
            case 7: InfoQuest("HIRE 35 WOKERS", StringManager.GetQuestHire() + "/35", 35, StringManager.GetQuestHire(), 2); break;
            case 8: InfoQuest("HIRE 40 WOKERS", StringManager.GetQuestHire() + "/40", 40, StringManager.GetQuestHire(), 2); break;
            case 9: InfoQuest("HIRE 45 WOKERS", StringManager.GetQuestHire() + "/45", 45, StringManager.GetQuestHire(), 2); break;
            case 10: InfoQuest("HIRE 50 WOKERS", StringManager.GetQuestHire() + "/50", 50, StringManager.GetQuestHire(), 2); break;
            case 11: InfoQuest("HIRE 100 WOKERS", StringManager.GetQuestHire() + "/100", 100, StringManager.GetQuestHire(), 2); break;
            case 12: questTyles[2].questLine.SetActive(false); break;
           
        }
    }

    void GetInfoQuestBomb()
    {
        if (PlayerPrefs.GetInt(StringManager.QUEST_LEVEL_ACTIVEBOMB) < 1)
            InfoQuest("ACTIVE A BOMB", PlayerPrefs.GetInt(StringManager.QUEST_ACTIVEBOMB) + "/1", 1, PlayerPrefs.GetInt(StringManager.QUEST_ACTIVEBOMB), 3);
        else
            questTyles[3].questLine.SetActive(false);
    }

    void GetInfoQuestRedWall()
    {
        if (PlayerPrefs.GetInt(StringManager.QUEST_LEVEL_UNLOCKREDWALL) < 1)
            InfoQuest("UNLOCK RED WALL", PlayerPrefs.GetInt(StringManager.QUEST_UNLOCKREDWALL) + "/1", 1, PlayerPrefs.GetInt(StringManager.QUEST_UNLOCKREDWALL), 4);
        else
            questTyles[4].questLine.SetActive(false);
    }

    void GetInfoQuestBoss()
    {
        if (PlayerPrefs.GetInt(StringManager.QUEST_LEVEL_KILLBOSS) < 1)
            InfoQuest("KILL BOSS", PlayerPrefs.GetInt(StringManager.QUEST_KILLBOSS) + "/1", 1, PlayerPrefs.GetInt(StringManager.QUEST_KILLBOSS), 5);
        else
            questTyles[5].questLine.SetActive(false);
    }

    void InfoQuest(string nameQuest, string processValue, int maxSlider, int valueSlider, byte questType)
    {
        questTyles[questType].nameQuest.text = nameQuest;
        questTyles[questType].processValue.text = processValue;
        questTyles[questType].sliderProcess.maxValue = maxSlider;
      

        if(valueSlider >= maxSlider)
        {
            questTyles[questType].demoReward.SetActive(false);
            questTyles[questType].buttonReward.gameObject.SetActive(true);
            questTyles[questType].sliderProcess.value = maxSlider;
            wainingfx.SetActive(true);
            
        }
        else
        {
            questTyles[questType].demoReward.SetActive(true);
            questTyles[questType].buttonReward.gameObject.SetActive(false);
            questTyles[questType].sliderProcess.value = valueSlider;
            wainingfx.SetActive(false);

        }

    }


    void ClaimRewardBtn(byte valueBtn)
    {
        InstMoneyFX();
        wainingfx.SetActive(false);

        switch (valueBtn)
        {
            case 0:

                if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) == 7)
                    TutorialManager.ins.CheckStageTutorial();

                ClaimInfo(StringManager.QUEST_ZOMBIE, StringManager.QUEST_LEVEL_ZOMBIE);
                GetInfoQuestZombie();
                break;
            case 1:

                if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) == 13)
                    TutorialManager.ins.CheckStageTutorial();


                ClaimInfo(StringManager.QUEST_RESCUE, StringManager.QUEST_LEVEL_RESCUE);
                GetInfoQuestRecure();
                break;
            case 2:
                ClaimInfo(StringManager.QUEST_HIRE, StringManager.QUEST_LEVEL_HIRE);
                GetInfoQuestHire();
                break;
            case 3:
                ClaimInfo(StringManager.QUEST_ACTIVEBOMB, StringManager.QUEST_LEVEL_ACTIVEBOMB);
                GetInfoQuestBomb();
                break;
            case 4:
                ClaimInfo(StringManager.QUEST_UNLOCKREDWALL, StringManager.QUEST_LEVEL_UNLOCKREDWALL);
                GetInfoQuestRedWall();
                break;
            case 5:
                ClaimInfo(StringManager.QUEST_KILLBOSS, StringManager.QUEST_LEVEL_KILLBOSS);
                GetInfoQuestBoss();
                break;
           
        }
       
        
        
    }

    void ClaimInfo(string valueReset, string levelUp)
    {
        SoundManager.ins.PlaySound(1);
        StringManager.AddMoney(250);
        GameManager.intance.UP_StatsCanvas();
        PlayerPrefs.SetInt(valueReset, 0);
        PlayerPrefs.SetInt(levelUp, PlayerPrefs.GetInt(levelUp) + 1);
        //AdsManager.intance.ShowInterstitial();

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SoundManager.ins.PlaySound(0);
            questPanel.SetActive(true);
           
        }

        if (other.gameObject.tag == "RangeCheck")
        {
           
            GetInfoQuestZombie();
            GetInfoQuestRecure();
            GetInfoQuestHire();
            GetInfoQuestBomb();
            GetInfoQuestRedWall();
            GetInfoQuestBoss();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            questPanel.SetActive(false);
        }

       
    }

    void Close()
    {
        SoundManager.ins.PlaySound(0);
        questPanel.SetActive(false);
        //AdsManager.intance.ShowInterstitial();

    }

    void InstMoneyFX()
    {
        FXManager.ins.ShowMoneyFX(250);
    }
}
