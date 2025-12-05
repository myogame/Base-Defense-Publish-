using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;


public class EnemyControler : MonoBehaviour
{

	[Header("Enemy Properties")]
	public int lives;
	public int dame;
	public GameObject ragdoll;
	public bool mute;
	public bool attacking;
	public string enemyTags;
	public float stoppingDistance = 0.5f;
	public bool isUseWeapon;

	[HideInInspector] public Transform currentTarget;
	[HideInInspector] public string attackTag = "Turret";
	[HideInInspector] public NavMeshAgent agent;
	[HideInInspector] public Animator animator;
	[HideInInspector] public ParticleSystem booldFx;
	GameObject health;
	Slider healthbar;
	int startLives;
	bool dead;
	public int moneyDrop = 3;
	private void Awake()
    {
		agent = gameObject.GetComponent<NavMeshAgent>();
		animator = gameObject.GetComponent<Animator>();
		
		health = transform.Find("Health").gameObject;
		healthbar = transform.Find("Health/Healthbar").GetComponent<Slider>();
		booldFx = transform.Find("Sparks").GetComponent<ParticleSystem>();




		health.SetActive(false);
		healthbar.maxValue = lives;
		startLives = lives;
	}

    void Start()
	{

		
		agent.enabled = false;
		
	}

    private void OnEnable()
    {
		ragdoll.SetActive(false);
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


		if (currentTarget == null && GameObject.FindGameObjectsWithTag(attackTag).Length > 0)
            currentTarget = findCurrentTarget();
		
		if (currentTarget != null && !dead)
		{
			if (!currentTarget.gameObject.activeSelf)
				currentTarget = null;
            else
            {
				


				agent.isStopped = false;
				
				agent.destination = currentTarget.position;

				if (Vector3.Distance(currentTarget.position, transform.position) <= agent.stoppingDistance + stoppingDistance)
				{
					transform.LookAt(currentTarget.position);

					if (isUseWeapon)
						animator.SetBool("AttackWithWeapon", true);
					else
						animator.SetBool("Attack", true);
					
					agent.isStopped = true;

				}
				else
				{
					if (isUseWeapon)
						animator.SetBool("AttackWithWeapon", false);
					else
						animator.SetBool("Attack", false);
					
					agent.isStopped = false;
				}
			}

			

		} 
	}

	public void FindTurret()
    {

			attackTag = "Turret";
			currentTarget = null;
			agent.speed = 0.5f;
		animator.SetBool("Run", false);
		

	}


	public Transform findCurrentTarget()
	{

		GameObject[] enemies = GameObject.FindGameObjectsWithTag(attackTag);
		Transform target = null;

			float closestDistance = Mathf.Infinity;

			foreach (GameObject potentialTarget in enemies)
			{
				if (Vector3.Distance(transform.position, potentialTarget.transform.position) < closestDistance && potentialTarget != null)
				{
					closestDistance = Vector3.Distance(transform.position, potentialTarget.transform.position);
					target = potentialTarget.transform;
				}
			}

			if (target)
				return target;
		

		return null;
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Gate")
        {
			if(!mute)
				FindTurret();

		}
    }

	public IEnumerator die()
	{
		dead = true;
		ragdoll.transform.rotation = transform.rotation;
		ragdoll.transform.position = transform.position;
		ragdoll.SetActive(true);

		if (ObjectPooler.instance.poolDic["Money"].Count > moneyDrop)
        {
			for (int i = 0; i < moneyDrop; i++)
				ObjectPooler.instance.SpawnFormPool("Money", transform.position, Quaternion.identity);
		}

		GameManager.intance.actionPoint += 0.001f;
		StringManager.AddQuestZombie(1);

		


		yield return new WaitForEndOfFrame();
        ResetEnmy();
        ObjectPooler.instance.EnQueueObject(enemyTags, gameObject);

    }
	

    public void ResetEnmy()
    {
		attackTag = "Turret";
		currentTarget = null;
		dead = false;
		mute = false;
		
		animator.SetBool("Run", false);
		health.SetActive(false);
		lives = startLives;
		healthbar.GetComponent<Slider>().maxValue = lives;
		agent.speed = 0.5f;
	}

	public void MoveToBomb(Transform bomb)
    {
		currentTarget = bomb;
		agent.speed = 3;
		mute = true;
		
		animator.SetBool("Run", true);
	}


	private void OnCollisionEnter(Collision collision)
	{

		
		if (collision.gameObject.tag == "Player" && attacking)
		{
            if (!PlayerControler.instance.dead)
            {
				WorldCanvasController.instance.AddDamageText(currentTarget.position + new Vector3(0, 2f, 0), "-" + dame.ToString(), Color.red);
				PlayerControler.instance.hearth_Player -= dame;
			}
			
		}

		if (collision.gameObject.tag == "Ally_Gun" && attacking)
		{
			
			WorldCanvasController.instance.AddDamageText(currentTarget.position + new Vector3(0, 2f, 0), "-" + dame.ToString(), Color.red);
			if(currentTarget.GetComponent<Ally_Gun_Controller>() != null)
				currentTarget.GetComponent<Ally_Gun_Controller>().TakeDame(dame, transform);
		}
	}

}
