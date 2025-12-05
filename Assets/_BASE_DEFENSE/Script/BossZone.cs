using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossType
{
    Circle,
    Line
}

public class BossZone : MonoBehaviour
{
    Boss bossCurrent;
    public BossType bossType;

    private void Awake()
    {
        SetBoss();
    }

   void SetBoss()
    {
        int random = Random.Range(0, 2);
        if (random == 1)
            bossType = BossType.Circle;
        else
            bossType = BossType.Line;


        switch (bossType)
        {
            case (BossType.Line):
                bossCurrent = transform.parent.Find("Boss_Line").GetComponent<Boss>();
                break;
            case (BossType.Circle):
                bossCurrent = transform.parent.Find("Boss_Cricle").GetComponent<Boss>();
                break;
        }

        bossCurrent.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Ally_Gun")
        {
            if (bossCurrent != null)
                bossCurrent.StartAttack(other.gameObject.transform);
        }

      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ally_Gun")
        {
            if(bossCurrent != null)
                bossCurrent.StopAttack();
        }
    }
}
