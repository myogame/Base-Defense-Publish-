using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
	public static BossController instance;

	[Header("Enemy Properties")]
	public int lives;
	public int dame;
	public int moneyDrop;
	public bool isStand;

	[HideInInspector]
	public NavMeshAgent agent;

	[HideInInspector]
	public float defaultStoppingDistance;
	[HideInInspector]
	public Animator animator;


	public bool attacking;

	public GameObject health;
	public Slider healthbar;
	private int startLives;
	private bool dead;
	PlayerControler playerControler;

	public bool free;
	public bool attack;

	public float[] max_minX;
	public float[] max_minZ;

	Vector3 newFreePos;

	public bool delayAttack;
	public float delayTime = 3;
	public Vector3 playerOldPos;

	public GameObject ragdoll;

	private void Awake()
	{
		instance = this;
		agent = gameObject.GetComponent<NavMeshAgent>();
		animator = gameObject.GetComponent<Animator>();
		



		health.SetActive(false);
		healthbar.maxValue = lives;
		startLives = lives;
	}

	void Start()
	{
		playerControler = PlayerControler.instance;

		defaultStoppingDistance = agent.stoppingDistance;
		agent.enabled = false;

		newFreePos = new Vector3(Random.Range(max_minX[0], max_minX[1]), transform.position.y, Random.Range(max_minZ[0], max_minZ[1]));
	}

	void FixedUpdate()
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

		if (free)
		{
			agent.destination = newFreePos;
			transform.LookAt(newFreePos);
			agent.isStopped = false;

			if (Vector3.Distance(newFreePos, transform.position) <= agent.stoppingDistance)
			{
				newFreePos = new Vector3(Random.Range(max_minX[0], max_minX[1]), transform.position.y, Random.Range(max_minZ[0], max_minZ[1]));
			}

		}


		if (attack)
		{
			if (!delayAttack)
			{
				playerOldPos = playerControler.transform.position;
				transform.LookAt(playerOldPos);

				delayAttack = true;
			}

			agent.destination = playerOldPos;

			if (Vector3.Distance(playerOldPos, transform.position) <= agent.stoppingDistance + 2f)
			{

				animator.SetBool("Attack", true);
				agent.isStopped = true;

				if (delayTime <= 0)
				{
					delayAttack = false;
					delayTime = 3;
				}
				else delayTime -= Time.deltaTime;

			}
			else
			{
				agent.isStopped = false;
				animator.SetBool("Attack", false);
			}


		}


	}

	public void Free()
    {
		
		attack = false;
		agent.speed = 1;
		animator.SetBool("Run", false);
		animator.SetBool("Attack", false);
	}

	public void Attack()
    {
		
		free = false;
		attack = true;
		agent.speed = 5;
		animator.SetBool("Run", true);
    } 

	

	public IEnumerator die()
	{

		dead = true;
		Instantiate(ragdoll, transform.position, Quaternion.identity);
		GameManager.intance.isBossDead = true;
		GameManager.intance.wallFinish.SetActive(false);
		yield return new WaitForEndOfFrame();
		Destroy(gameObject);

	}


	

	private void OnCollisionEnter(Collision collision)
	{

		if (collision.gameObject.tag == "Player" && attacking)
		{

				WorldCanvasController.instance.AddDamageText(playerControler.transform.position + new Vector3(0, 2f, 0), "-" + dame.ToString(), Color.red);
				playerControler.hearth_Player -= dame;

		}
		
	}
}
