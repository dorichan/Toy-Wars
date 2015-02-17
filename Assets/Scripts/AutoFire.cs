using UnityEngine;
using System.Collections;

public class AutoFire : MonoBehaviour 
{
	public GameObject bulletPrefab;
	public Transform spawnPoint;
	public float frequency = 5.0f;
	public float coneAngle = 1.5f;
	public float range = 1000.0f;
	public bool isFiring = false;

	float forcePerSecond = 20.0f;
	float hitSoundVolume = 0.5f;
	public GameObject muzzleFlashFront;
	private float lastFireTime = -1.0f;

	public AudioClip f_gunSound;
	public AudioClip ricochet;

	void Awake() 
	{
		muzzleFlashFront.SetActive (false);
		if (spawnPoint == null) {
			spawnPoint = transform;
		}
	}

	void Start()
	{
		muzzleFlashFront.SetActive (false);
	}

	void Update() 
	{
		if(isFiring) {
			if(Time.time > lastFireTime + 1 / frequency) {
				Quaternion coneRandomRotation = Quaternion.Euler (Random.Range (-coneAngle, coneAngle), Random.Range (-coneAngle, coneAngle), 0);
				GameObject go = Spawner.Spawn (bulletPrefab, spawnPoint.position, spawnPoint.rotation * coneRandomRotation) as GameObject;
				
				// Get the damage done by the missile	
				Bullet bullet = go.GetComponent<Bullet> ();
				lastFireTime = Time.time;

				// Find the object hit by the raycast
				RaycastHit hitInfo;	
				LayerMask takedamage= 1<<10; // only hit obstacles that can be damaged i.e. have health in layer 10

				if (Physics.Raycast(transform.position,transform.forward, out hitInfo, range, takedamage )){
					Debug.LogWarning("vehicle hit! Health may be effected " + hitInfo.transform.name);
					if (hitInfo.transform) {
						// Get the health component of the target if any
						Health targetHealth = hitInfo.transform.gameObject.GetComponent<Health> ();
						if (targetHealth) {
							// Apply damage
							Debug.LogWarning("vehicle damage! Health is now " + targetHealth);
							targetHealth.OnDamage ();
						}
						
						// Get the rigidbody if any
						if (hitInfo.rigidbody) {
							// Apply force to the target object at the position of the hit point
							Vector3 force  = transform.forward * (forcePerSecond / frequency);
							hitInfo.rigidbody.AddForceAtPosition (force, hitInfo.point, ForceMode.Impulse);
						}

						AudioSource.PlayClipAtPoint (ricochet, hitInfo.point, hitSoundVolume);					
						bullet.dist = hitInfo.distance;
					}
				} else {
					bullet.dist = 1000.0f;
				}
			}
		}
	}

	public void OnStartFire () 
	{
		if (Time.timeScale == 0)
			return;

		isFiring = true;
		muzzleFlashFront.SetActive(true);

		audio.Play();
	}
	
	public void OnStopFire () {
		isFiring = false;
		muzzleFlashFront.SetActive(false);

		audio.Stop();
	}
}