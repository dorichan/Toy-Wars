using UnityEngine;
using System.Collections;

public class CursorBehaviour : MonoBehaviour 
{
	private float speed;
	private float cTimer;
	public bool isActive;
	private bool startCTimer;

	void Start()
	{
		speed = 25.0f;
		cTimer = 0.0f;
		isActive = false;
		startCTimer = false;
	}

	void Update ()
	{
		float rotate = speed * Time.deltaTime;
		this.transform.Rotate(0.0f, rotate, 0.0f);

		if(isActive) {
			this.gameObject.transform.FindChild("Cursor").GetComponent<Renderer>().enabled = true;
			startCTimer = true;
		}
		if(!isActive) {
			this.gameObject.transform.FindChild("Cursor").GetComponent<Renderer>().enabled = false;
		}

		if(startCTimer) {
			cTimer += 1.0f * Time.deltaTime;
		}

		if(cTimer >= 3.0f) {
			isActive = false;
			startCTimer = false;
			cTimer = 0.0f;
		}
	}
}
