using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetZombie : MonoBehaviour
{
    public GameObject[] zombies;
    public GameObject[] ragdolls;
    public int idZombie;

    private void Start()
    {
        idZombie = Random.Range(0, zombies.Length);
       
        zombies[idZombie].SetActive(true);
        ragdolls[idZombie].SetActive(true);
    }
}
