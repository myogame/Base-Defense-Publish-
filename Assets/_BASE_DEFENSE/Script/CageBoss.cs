using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CageBoss : MonoBehaviour
{
    TextMeshProUGUI helpText;
    public GameObject general;


    void Awake()
    {
        helpText = transform.Find("Canvas/Image/Text").GetComponent<TextMeshProUGUI>();
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!GameManager.intance.isBossDead)
                helpText.text = "Kill Boss";
            else
            {
                gameObject.transform.DOMoveY(54, 5f).OnComplete(MoveGeneral);
                
            }
                

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            helpText.text = "Help";
        }
    }

    void MoveGeneral()
    {
        general.GetComponent<Animator>().SetBool("Run", true);
        general.transform.DORotate(new Vector3(0, 180, 0), 0.5f);
        general.transform.DOMoveZ(-92, 5f);
       
    }

}
