using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    GameObject loadLevelPop;

    private void Awake()
    {
        loadLevelPop = transform.Find("/Canvas/NextLevel").gameObject;
        loadLevelPop.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            loadLevelPop.SetActive(true);
        }
    }
}
