using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : Boss
{

    private void Awake()
    {
        IntInfoBoss();
    }

    private void Update()
    {
        if (lives != startLives)
        {
            if (!health.activeSelf)
                health.SetActive(true);

            health.transform.LookAt(2 * transform.position - Camera.main.transform.position);
            healthbar.value = lives;
        }

        if (lives < 1 && !dead) 
            StartCoroutine(die());


    }

  

}
