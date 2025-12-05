using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GunControler : MonoBehaviour
{

	public float timeDelay;
	bool shooting;
	public bool shootStart;
	public ParticleSystem fxMuzzle;
	PlayerControler playerControler;

    private void Awake()
    {
		playerControler = GetComponent<PlayerControler>();
    }


    void Update()
	{

		if (!shooting && shootStart && !playerControler.enter_Base)
			StartCoroutine(shoot());

	}


	IEnumerator shoot()
	{
		shooting = true;
		fxMuzzle.Play();

		SoundManager.ins.PlayBulletSound("BulletPistolSound", transform.position);
		ObjectPooler.instance.SpawnFormPool("Bullet_Gun", fxMuzzle.transform.position, fxMuzzle.transform.rotation);

		yield return new WaitForSeconds(timeDelay);
		shooting = false;
	}

	
}
