using UnityEngine;
using System.Collections;
using DG.Tweening;


public class BulletStats : MonoBehaviour
{

	
	public int dame;
	public float timelife = 1f;
	float existTime;
	public float speed = 50;
	public string tags;

	public bool isRocket;
	public bool isSniper;
	ObjectPooler objectPooler;

    private void Awake()
    {
		objectPooler = ObjectPooler.instance;

	}


    private void OnEnable()
    {
		existTime = timelife;
		
	}

    private void Update()
    {


        if (existTime <= 0)
        {
            if (objectPooler.poolDic[tags].Count > 0)
            {
                objectPooler.EnQueueObject(tags, gameObject);
            }
        }
        else existTime -= Time.deltaTime;

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
	{
		//freeze arrow when it hits an enemy and parent it to the enemy to move with it
		if ((collision.gameObject.tag == "Enemy"))
		{
			if(!isSniper)
			ObjectPooler.instance.EnQueueObject(tags, gameObject);


			if (isRocket)
            {
				ObjectPooler.instance.SpawnFormPool("RocketFX", transform.position, Quaternion.identity);
			}
            else
            {
				WorldCanvasController.instance.AddDamageText(collision.transform.position + new Vector3(0, 2f, 0), "-" + dame.ToString(), Color.green);
				collision.gameObject.GetComponent<EnemyControler>().lives -= dame;
				collision.gameObject.GetComponent<EnemyControler>().booldFx.Play();

			}
			
			
		}

		if ((collision.gameObject.tag == "Boss"))
		{
			ObjectPooler.instance.EnQueueObject(tags, gameObject);
			WorldCanvasController.instance.AddDamageText(collision.transform.position + new Vector3(0, 5f, 0), "-" + dame.ToString(), Color.green);
			collision.gameObject.GetComponent<Boss>().lives -= dame;

		}

		if (collision.gameObject.tag == "PreventBullet")
		{
			ObjectPooler.instance.EnQueueObject(tags, gameObject);

		}
	}


}
