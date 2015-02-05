using UnityEngine;
using System.Collections;

public class AIBehaviour : MonoBehaviour 
{
	private Transform target;
	private Transform enemy;

	public AutoFire shoot;
	public AutoFire cease;

	public State currentState;
	public static State current;

	private bool nextDest = false;
	private bool isAvoidingWall = false;
	private Vector3 random;

	private float maxRange = 5.0f;
	private Vector3 heading_left, heading_right;
	private float moveSpeed = 5.0f;
	private float rotationSpeed = 10.0f;
	private float wanderDistance = 8.0f;
	private float arriveDistance = 4.0f;
	private float minDistance = 6.0f;
	private float attackDistance = 15.0f;
	private float safeDistance = 25.0f;
	private float counter = 0.0f;

	void Start()
	{
		cease.OnStopFire ();
	}

	public enum State 
	{
		Track,
		Move,
		Retreat,
		Patrol,
		Defend
	}

	void Update ()
	{
		switch(currentState)
		{
			case State.Track:
				Pursuit ();
				break;
			case State.Move:
				Seek();
				break;
			case State.Retreat:
				Evade();
				break;
			case State.Patrol:
				Wander();
				break;
			case State.Defend:
				Defend();
				break;
		}

		current = currentState;
		WallAvoidance();
		counter += Time.deltaTime;

		if(counter > 5.0)
		{
			counter = 0.0f;
			nextDest = true;
		}
	}

	void Defend()
	{
		transform.RotateAround (transform.position, transform.up, (Random.Range (-5.0f, 50.0f)) * Time.deltaTime);
		Attack ();
	}

	void Wander()
	{
		if(nextDest == true)
		{
			random = new Vector3(Random.Range(-10, 10), 0.0f, (Random.Range (-10, 10)));
			nextDest = false;
		}

		transform.rotation = 
			Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(random), 
			                rotationSpeed * Time.deltaTime);

		if(random.magnitude > wanderDistance)
		{
			Vector3 moveVector = random.normalized * moveSpeed * Time.deltaTime;
			transform.position += moveVector;
		}

		Attack ();
	}

	void Seek() 
	{
		cease.OnStopFire ();

		Vector3 direction = target.position - transform.position;
		direction.y = 0.0f;

		// Look at the target
		transform.rotation = 
			Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 
			                 rotationSpeed * Time.deltaTime);

		// If the distance between the target and the gameObject is greater than the minDistance - keep moving
		if(direction.magnitude > minDistance && isAvoidingWall == false)
		{
			Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
			transform.position += moveVector;
		}

		Arrive ();
	}

	void Flee()
	{
		Vector3 direction = target.position - transform.position;
		direction.y = 0.0f;

		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-direction),
		                                      rotationSpeed * Time.deltaTime);
		
		if(direction.magnitude < safeDistance)
		{
			Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
			transform.position -= moveVector;
		}
	}

	void Arrive()
	{
		if(enemy != null) {
			Vector3 direction = target.position - transform.position;
			direction.y = 0.0f;

			if(direction.magnitude > arriveDistance)
			{
				Vector3 moveVector = direction.normalized * (moveSpeed / 2) * Time.deltaTime;
				transform.position += moveVector;
			}

			Attack();
		}
	}

	// Should be something similar to seek, only it follows a target instead of a cursor
	void Pursuit()
	{
		if(enemy != null) {
			Vector3 direction = enemy.position - transform.position;
			direction.y = 0.0f;

			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (direction),
			                                       rotationSpeed * Time.deltaTime);

			if(direction.magnitude > attackDistance && enemy != null)
			{
				Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
				transform.position += moveVector;
			}

			Attack ();
		}
	}

	public void Attack()
	{
		Vector3 direction = enemy.position - transform.position;
		direction.y = 0.0f;


		if(direction.magnitude < attackDistance)
		{
			shoot.OnStartFire();
		}
		if(direction.magnitude > attackDistance)
		{
			cease.OnStopFire();
		}
		if(enemy == null)
		{
			cease.OnStopFire();
		}
	}

	// Should be same code as above - only difference should be the if statement determining that this is an evade situation
	void Evade()
	{
		Vector3 direction = enemy.position - transform.position;
		direction.y = 0.0f;
		
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (-direction),
		                                      rotationSpeed * Time.deltaTime);
		
		if(direction.magnitude < safeDistance)
		{
			Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
			transform.position -= moveVector;
			// Once evading - should look for health or ammo packs on its own
		}
	}

	void WallAvoidance()
	{
		RaycastHit hit;
		LayerMask wall = 1 << 8;

		Vector3 left = transform.position;
		Vector3 right = transform.position;

		heading_right = transform.TransformDirection(1.0f, 0.0f, 1.0f);
		heading_left = transform.TransformDirection(-1.0f, 0.0f, 1.0f);


		Vector3 moveVector = Vector3.zero;
		
		if (Physics.Raycast (transform.position, transform.forward, out hit, maxRange, wall))
		{
			isAvoidingWall = true;

			moveVector = transform.right * moveSpeed * Time.deltaTime;
			moveVector.y = 0.0f;

			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (moveVector),
			                                      rotationSpeed * Time.deltaTime);
			transform.position += moveVector;
		}
		
		if (Physics.Raycast (right, heading_right, out hit, maxRange, wall))
		{
			isAvoidingWall = true;

			moveVector = -transform.right * moveSpeed * Time.deltaTime;
			moveVector.y = 0.0f;

			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (moveVector),
			                                      rotationSpeed * Time.deltaTime);
			transform.position += moveVector;
		}
		
		if (Physics.Raycast (left, heading_left, out hit, maxRange, wall))
		{
			isAvoidingWall = true;

			moveVector = transform.right * moveSpeed * Time.deltaTime;
			moveVector.y = 0.0f;

			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (moveVector),
			                                      rotationSpeed * Time.deltaTime);
			transform.position += moveVector;
		}

		isAvoidingWall = false;
	}

	public void setTarget(GameObject targ)
	{
		target = targ.transform;
	}
	
	public void SetRobotEnemies()
	{
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Floater");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		foreach(GameObject go in gos) {
			if((go.transform.position - position).sqrMagnitude < distance) {
				closest = go;
			}
		}
		if(gos == null){
			enemy = null;
		}
		if(gos != null) {
			enemy = closest.transform;
		}
//
//		userinterface.rCursor.SetActive (true);
//		userinterface.bCursor.SetActive (false);
//		userinterface.blueTurn = false;
//		userinterface.redTurn = true;
	}

	public void SetFloaterEnemies()
	{
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Robot");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		
		foreach(GameObject go in gos) {
			if((go.transform.position - position).sqrMagnitude < distance) {
				closest = go;
			}
		}
		if(gos == null){
			enemy = null;
		}
		if(gos != null) {
			enemy = closest.transform;
		}

//		userinterface.rCursor.SetActive (false);
//		userinterface.bCursor.SetActive (true);
//		userinterface.blueTurn = true;
//		userinterface.redTurn = false;
	}
}