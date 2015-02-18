using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserInterface : MonoBehaviour
{
	private GameObject cursor;
	private Ray selectionRay;
	private Ray attackRay;
	public bool gameInProgress;
	public List<GameObject> rList = new List<GameObject>();
	public List<GameObject> bList = new List<GameObject>();
	public List<GameObject> selected = new List<GameObject>();

	void Awake()
	{
		cursor = GameObject.Find ("Cursor");
	}

	void Start ()
	{
		gameInProgress = false;
	}
	
	void Update ()
	{
		if (gameInProgress) {		
			if (Input.GetMouseButtonDown(0)) {
				RaycastHit hit = new RaycastHit ();
				selectionRay = camera.ScreenPointToRay (Input.mousePosition);
				cursor.GetComponent<CursorBehaviour>().isActive = true;

				if (Physics.Raycast (selectionRay, out hit)) {
					cursor.transform.position = hit.point;
					foreach (var r in rList) {
						r.GetComponent<ToyAI>().target = cursor.transform;
					}
				}
			}

			if (Input.GetMouseButtonDown(1)) {
				RaycastHit hit = new RaycastHit();
				attackRay = camera.ScreenPointToRay (Input.mousePosition);
					
				if (Physics.Raycast (attackRay, out hit)) {
					cursor.transform.position = hit.point;
					if (bList.Contains (hit.transform.gameObject)) {
						foreach (var r in rList) {
							r.GetComponent<ToyAI>().enemy = hit.transform.gameObject;
						}
					}
				}
			}
		}
	}

	public void SeeMe(GameObject _obj)
	{
		if(_obj.tag == "Blue") {
			bList.Add(_obj);
		}
		if(_obj.tag == "Red") {
			rList.Add(_obj);
		}
	}
}