// http://answers.unity3d.com/questions/212189/camera-shake.html 
// Script by bgprocks on Unity Answers Forum

using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
	private bool Shaking;
	private float ShakeDecay;
	private float ShakeIntensity;
	private Vector3 OriginalPos;
	private Quaternion OriginalRot;
	
	void Start()
	{
		Shaking = false;    
	}

	void Update () 
	{
		if(ShakeIntensity > 0)
		{
			transform.position = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
			transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f);
			
			ShakeIntensity -= ShakeDecay;
		}
		else if (Shaking)
		{
			Shaking = false;    
		}
		DoShake ();
	}
	
	public void DoShake()
	{
		OriginalPos = transform.position;
		OriginalRot = transform.rotation;
		
		ShakeIntensity = 0.05f * Time.deltaTime;
		ShakeDecay = 0.2f * Time.deltaTime;
		Shaking = true;
	}
}