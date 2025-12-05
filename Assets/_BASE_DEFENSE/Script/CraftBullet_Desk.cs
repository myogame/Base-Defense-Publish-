using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftBullet_Desk : MonoBehaviour
{
    PlayerControler playerController;

    private void Awake()
    {
        playerController = PlayerControler.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" )
        {
            if (!PlayerControler.instance.bullet_Carry)
            {
                SoundManager.ins.PlaySound(2);
                PlayerControler.instance.bullet_Carry = true;
                StartCoroutine(MoveBulletToInvntory(other.gameObject.transform));
            }

            if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) == 1)
                TutorialManager.ins.CheckStageTutorial();

            if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) == 9)
                TutorialManager.ins.CheckStageTutorial();
        }

        if (other.gameObject.tag == "Allay_Amor" && !other.gameObject.GetComponent<Allay_Turret_GetAmor>().bullet_Carry)
        {

            Allay_Turret_GetAmor allyAmor = other.gameObject.GetComponent<Allay_Turret_GetAmor>();
            allyAmor.turretGun = allyAmor.findCurrentTarget_Random();
            allyAmor.restTime = 3;
            allyAmor.reload = false;
            allyAmor.bullet_Carry = true;
            StartCoroutine(MoveBulletToInvntory_Ally(other.gameObject.transform, allyAmor));

        }
    }

    IEnumerator MoveBulletToInvntory(Transform player)
    {
        for (int i = 0; i < playerController.bullet_list.Count; i++)
        {
            playerController.bullet_list[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);

        }
        
    }

    IEnumerator MoveBulletToInvntory_Ally(Transform player, Allay_Turret_GetAmor allyAmor)
    {
        for (int i = 0; i < allyAmor.bullet_list.Count; i++)
        {
            allyAmor.bullet_list[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);

        }
    }
}
