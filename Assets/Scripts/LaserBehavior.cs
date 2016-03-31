using UnityEngine;
using System.Collections;

public class LaserBehavior : MonoBehaviour 
{
	public float counter;
	private float speed;
	private ToyAI ta;
	private GameManager gm;

	void Awake() 
	{
		ta = GetComponent<ToyAI> ();
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}

	void Start()
	{
		counter = 0;
		speed = 50.0f;
	}

	void Update()
	{
		counter += 1.0f * Time.deltaTime;
		transform.position += transform.forward * speed * Time.deltaTime;

		RaycastHit hit;
		if (Physics.Raycast (transform.position, -Vector3.forward, out hit)) {
			if(hit.transform) {
				Health targetHealth = hit.transform.gameObject.GetComponent<Health>();

				if(targetHealth) {
					targetHealth.OnDamage();

					Debug.Log (hit.transform.gameObject.name + " has been damaged and has " + 
					           hit.transform.gameObject.GetComponent<Health>().health + " health.");
				}

			}
		}

		if (counter >= 2.0f) {
			counter = 0.0f;
			Destroy(this.gameObject);
		}
	}
}