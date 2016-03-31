using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToyAI : MonoBehaviour 
{
	public enum State {
		Attack, 
		Move,
		Idle
	};

	public State currentState;
	public Transform target;
	public string enemyTag;
	public GameObject enemy;

	private UserInterface ui;
	private ToyMovement tm; 
	private ToyAutoFire af;

	public List<GameObject> inRange = new List<GameObject> ();

	void Awake()
	{
		ui = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UserInterface>();
		tm = GetComponent<ToyMovement>();
		af = GetComponent<ToyAutoFire>();
	}

	void Start()
	{
		ui.SeeMe(this.gameObject);
		currentState = State.Idle;
		SetEnemies ();
	}

	void Update()
	{
		if (inRange.Capacity > 0 && enemy == null) {
			FindClosestEnemy ();
		} 

		if (enemy != null) {
			currentState = State.Attack;
		}

		if (currentState == State.Attack) {
			tm.isAttacking = true;
			transform.LookAt(enemy.transform);
			af.DoShoot ();
		}

		if (currentState == State.Move) {
			if (target != null || enemy != null) {
				tm.isMoving = true;
				tm.isAttacking = false;
			}
		}

		if (currentState == State.Idle) {
			if(target != null) {
				currentState = State.Move;
			}
		}
	}

	void SetEnemies()
	{
		if (this.gameObject.tag == "Blue") {
			enemyTag = "Red";
		}
		if (this.gameObject.tag == "Red") {
			enemyTag = "Blue";
		}
	}
	
	void FindClosestEnemy()
	{
		enemy = inRange [0]; 
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == enemyTag) {
			currentState = State.Attack;
			inRange.Add (other.gameObject);
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == enemyTag) {
			inRange.Remove (other.gameObject);
			enemy = null;
		}
	}
}