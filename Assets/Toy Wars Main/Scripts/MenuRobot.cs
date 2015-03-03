using UnityEngine;
using System.Collections;

public class MenuRobot : MonoBehaviour 
{
	void Update () 
	{
		transform.RotateAround (transform.position, transform.up, (Random.Range (-5.0f, 50.0f)) * Time.deltaTime);
	}
}