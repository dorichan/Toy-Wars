using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour 
{
	void OnTriggerStay(Collider other)
	{
		AIBehaviour ai = GetComponent<AIBehaviour>();

		if(ai.currentState == AIBehaviour.State.Patrol && other.gameObject.tag == "Robot") {
			ai.currentState = AIBehaviour.State.Track;
		}
		if(ai.currentState == AIBehaviour.State.Track && other.gameObject.tag == "Robot") {
			ai.Attack();
		}
		else 
		{
			ai.cease.OnStopFire();
		}
	}
}
