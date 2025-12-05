using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMine : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ally_Mine")
        {
            Mine_Ally_Controller mineAlly = collision.gameObject.GetComponent<Mine_Ally_Controller>();

            if (!mineAlly.carry)
            {
                mineAlly.pickAxe.SetActive(true);
                mineAlly.animator.SetBool("Miner", true);
                StartCoroutine(MinerComp(mineAlly));
            }


        }
    }

    IEnumerator MinerComp(Mine_Ally_Controller mineAlly)
    {
        yield return new WaitForSeconds(30);
        mineAlly.animator.SetBool("Miner", false);
        mineAlly.pickAxe.SetActive(false);
        mineAlly.GemCarry.SetActive(true);
        mineAlly.carry = true;

    }
}
