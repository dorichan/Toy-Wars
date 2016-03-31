using UnityEngine;
using System.Collections;

public class ToyMovement : MonoBehaviour 
{
	private float attackDistance;
	private float arriveDistance;
	private float stopDistance;
	public float speed; 
	private ToyAI thisToyAI;

	public bool isMoving;
	public bool isAttacking;

	void Awake()
	{
		thisToyAI = GetComponent<ToyAI>();
	}

	void Start()
	{
		attackDistance = 5.0f;
		arriveDistance = 8.0f;
		stopDistance = 8.0f;
		speed = 8.0f;
	}

	void Update()
	{
		if (isMoving) {
			Vector3 distance = thisToyAI.target.transform.position - this.gameObject.transform.position;

			if (distance.magnitude > arriveDistance) {
				Vector3 moveVector = distance.normalized * speed * Time.deltaTime;
				moveVector.y = 0.0f;
				transform.position += moveVector;
				transform.LookAt(GameObject.Find ("Cursor").transform);
			}

			if(distance.magnitude < stopDistance) {
				thisToyAI.currentState = ToyAI.State.Idle;
			}

			if (isAttacking) {
				if(distance.magnitude > attackDistance) {
					Vector3 moveVector = distance.normalized * speed * Time.deltaTime;
					moveVector.y = 0.0f;
					transform.position += moveVector;
					transform.LookAt(thisToyAI.enemy.transform);
				}
			}
		}
	}
}