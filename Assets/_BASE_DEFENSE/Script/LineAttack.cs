using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LineAttack : BossSkill
{
    //public GameObject circleShape;
    public GameObject laserLine;
    public ParticleSystem skillFx;
    public ParticleSystem stompFx;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = skillFx.gameObject.GetComponent<AudioSource>();
    }

    void Update()
	{

		if (!shooting && boss.currentTarget != null && boss.attacking)
        {
           
            StartCoroutine(Throw());
        }
      
	}


    public override IEnumerator Throw()
    {
        shooting = true;
        
        DetectedLocation();
        //GameObject bomb = ObjectPooler.instance.SpawnFormPool(tagsBullet, arrowSpawner.position, arrowSpawner.rotation);
        //bomb.transform.parent = arrowSpawner.transform;
        animator.SetTrigger(attackAnimation.ToString());
        yield return new WaitForSeconds(1f);
        MoveToLocation();
        yield return new WaitForSeconds(1.667f);
        shooting = false;
    }

    void MoveToLocation()
    {

        if (PlayerPrefs.GetInt(StringManager.SOUND) == 0)
            audioSource.Play();

        stompFx.Play();
        skillFx.Play();
        
        laserLine.SetActive(false);


    }

    public override void DetectedLocation()
    {
        transform.LookAt(GetComponent<Boss>().currentTarget.position);
        laserLine.SetActive(true);

    }
}
