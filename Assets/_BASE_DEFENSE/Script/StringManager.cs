using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringManager : MonoBehaviour
{
    static string MONEY = "MONEY";
    static string GEM = "GEM";
    public static string SPEED_AMMO = "SPEED_AMMO";
    public static string SPEED_MONEY = "SPEED_MONEY";
    public static string SPEED_PLAYER = "SPEED_PLAYER";
    public static string SPEED_PLAYER_MONEY = "SPEED_PLAYER_MONEY";
    public static string CAP_PLAYER = "CAP_PLAYER";
    public static string CAP_PLAYER_MONEY = "CAP_PLAYER_MONEY";
    public static string HEATH_PLAYER = "HEATH_PLAYER";
    public static string HEATH_PLAYER_MONEY = "HEATH_PLAYER_MONEY";
    public static string RANK_LEVEL = "RANK_LEVEL";
    public static string ACTIVE_AUTO_TORRET = "ACTIVE_AUTO_TORRET";
    public static string ACTIVE_ROOM = "ACTIVE_ROOM";
    public static string MONEY_WORKER = "MONEY_WORKER";
    public static string BULLET_WORKER = "BULLET_WORKER";
    public static string MINE_WORKER = "MINE_WORKER";
    public static string RED_WALL = "RED_WALL";
    public static string LEVEL_CURRENT = "LEVEL_CURRENT";

    public static string QUEST_LEVEL_ZOMBIE = "QUEST_LEVEL_ZOMBIE";
    public static string QUEST_LEVEL_RESCUE = "QUEST_LEVEL_RESCUE";
    public static string QUEST_LEVEL_HIRE = "QUEST_LEVEL_HIRE";
    public static string QUEST_LEVEL_ACTIVEBOMB = "QUEST_LEVEL_ACTIVEBOMB";
    public static string QUEST_LEVEL_UNLOCKREDWALL = "QUEST_LEVEL_UNLOCKREDWALL";
    public static string QUEST_LEVEL_KILLBOSS = "QUEST_LEVEL_KILLBOSS";

    public static string QUEST_ZOMBIE = "QUEST_ZOMBIE";
    public static string QUEST_RESCUE = "QUEST_RESCUE";
    public static string QUEST_HIRE = "QUEST_HIRE";
    public static string QUEST_ACTIVEBOMB = "QUEST_ACTIVEBOMB";
    public static string QUEST_UNLOCKREDWALL = "QUEST_UNLOCKREDWALL";
    public static string QUEST_KILLBOSS = "QUEST_KILLBOSS";

    public static string TUT_STAGE = "TUT_STAGE";

    public static string SOUND = "SOUND";
    public static string VIBRATE = "VIBRATE";
    public static string REMOVEADS = "REMOVEADS";



    private void Awake()
    {
        if(PlayerPrefs.GetInt("NEWGAME") == 0) 
        {
            PlayerPrefs.SetInt("NEWGAME", 1);
            PlayerPrefs.SetInt(MONEY, 0);
            PlayerPrefs.SetInt(GEM, 0);
            PlayerPrefs.SetFloat(SPEED_AMMO, 1.5f);
            PlayerPrefs.SetFloat(SPEED_MONEY, 1.5f);
            PlayerPrefs.SetInt(SPEED_PLAYER_MONEY, 250);
            PlayerPrefs.SetInt(CAP_PLAYER_MONEY, 250);
            PlayerPrefs.SetInt(HEATH_PLAYER_MONEY, 250);
            PlayerPrefs.SetInt(CAP_PLAYER, 10);
            PlayerPrefs.SetInt(HEATH_PLAYER, 100);
            PlayerPrefs.SetFloat(SPEED_PLAYER, 3);
            PlayerPrefs.SetInt(RANK_LEVEL, 0);
            PlayerPrefs.SetInt(LEVEL_CURRENT, 1);


        }
    }

    public static void ResetLevel()
    {

        PlayerPrefs.SetInt(MONEY, 0);
        PlayerPrefs.SetInt(GEM, 0);
        PlayerPrefs.SetInt(ACTIVE_ROOM + "LEFT", 0);
        PlayerPrefs.SetInt(ACTIVE_ROOM + "RIGHT", 0);
        PlayerPrefs.SetInt(MONEY_WORKER, 0);
        PlayerPrefs.SetInt(BULLET_WORKER, 0);
        PlayerPrefs.SetInt(MINE_WORKER, 0);
        PlayerPrefs.SetInt(ACTIVE_AUTO_TORRET + "0", 0);
        PlayerPrefs.SetInt(ACTIVE_AUTO_TORRET + "1", 0);
        PlayerPrefs.SetInt(ACTIVE_AUTO_TORRET + "2", 0);
        PlayerPrefs.SetInt(ACTIVE_AUTO_TORRET + "3", 0);
        PlayerPrefs.SetInt(RED_WALL + "0", 0);
        PlayerPrefs.SetInt(RED_WALL + "1", 0);


    }

    public static void AddMoney(int value)
    {
        PlayerPrefs.SetInt(MONEY, PlayerPrefs.GetInt(MONEY) + (value));
    }

    public static int GetMoney()
    {
        return PlayerPrefs.GetInt(MONEY);
    }

    public static void AddGem(int value)
    {
        PlayerPrefs.SetInt(GEM, PlayerPrefs.GetInt(GEM) + (value));
    }

    public static int GetGem()
    {
        return PlayerPrefs.GetInt(GEM);
    }



    public static void AddQuestZombie(int value)
    {
        PlayerPrefs.SetInt(QUEST_ZOMBIE, PlayerPrefs.GetInt(QUEST_ZOMBIE) + (value));
    }

    public static int GetQuestZombie()
    {
        return PlayerPrefs.GetInt(QUEST_ZOMBIE);
    }

    public static void AddQuestRescue(int value)
    {
        PlayerPrefs.SetInt(QUEST_RESCUE, PlayerPrefs.GetInt(QUEST_RESCUE) + (value));
    }

    public static int GetQuestRescue()
    {
        return PlayerPrefs.GetInt(QUEST_RESCUE);
    }

    public static void AddQuestHire(int value)
    {
        PlayerPrefs.SetInt(QUEST_HIRE, PlayerPrefs.GetInt(QUEST_HIRE) + (value));
    }

    public static int GetQuestHire()
    {
        return PlayerPrefs.GetInt(QUEST_HIRE);
    }

    public static void AddQuestActiveBomb(int value)
    {
        PlayerPrefs.SetInt(QUEST_ACTIVEBOMB, PlayerPrefs.GetInt(QUEST_ACTIVEBOMB) + (value));
    }

    public static void AddQuestRedWall(int value)
    {
        PlayerPrefs.SetInt(QUEST_UNLOCKREDWALL, PlayerPrefs.GetInt(QUEST_UNLOCKREDWALL) + (value));
    }

    public static void AddQuestKillBoss(int value)
    {
        PlayerPrefs.SetInt(QUEST_KILLBOSS, PlayerPrefs.GetInt(QUEST_KILLBOSS) + (value));
    }

}
