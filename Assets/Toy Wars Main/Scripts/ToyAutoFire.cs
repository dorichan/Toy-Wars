using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToyAutoFire : MonoBehaviour 
{
	public float counter;
	public GameObject laserPrefab;
	private GameManager gm;
	private ToyAI ta;

	void Awake()
	{
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		ta = GetComponent<ToyAI> ();
	}

	void Start()
	{
		counter = 0.0f;

		if (laserPrefab == null) {
			laserPrefab = GameObject.FindGameObjectWithTag("Laser");
		}
	}

	public void DoShoot()
	{
		if(ta.enemy != null) { 
//			gm.GetNextLaser (transform.position, transform.rotation);

			counter += 1.0f * Time.deltaTime;

			if(counter >= 2.0f) {
				GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation) as GameObject;
			} 
			if (counter >= 2.1f) {
				counter = 0.0f;
			}
		}
	}


}