using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RespawnTime : MonoBehaviour
{
    TextMeshProUGUI spawnText;
    float time = 10;

    private void Awake()
    {
        spawnText = transform.Find("BG/Title").GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        time = 10;
    }

    private void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
            spawnText.text = "RESPAWN IN " + Mathf.RoundToInt(time).ToString() + "s";
        }
        else
        {
            time = 0;
            spawnText.text = "RESPAWN IN 0s";
        }
    }

}
