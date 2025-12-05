using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GunControler_Turret : MonoBehaviour
{

	float timeDelay = 1;
	[HideInInspector] public bool shooting;
	float delayshoot;
	[HideInInspector] public bool active_Gun;
	Bullet_Turret_Zone bullet_zone;
	GameObject bulletEmtyleWaining;
	Animator turretAnimator;
	ParticleSystem mozzlefx;

	void Awake()
    {
		turretAnimator = GetComponent<Animator>();
		mozzlefx = transform.Find("tower/gun/FX").GetComponent<ParticleSystem>();
		bullet_zone = transform.parent.Find("BulletZone").GetComponent<Bullet_Turret_Zone>();
		bulletEmtyleWaining = transform.parent.Find("Canvas").gameObject;
	}


	void Update()
	{
        if (active_Gun)
        {
			if (bullet_zone.bulletCurrent > 0)
			{
				bulletEmtyleWaining.SetActive(false);

				if (delayshoot > 0)
					delayshoot -= Time.deltaTime;
				else
					delayshoot = timeDelay;


				if (delayshoot <= 0 && !shooting)
					StartCoroutine(shoot());
			}
			else
				EmtyBullet();

			
		}

		
	}

	void EmtyBullet()
    {
		bulletEmtyleWaining.SetActive(true);

		if (PlayerPrefs.GetInt(StringManager.TUT_STAGE) == 4)
			TutorialManager.ins.CheckStageTutorial();
	}



	

	IEnumerator shoot()
	{
		shooting = true;

		for(int i = 0; i < 4; i++)
        {
			turretAnimator.SetTrigger("Shoot");
			mozzlefx.Play();


			SoundManager.ins.PlayBulletSound("BulletTurretSound", transform.position);
			ObjectPooler.instance.SpawnFormPool("Bullet_Turret", mozzlefx.transform.position, mozzlefx.transform.rotation);



			yield return new WaitForSeconds(0.2f);
		}

		bullet_zone.bulletCurrent--;

		if (bullet_zone.bulletCurrent < bullet_zone.listBullet_inTurret.Count)
        {
			bullet_zone.listBullet_inTurret[bullet_zone.bulletCurrent].gameObject.SetActive(false);
		}

		

		yield return new WaitForSeconds(timeDelay);
		shooting = false;
	}

	
}
