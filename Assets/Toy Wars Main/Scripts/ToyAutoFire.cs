using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToyAutoFire : MonoBehaviour 
{
	public float counter;
	public GameObject gunTip1;
	public GameObject gunTip2;
	public GameObject laserPrefab;
	public Transform activeGun;
	private GameManager gm;
	private ToyAI ta;
	private int randInt;

	void Awake()
	{
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		ta = GetComponent<ToyAI> ();

		counter = 0.0f;
		randInt = 0;
		activeGun = null;
	}

	void Start()
	{
		counter = 0.0f;

		if (laserPrefab == null) {
			laserPrefab = GameObject.FindGameObjectWithTag("Laser");
		}
	}

	void Update()
	{
		if (counter <= 0.9f) {
			randInt = Random.Range (0,6);
			GetGun();
		}
	}

	void GetGun()
	{
		if (this.gameObject.tag == "Red") {
			if (randInt % 2 == 0) {
				activeGun = gunTip1.transform;
			} else {
				activeGun = gunTip2.transform;
			}
		}

		if (this.gameObject.tag == "Blue") {
			activeGun = gunTip1.transform;
		}
	}

	public void DoShoot()
	{
		if(ta.enemy != null) { 
			counter += 1.0f * Time.deltaTime;

			if(counter >= 2.0f) {
				GameObject laser = Instantiate(laserPrefab, activeGun.transform.position, transform.rotation) as GameObject;
			} 
			if (counter >= 2.1f) {
				counter = 0.0f;
			}
		}
	}


}