using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestRagdoll : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlaySound();
        StartCoroutine(WaitOneSecond());
    }

    void PlaySound()
    {
        if (PlayerPrefs.GetInt(StringManager.SOUND) == 0)
            audioSource.Play();
    }

    IEnumerator WaitOneSecond()
    {
        
        animator.enabled = true;
        yield return new WaitForEndOfFrame();
        animator.enabled = false;
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
