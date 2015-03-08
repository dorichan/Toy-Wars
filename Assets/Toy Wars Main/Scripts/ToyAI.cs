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
		if (inRange.Capacity > 0 && enemy == null) { // TODO Throws argument out of range error 
			FindClosestEnemy();
		}

		if (enemy != null) {
			currentState = State.Attack;
		}

		if (currentState == State.Attack) {
			transform.LookAt(enemy.transform);
			af.Fire ();
		}

		if (currentState == State.Move) {
			if (target != null || enemy != null) {
				tm.isMoving = true;
			}
		}

		if (currentState == State.Idle) {
			tm.isMoving = false;

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