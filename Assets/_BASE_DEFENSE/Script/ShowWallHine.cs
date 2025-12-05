using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWallHine : MonoBehaviour
{
    public GameObject wall;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(ShowWall());
        }
    }

    IEnumerator ShowWall()
    {
        yield return new WaitForSeconds(1);
        wall.SetActive(true);
        Destroy(gameObject);
    }
}
