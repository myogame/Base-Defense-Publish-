using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GuitarManager : MonoBehaviour
{
    GameObject guitar;
    AudioSource guitarAudio;
    bool startGuitar;


    private void Awake()
    {
        guitar = transform.Find("Guitar").gameObject;
        guitarAudio = transform.Find("AudioSource").GetComponent<AudioSource>();

   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            guitar.SetActive(false);
            PlayerControler.instance.PlayGuitar();
           
            startGuitar = true;
            guitarAudio.Play();
 
        }
    }

    private void Update()
    {
        if (startGuitar && guitarAudio.volume <= 1)
            guitarAudio.volume += Time.deltaTime*0.1f;
   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            guitar.SetActive(true);
            PlayerControler.instance.StopGuitar();
           
            guitarAudio.Stop();
            startGuitar = false;
            guitarAudio.volume = 0;
          
        }
    }



}
