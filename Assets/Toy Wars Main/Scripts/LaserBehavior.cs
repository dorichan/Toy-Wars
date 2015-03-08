using UnityEngine;
using System.Collections;

public class LaserBehavior : MonoBehaviour 
{
	public float counter;
	public bool isShooting;
	private float speed;
	private ToyAI ta;

	void Awake() 
	{
		ta = GetComponent<ToyAI> ();
	}

	void Start()
	{
		counter = 0;
		isShooting = true;
		speed = 100.0f;
	}

	void Update()
	{
		if (isShooting) {
			Debug.Log ("I'm alive!");
			counter += 1.0f * Time.deltaTime;

			if (counter >= 2.0f) {
				transform.position += transform.position * speed * Time.deltaTime;
			} else if (counter >= 3.0f) {
				Destroy (this.gameObject);
			}
		}
	}
}