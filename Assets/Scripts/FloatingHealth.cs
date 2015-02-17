using UnityEngine;
using System.Collections;

public class FloatingHealth : MonoBehaviour {
			
	public Texture2D bar;
	public GUIStyle healthBar;
	public GUIStyle emptyBar;

	private float barDisplay;
	private Health health;
	
	void Start()
	{
		GameObject h = GameObject.FindWithTag ("Robot");
		health = h.GetComponent<Health> ();
	}

	void Update()
	{
		barDisplay = barDisplay * health.health;
	}
		
	void OnGUI()
	{
		Vector2 targetPos;
		targetPos = Camera.main.WorldToScreenPoint (transform.position);

		GUI.BeginGroup (new Rect (targetPos.x, Screen.height - targetPos.y + 3.0f, 50, 10), "");
	//		GUI.Box (new Rect (0, 0, 50, 10), emptyTex, emptyBar);
			GUI.BeginGroup (new Rect (0, 0, barDisplay * 2, 10));
	//		GUI.Box (new Rect (0, 0, 40, 10), fullTex, healthBar);
			GUI.EndGroup ();
			GUI.DrawTexture(new Rect(15, 3, 29, 4), bar);
		GUI.EndGroup ();
	}
}
