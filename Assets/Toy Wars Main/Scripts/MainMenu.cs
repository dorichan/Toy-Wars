using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUISkin menuSkin;
	public Transform target;
	
	void Update()
	{
		Vector3 direction = target.position - transform.position;
		transform.rotation = Quaternion.Lerp (transform.rotation, 
		                                      Quaternion.LookRotation (direction),
		                                      2 * Time.deltaTime);
	}

	void OnGUI()
	{
		GUI.skin = menuSkin;

		GUI.BeginGroup (new Rect(0, 0, Screen.height, Screen.width));
		GUI.Box (new Rect (240, 570, 320, 260), "");
		GUI.Label (new Rect (250, 580, 300, 60), "Toy Wars");
		if (GUI.Button (new Rect (250, 650, 150, 50), "Play")) {
			Application.LoadLevel ("level_02");
		}
		if (GUI.Button (new Rect (250, 710, 150, 50), "Credits")) {
			Application.LoadLevel ("credits");
		}
		if (GUI.Button (new Rect (250, 770, 150, 50), "Exit")) {
			Application.Quit ();
		}

		GUI.EndGroup ();
	}
}