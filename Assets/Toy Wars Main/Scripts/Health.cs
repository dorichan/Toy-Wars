using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public ParticleSystem explosion;
	public float health;
	public bool isDead;

	private float maxHealth;
	private float damage;
	private GameObject gm;

	void Awake()
	{
		gm = GameObject.FindGameObjectWithTag("GameManager");
		explosion = transform.FindChild ("Explosion").gameObject.GetComponent<ParticleSystem> ();
	}

 	void Start () 
	{
		maxHealth = 250.0f;
		health = maxHealth;
		damage = 0.5f;
		isDead = false;
	}

	void Update()
	{
		if (health <= 0.0f) {
			Dead ();
		}
	}
	
	public void OnDamage () 
	{
		explosion.Play ();
		health -= damage;
	}

	void Dead()
	{
		transform.rigidbody.velocity += transform.up * 5;

		if(gameObject == GameObject.FindWithTag("Red")) {
			gm.GetComponent<GameManager>().numRobots -= 1;
		}
		if(gameObject == GameObject.FindWithTag("Blue")) {
			gm.GetComponent<GameManager>().numFloaters -= 1;
		}
		Destroy (gameObject);
	}
}