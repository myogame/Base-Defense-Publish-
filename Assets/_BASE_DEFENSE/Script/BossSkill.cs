using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackAnimation
{
	AttackCricle,
	AttackLine
}

public abstract class BossSkill : MonoBehaviour
{

	[HideInInspector] public bool shooting;
	public AttackAnimation attackAnimation;
	[HideInInspector] public Boss boss;
	[HideInInspector] public Animator animator;

	public void Awake()
	{
		boss = GetComponent<Boss>();
		animator = GetComponent<Animator>();
	}

	public abstract IEnumerator Throw();
	public abstract void DetectedLocation();

}
