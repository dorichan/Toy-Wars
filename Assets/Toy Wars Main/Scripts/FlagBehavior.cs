using UnityEngine;
using System.Collections;

public class FlagBehavior : MonoBehaviour 
{
	public bool isCapturing;
	public int index;
	private float speed;
	private Vector3 direction;
	private float maxHeight;
	private float minHeight;

	private GameManager gm;

	void Awake()
	{
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}

	void Start()
	{
		index = 0;
		speed = 2.0f;
		direction = -Vector3.up;
		maxHeight = 13.0f;
		minHeight = 6.0f;
	}

	void Update()
	{
		if (isCapturing) {
			speed = 2.0f;
			transform.position += direction * speed * Time.deltaTime; 
		}

		if (transform.position.y >= maxHeight) {
			if (index == 1) {
				if (this.gameObject.tag == "BlueFlag") {
					Stop ("Red");
				}

				if (this.gameObject.tag == "RedFlag") {
					Stop ("Blue");
				}
			}
		}

		if (transform.position.y <= minHeight) {
			Reverse();
		}
	}

	public void SetActive()
	{
		isCapturing = true;
	}

	public void Reverse()
	{
		direction = Vector3.up;
		index = 1;

		if (this.gameObject.tag == "BlueFlag") {
			renderer.material.color = Color.red;
		}

		if (this.gameObject.tag == "RedFlag") {
			renderer.material.color = Color.blue;
		}
	}

	public void StopCapture()
	{
		isCapturing = false;
	}

	void Stop(string _winner)
	{
		isCapturing = false;
		index = 0;

		if (_winner == "Red") {
			gm.redWin = true;
		}

		if (_winner == "Blue") {
			gm.blueWin = true;
		}
	}
}