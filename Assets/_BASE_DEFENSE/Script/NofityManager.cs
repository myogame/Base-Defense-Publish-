using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NofityManager : MonoBehaviour
{
    public static NofityManager ins;

    GameObject nofityPop;
    TextMeshProUGUI nofityText;

    private void Awake()
    {
        ins = this;
        nofityPop = transform.Find("/Canvas/Nofity").gameObject;
        nofityText = transform.Find("/Canvas/Nofity/BG/Title").GetComponent<TextMeshProUGUI>();
        nofityPop.SetActive(false);
    }

    public void Nofity(string content)
    {
        nofityText.text = content;
        nofityPop.SetActive(true);
        StartCoroutine(SetDeActive());
    }

    IEnumerator SetDeActive()
    {
        yield return new WaitForSeconds(3);
        nofityPop.SetActive(false);
    }
}
