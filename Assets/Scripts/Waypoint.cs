using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour 
{
	private float radius = 0.5f;
	public GameObject waypoint;

	void OnDrawGizmos()
	{
		Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 0.5f);
		Gizmos.DrawSphere(transform.position, radius);
		if (waypoint != null)
		{
			Gizmos.DrawLine (transform.position, waypoint.transform.position);
		}
	}
	
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, radius);
		if(waypoint != null)
		{
			Gizmos.DrawLine(transform.position, waypoint.transform.position);
		}
	}
}
