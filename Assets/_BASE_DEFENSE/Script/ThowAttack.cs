using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThowAttack : BossSkill
{
    public GameObject circleShape;
    public Transform arrowSpawner;
    public string tagsBullet;
    public float timeDelay;
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
        GameObject bomb = ObjectPooler.instance.SpawnFormPool(tagsBullet, arrowSpawner.position, Quaternion.identity);

        bomb.transform.parent = arrowSpawner.transform;
        animator.SetTrigger(attackAnimation.ToString());
        yield return new WaitForSeconds(1);
        MoveToLocation(bomb.transform);

    }

    void MoveToLocation(Transform bomb)
    {
        float disBomb = Vector3.Distance(bomb.position, circleShape.transform.position);
        bomb.transform.parent = null;
       
        bomb.transform.DOJump(circleShape.transform.position, 5, 1, disBomb/10).OnComplete(()=> {
            shooting = false;
            circleShape.SetActive(false);

        }).SetEase(Ease.Flash);
       

    }

    public override void DetectedLocation()
    {
        circleShape.SetActive(true);
        circleShape.transform.position = GetComponent<Boss>().currentTarget.position;
        transform.LookAt(circleShape.transform.position);
       
    }
}
