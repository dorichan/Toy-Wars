using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToyAutoFire : MonoBehaviour 
{
	private GameManager gm;
	private ToyAI ta;

	void Awake()
	{
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		ta = GetComponent<ToyAI> ();
	}

	public void Fire()
	{
		if(ta.enemy != null) {
			gm.Shoot (ta.enemy.transform.position);
		}
	}


}