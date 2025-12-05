using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCart : MonoBehaviour
{
    public static MineCart instance;

    public GameObject[] gemOnCart;
    int gemCurrent = 6;
    Animator animator;
    [HideInInspector]public bool open = true;

    private void Awake()
    {
        instance = this;
        animator = transform.parent.parent.GetComponent<Animator>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ally_Mine")
        {
            Mine_Ally_Controller mineAlly = collision.gameObject.GetComponent<Mine_Ally_Controller>();

            if (!mineAlly.carry && gemCurrent > 0 && open)
            {
                mineAlly.GemCarry.SetActive(true);
                mineAlly.carry = true;
                gemCurrent--;
                gemOnCart[gemCurrent].SetActive(false);

            }
            else
            {
                open = false;
                animator.SetBool("Open", false);
                StartCoroutine(LoadGemCart());

            }


        }
    }

    IEnumerator LoadGemCart()
    {
        yield return new WaitForSeconds(60);
        gemCurrent = 6;

        for (int i = 0; i < gemCurrent; i++)
            gemOnCart[i].SetActive(true);

        animator.SetBool("Open", true);
        open = true;

    }
   
}
