using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager ins;

    public AudioClip[] sounds;
    AudioSource audioSource;
    AudioSource backgroundSound;

    private void Awake()
    {
        ins = this;
        audioSource = GetComponent<AudioSource>();
        backgroundSound = transform.Find("/BackgroundSound").GetComponent<AudioSource>();
    }

    public void PlaySound(int soundNumber)
    {
        if(PlayerPrefs.GetInt(StringManager.SOUND) == 0)
        {
            audioSource.clip = sounds[soundNumber];
            audioSource.Play();
        }
        
    }

    public void PlayBulletSound(string tags, Vector3 position)
    {
        if (PlayerPrefs.GetInt(StringManager.SOUND) == 0)
        {
            ObjectPooler.instance.SpawnFormPool(tags, position, Quaternion.identity);
        }
       
    }


    public void SwichBGSound()
    {
        if(PlayerPrefs.GetInt(StringManager.SOUND) == 1)
        {
            backgroundSound.Stop();
        }
        else
        {
            backgroundSound.Play();
        }
    }
}
