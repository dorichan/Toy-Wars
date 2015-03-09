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
		speed = 25.0f;
	}

	void Update()
	{
		counter += 1.0f * Time.deltaTime;
		transform.position += transform.position * speed * Time.deltaTime;

		if (counter >= 2.0f) {
			counter = 0.0f;
			Destroy(this.gameObject);
//			gm.laserCache[gm.activeObj].SetActive (false);
//			gm.activeObj += 1;
		}
	}
}