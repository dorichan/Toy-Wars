using UnityEngine;
using System.Collections;

public class CaptureBlueCastle : MonoBehaviour 
{
	private GameManager gameManager;
	public int scoreValue = 10;

	void Start()
	{
		GameObject gm = GameObject.FindWithTag ("GameManager");
		if (gm != null) {
			gameManager = gm.GetComponent<GameManager>();
		}
		if(gm == null) {
			Debug.Log ("Cannot find GameManger script.");
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject == GameObject.FindWithTag ("Robot")) {
			gameManager.AddRedScore(scoreValue);
		}
	}
}