using UnityEngine;
using System.Collections;

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

	void Awake()
	{
		ui = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UserInterface>();
		tm = GetComponent<ToyMovement>();
	}

	void Start()
	{
		SetEnemies();
		ui.SeeMe(this.gameObject);
		currentState = State.Move;
	}

	void Update()
	{
		if (currentState == State.Attack) {

		}

		if (currentState == State.Move) {
			if(target != null) {
				tm.isMoving = true;
			}
		}

		if (currentState == State.Idle) {

		}
	}

	void OnTriggerEnter(Collider other)
	{

	}

	void OnTriggerExit(Collider other)
	{

	}

	public void SetTarget(GameObject target)
	{
		
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
		
	}
}