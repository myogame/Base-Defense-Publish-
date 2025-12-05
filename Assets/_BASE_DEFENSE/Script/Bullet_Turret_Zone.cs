using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Turret_Zone : MonoBehaviour
{
    [HideInInspector] public List<GameObject> listBullet_inTurret;
    [HideInInspector] public int bulletCurrent;
    PlayerControler playerControler;

    void Awake()
    {
        listBullet_inTurret = new List<GameObject>();

        foreach(Transform child in transform)
        {
            listBullet_inTurret.Add(child.gameObject);
        }
    }

    private void Start()
    {
        playerControler = PlayerControler.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && playerControler.bullet_Carry)
        {
            SoundManager.ins.PlaySound(2);
            StartCoroutine(MoveBulletToTurret());

            if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) == 2)
                TutorialManager.ins.CheckStageTutorial();

            if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) == 10)
                TutorialManager.ins.CheckStageTutorial();

        }

        if (other.gameObject.tag == "Allay_Amor" && other.gameObject.GetComponent<Allay_Turret_GetAmor>().bullet_Carry)
        {
            Allay_Turret_GetAmor allayAmor = other.gameObject.GetComponent<Allay_Turret_GetAmor>();
            allayAmor.restTime = 3;
            allayAmor.reload = true;
            StartCoroutine(MoveBulletToTurret_Ally(allayAmor));
        }
    }

    IEnumerator MoveBulletToTurret()
    {
        for (int i = playerControler.bullet_list.Count-1; i >= 0; i--)
        {
            
            playerControler.bullet_list[i].SetActive(false);

            if (bulletCurrent < listBullet_inTurret.Count)
                listBullet_inTurret[bulletCurrent].SetActive(true);

            bulletCurrent++;
            yield return new WaitForSeconds(0.1f);
        }


        playerControler.bullet_Carry = false;

    }

    IEnumerator MoveBulletToTurret_Ally(Allay_Turret_GetAmor allayAmor)
    {
        for (int i = allayAmor.bullet_list.Count - 1; i >= 0; i--)
        {

            allayAmor.bullet_list[i].SetActive(false);

            if (bulletCurrent < listBullet_inTurret.Count)
                listBullet_inTurret[bulletCurrent].SetActive(true);

            bulletCurrent++;
            yield return new WaitForSeconds(0.1f);
        }

        allayAmor.bullet_Carry = false;

    }
}
