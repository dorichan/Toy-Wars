using UnityEngine;
using System.Collections;

public class CaptureCastle : MonoBehaviour 
{
	private GameManager gm;
	public int scoreValue = 10;

	void Start()
	{
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject == GameObject.FindWithTag ("Blue") && this.gameObject.tag == "RedCastle") {
			gm.AddBlueScore(scoreValue);
		}
		if(other.gameObject == GameObject.FindWithTag ("Red") && this.gameObject.tag == "BlueCastle") {
			gm.AddRedScore(scoreValue);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject == GameObject.FindWithTag ("Blue") && this.gameObject.tag == "RedCastle") {
			gm.StopBlueScore();
		}
		if(other.gameObject == GameObject.FindWithTag ("Red") && this.gameObject.tag == "BlueCastle") {
			gm.StopRedScore();
		}
	}
}