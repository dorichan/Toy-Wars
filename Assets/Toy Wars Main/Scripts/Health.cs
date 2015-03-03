using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public float health;
	public bool isDead;

	private float maxHealth;
	private float damage;
	private GameObject gm;

	void Awake()
	{
		gm = GameObject.FindGameObjectWithTag("GameManager");
	}

 	void Start () 
	{
		maxHealth = 100.0f;
		health = maxHealth;
		damage = 10.0f;
		isDead = false;
	}

	public void OnDamage () 
	{
		health -= damage;
	}

	void Dead()
	{
		if(gameObject == GameObject.FindWithTag("Robot")) {
			gm.GetComponent<GameManager>().numRobots -= 1;
		}
		if(gameObject == GameObject.FindWithTag("Floater")) {
			gm.GetComponent<GameManager>().numFloaters -= 1;
		}
		Destroy (gameObject);
	}
}