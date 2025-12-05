using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Ally_Gun_Controller : MonoBehaviour
{
	[Header("Ally Gun Properties")]
	public int lives;
	public float runSpeed;
	public float backSpeed;

	[HideInInspector] public NavMeshAgent agent;
	[HideInInspector] public Transform currentTarget;
	[HideInInspector] public string attackTag = "Enemy";

	
	Animator animator;
	public bool attacking;
	GameObject health;
	Slider healthbar;
	private int startLives;
	private bool dead;
	GunControler_Ally gunController;
	public int range;

	public bool isOnBase = true;

	//Image rankShow;
	//public Sprite[] rankImage;
	//[HideInInspector]  public int rankValue;
	//public int rankMaxvalue = 1000;
	int rankCurrent;

	public string classSoldier;

    private void Awake()
    {
		agent = gameObject.GetComponent<NavMeshAgent>();
		animator = gameObject.GetComponent<Animator>();
		gunController = GetComponent<GunControler_Ally>();
		health = transform.Find("Health").gameObject;
		healthbar = transform.Find("Health/Healthbar").GetComponent<Slider>();
		//rankShow = transform.Find("Canvas/Image").GetComponent<Image>();

		health.SetActive(false);
		healthbar.maxValue = lives;
		startLives = lives;
		

	}
    private void Start()
    {
        LoadNewRank();
    }

    void LoadNewRank()
    {
        agent.speed = runSpeed;
        rankCurrent = PlayerPrefs.GetInt(StringManager.RANK_LEVEL);
        //rankShow.sprite = rankImage[rankCurrent];
        //rankMaxvalue = rankMaxvalue * (rankCurrent + 1);
        UpgradeRank();
    }

    //   void CheckRank()
    //   {
    //	rankValue = 0;
    //	rankMaxvalue = rankMaxvalue * rankCurrent;
    //	rankCurrent++;

    //	rankShow.sprite = rankImage[rankCurrent];

    //	UpgradeRank();

    //}

    void UpgradeRank()
    {
        lives = startLives + (100 * rankCurrent);
        healthbar.maxValue = lives;
        startLives = lives;
        range += (1 * rankCurrent);

    }
    private void Update()
    {
		agent.enabled = true;

		if (lives != startLives)
		{
			if (!health.activeSelf)
				health.SetActive(true);

			health.transform.LookAt(2 * transform.position - Camera.main.transform.position);
			healthbar.value = lives;

			
		}

		if (lives < 1 && !dead)
			StartCoroutine(die());

		if (isOnBase)
		{
			agent.destination = GameManager.intance.startMovePos.position;


			if (Vector3.Distance(GameManager.intance.startMovePos.position, transform.position) >= agent.stoppingDistance+1)
			{
				agent.isStopped = false;
				animator.SetBool("Attack", false);
				animator.SetBool("Walk", true);
				//transform.LookAt(GameManager.intance.startMovePos.position);
			}
			else
			{
				isOnBase = false;

			}

		}
		else
		{
			if (currentTarget == null && GameObject.FindGameObjectsWithTag(attackTag).Length > 0)
				currentTarget = findCurrentTarget();

			if (currentTarget != null)
			{
                if (currentTarget.gameObject.activeSelf)
                {
				

				
					
					

					if (Vector3.Distance(currentTarget.position, transform.position) <= agent.stoppingDistance + range)
					{
						
						if(Vector3.Distance(currentTarget.position, transform.position) <= 3 )
                        {
							transform.LookAt(currentTarget.position);
							animator.SetBool("RunShoot", true);
							animator.SetBool("Attack", false);
							animator.SetBool("Walk", true);
							gunController.shootStart = true;
							agent.isStopped = false;
							agent.destination = -currentTarget.position;
							agent.speed = backSpeed;
						}
                        else
                        {
							transform.LookAt(currentTarget.position);
							animator.SetBool("RunShoot", false);
							animator.SetBool("Attack", true);
							animator.SetBool("Walk", false);
							gunController.shootStart = true;
							agent.destination = currentTarget.position;
							agent.isStopped = true;
							agent.speed = runSpeed;

						}

					}
					else
					{
						animator.SetBool("RunShoot", false);
						animator.SetBool("Attack", false);
						animator.SetBool("Walk", true);
						gunController.shootStart = false;
						agent.isStopped = false;
						agent.destination = currentTarget.position;
						agent.speed = runSpeed;
					}
                }
                else
                {
					currentTarget = null;
                }

				

			}


		}

	}

	public void TakeDame(int dameEnemy, Transform enemyPos)
    {
		lives -= dameEnemy;
		currentTarget = enemyPos;
	}
 

	public IEnumerator die()
	{

		dead = true;
		if (ObjectPooler.instance.poolDic["Soldier_Ragdoll"].Count > 0)
		{
			ObjectPooler.instance.SpawnFormPool("Soldier_Ragdoll", transform.position, transform.rotation);
		}

		yield return new WaitForEndOfFrame();
		ResetSoldier();


		ObjectPooler.instance.EnQueueObject(classSoldier, gameObject);
	}

	void ResetSoldier()
    {
		currentTarget = null;
		dead = false;
		animator.SetBool("Attack", false);
		lives = startLives;
		health.SetActive(false);
		healthbar.GetComponent<Slider>().maxValue = lives;
		
	}

	public Transform findCurrentTarget()
	{

		GameObject[] enemies = GameObject.FindGameObjectsWithTag(attackTag);
		Transform target = null;

		float closestDistance = Mathf.Infinity;

		foreach (GameObject potentialTarget in enemies)
		{
			if (Vector3.Distance(transform.position, potentialTarget.transform.position) < closestDistance && potentialTarget != null && potentialTarget.activeSelf)
			{
				closestDistance = Vector3.Distance(transform.position, potentialTarget.transform.position);
				target = potentialTarget.transform;
			}
		}

		if (target)
			return target;


		return null;
	}
}
