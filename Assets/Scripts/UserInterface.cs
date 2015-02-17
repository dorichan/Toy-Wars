using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserInterface : MonoBehaviour
{
	private GameObject rCursor;
	private GUISkin redUnit;
	private Ray myray;
	private Ray newray;
	private Texture2D roboticon;

	public bool gameInProgress;
	public GUISkin redGUISkin;

	public List<GameObject> rList = new List<GameObject>();

	void Start ()
	{
		rCursor = GameObject.Find ("RedCursor");
		gameInProgress = false;
	}
	
	void Update ()
	{
		if (gameInProgress) {		
			if (Input.GetMouseButtonDown(0)) {
				RaycastHit redhit = new RaycastHit ();
				myray = camera.ScreenPointToRay (Input.mousePosition);
				rCursor.GetComponent<CursorBehaviour>().isActive = true;

				if (Physics.Raycast (myray, out redhit)) {
					rCursor.transform.position = redhit.point;
				}
			}

			if (Input.GetMouseButtonDown(1)) {
				RaycastHit enemySelect = new RaycastHit();
				myray = camera.ScreenPointToRay (Input.mousePosition);
					
				if (Physics.Raycast (myray, out enemySelect)) {
					rCursor.transform.position = enemySelect.point;
				}
			}
		}
	}

	void FindToys()
	{
		// Finding all robots and floaters in the scene.
		GameObject[] robots = GameObject.FindGameObjectsWithTag("Robot");
		GameObject[] floaters = GameObject.FindGameObjectsWithTag("Floater");

		foreach(var f in floaters) {
			f.GetComponent<AIBehaviour>().SetFloaterEnemies();
		}

		foreach(var r in robots) {
			r.GetComponent<AIBehaviour>().SetRobotEnemies();
			r.GetComponent<AIBehaviour>().setTarget(rCursor);
			rList.Add (r);
		}
	}
	
	void OnGUI ()
	{
		GUI.BeginGroup (new Rect (0, Screen.height - 150, 120, 160));
		GUI.skin = redGUISkin;
	
		Rect rmoveButton = new Rect (10, 20, 100, 20);
		Rect rdefendButton = new Rect (10, 45, 100, 20);
		Rect rtrackButton = new Rect (10, 70, 100, 20);
		Rect rpatrolButton = new Rect (10, 95, 100, 20);
		Rect rretargetButton = new Rect (10, 120, 100, 20);

		GUI.Box (new Rect (0, 0, 200, 160), "");

		if (GUI.Button (rmoveButton, "Move")) {
			GameObject.Find("Robot(Clone)0").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Move;
			GameObject.Find("Robot(Clone)1").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Move;
			GameObject.Find("Robot(Clone)2").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Move;
			GameObject.Find("Robot(Clone)3").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Move;
			GameObject.Find("Robot(Clone)4").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Move;

			rCursor.SetActive (true);
		}
		if (GUI.Button (rdefendButton, "Defend")) {
			GameObject.Find("Robot(Clone)0").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Defend;
			GameObject.Find("Robot(Clone)1").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Defend;
			GameObject.Find("Robot(Clone)2").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Defend;
			GameObject.Find("Robot(Clone)3").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Defend;
			GameObject.Find("Robot(Clone)4").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Defend;

			rCursor.SetActive (true);
		}
		if (GUI.Button (rtrackButton, "Track")) {
			GameObject.Find("Robot(Clone)0").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Track;
			GameObject.Find("Robot(Clone)1").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Track;
			GameObject.Find("Robot(Clone)2").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Track;
			GameObject.Find("Robot(Clone)3").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Track;
			GameObject.Find("Robot(Clone)4").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Track;

			rCursor.SetActive (true);
		}
		if (GUI.Button (rpatrolButton, "Patrol")) {
			GameObject.Find("Robot(Clone)0").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Patrol;
			GameObject.Find("Robot(Clone)1").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Patrol;
			GameObject.Find("Robot(Clone)2").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Patrol;
			GameObject.Find("Robot(Clone)3").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Patrol;
			GameObject.Find("Robot(Clone)4").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Patrol;

			rCursor.SetActive (true);
		}
		if (GUI.Button (rretargetButton, "Retarget")) {
			GameObject.Find("Robot(Clone)0").GetComponent<AIBehaviour>().SetRobotEnemies();
			GameObject.Find("Robot(Clone)1").GetComponent<AIBehaviour>().SetRobotEnemies();
			GameObject.Find("Robot(Clone)2").GetComponent<AIBehaviour>().SetRobotEnemies();
			GameObject.Find("Robot(Clone)3").GetComponent<AIBehaviour>().SetRobotEnemies();
			GameObject.Find("Robot(Clone)4").GetComponent<AIBehaviour>().SetRobotEnemies();

			GameObject.Find("Robot(Clone)0").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Track;
			GameObject.Find("Robot(Clone)1").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Track;
			GameObject.Find("Robot(Clone)2").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Track;
			GameObject.Find("Robot(Clone)3").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Track;
			GameObject.Find("Robot(Clone)4").GetComponent<AIBehaviour>().currentState = AIBehaviour.State.Track;

			rCursor.SetActive (true);
		}

		GUI.EndGroup ();

		GUI.BeginGroup (new Rect (120, Screen.height - 150, 750, 160));
		GUI.skin = redUnit;
		GUI.Box (new Rect (0, 0, 350, 160), "");
		if (GUI.Button (new Rect (5, 20, 100, 100), roboticon)) {
				
		}
		if (GUI.Button (new Rect (65, 20, 100, 100), roboticon)) {

		}
		if (GUI.Button (new Rect (120, 20, 100, 100), roboticon)) {

		}
		if (GUI.Button (new Rect (175, 20, 100, 100), roboticon)) {

		}
		if (GUI.Button (new Rect (230, 20, 100, 100), roboticon)) {
				
		}
		GUI.EndGroup ();
	}
}