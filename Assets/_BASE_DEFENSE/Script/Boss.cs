using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int lives;
    public int dame;
    protected int amountMoney = 10;
    [HideInInspector] public Transform currentTarget;
    protected Animator animator;
    public bool attacking;
    protected GameObject health;
    protected Slider healthbar;
    protected int startLives;
    protected bool dead;
    protected ParticleSystem booldFx;
    public GameObject ragdoll;
    public SkinnedMeshRenderer skinBoss;
    public SkinnedMeshRenderer skinRagdoll;
    public AudioSource audioSource;



    public void IntInfoBoss() 
    {
        animator = gameObject.GetComponent<Animator>();
        health = transform.Find("Health").gameObject;
        healthbar = transform.Find("Health/Healthbar").GetComponent<Slider>();
        booldFx = transform.Find("Sparks").GetComponent<ParticleSystem>();
        audioSource = ragdoll.GetComponent<AudioSource>();

        health.SetActive(false);
        healthbar.maxValue = lives;
        startLives = lives;
    }

    public IEnumerator die()
    {

        dead = true;

        

        StringManager.AddQuestKillBoss(1);
        skinRagdoll.sharedMesh = skinBoss.sharedMesh;
        ragdoll.SetActive(true);

        if (PlayerPrefs.GetInt(StringManager.SOUND) == 0)
            audioSource.Play();

        GameManager.intance.isBossDead = true;
        GameManager.intance.wallFinish.SetActive(false);
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);

    }

    public void StartAttack(Transform currentTarget)
    {
        attacking = true;
        this.currentTarget = currentTarget;
       
    }

    public void StopAttack()
    {
        attacking = false;
        this.currentTarget = null;
      
    }
}

