using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GunControler_Ally : MonoBehaviour
{

	//variables visible in the inspector
	
	public float timeDelay;
	bool shooting;
	public bool shootStart;
	public ParticleSystem fxMuzzle;
	Transform arrowSpawner;
	Ally_Gun_Controller ally_Gun_Controller;

	public string bulletTag;
	public string bulletSoundTags;

    private void Awake()
    {
		ally_Gun_Controller = GetComponent<Ally_Gun_Controller>();
		arrowSpawner = fxMuzzle.gameObject.transform;
	}


    void Update()
	{

		if (!shooting && ally_Gun_Controller.currentTarget != null && shootStart)
			StartCoroutine(shoot());

	}


	IEnumerator shoot()
	{
		shooting = true;
		fxMuzzle.Play();

		SoundManager.ins.PlayBulletSound(bulletSoundTags, transform.position);
		ObjectPooler.instance.SpawnFormPool(bulletTag, arrowSpawner.position, arrowSpawner.rotation);
	
		yield return new WaitForSeconds(timeDelay);
		shooting = false;
	}

	
}
