using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HideObject : MonoBehaviour
{
    public float timelife;
    float existTime;
    public string tags;
    Animator animator;
    public bool isUseAnimation;
    AudioSource audioSource;

    private void Awake()
    {
        if (!isUseAnimation)
            animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }



    private void OnEnable()
    {
        existTime = timelife;

        PlaySound();

        if (!isUseAnimation)
        StartCoroutine(WaitOneSecond());

    }


    void PlaySound()
    {
        if (PlayerPrefs.GetInt(StringManager.SOUND) == 0)
            audioSource.Play();
    }


    void Update()
    {
        if (existTime <= 0)
        {
            if (tags != null)
                ObjectPooler.instance.EnQueueObject(tags, gameObject);
            else
                Destroy(gameObject);
        }
        else existTime -= Time.deltaTime;
    }

    IEnumerator WaitOneSecond()
    {
        animator.enabled = true;
        yield return new WaitForEndOfFrame();
        animator.enabled = false;
    }
}
