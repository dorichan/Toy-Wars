using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToyAutoFire : MonoBehaviour 
{
	public bool isAttacking;
	private ToyAI ta;

	void Start()
	{
		isAttacking = false;
		ta = GetComponent<ToyAI> ();
	}

	void Update()
	{
		if (ta.enemy != null) {
			Debug.Log (this.gameObject.name + " sees target!");

		}
	}
}