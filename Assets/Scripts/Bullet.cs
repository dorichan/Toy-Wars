using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public GameObject bulletHit;
	public float speed = 15.0f;
	public float lifeTime = 0.75f;
	public float dist = 1000.0f;
	public int bulletDmg = 10;

	private float spawnTime = 0.0f;
	private Transform tr;

	public int getDmg()
	{
		return bulletDmg;
	}

	void OnEnable()
	{
		tr = transform;
		spawnTime = Time.time;
	}

	void Update()
	{
		tr.position += tr.forward * speed * Time.deltaTime;
		dist -= speed * Time.deltaTime;

		if(Time.time > spawnTime + lifeTime || dist < 0)
		{
			Spawner.Destroy (gameObject);
		}
	}
}