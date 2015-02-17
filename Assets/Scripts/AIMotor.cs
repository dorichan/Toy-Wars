using UnityEngine;
using System.Collections;

public class AIMotor : MonoBehaviour 
{
	private float speed = 10.0f;
	public Vector3 velocity;
	private Vector3 position;

	void Start ()
	{
		velocity = Vector3.zero;
	}

	void Update ()
	{
		
		position = this.transform.position;

		var horizontalMovement = Input.GetAxis ("Horizontal");
		var forwardMovement = Input.GetAxis ("Vertical");

		if(horizontalMovement != 0.0f)
		{
			transform.Translate (transform.right * speed * Time.deltaTime);
		}
		if(forwardMovement != 0.0f)
		{
			transform.Translate (transform.forward * speed * Time.deltaTime);
		}

		velocity = transform.position - position;
	}
}