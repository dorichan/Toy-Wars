using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public GameObject robotPrefab;
	public GameObject floaterPrefab;
	public Transform redSpawnPoint;
	public Transform blueSpawnPoint;

	public int redscore;
	public int bluescore;
	public int numRobots;
	public int numFloaters;
	private int counter;
	private int maxRobots;
	private int maxFloaters;

	public bool newGame;
	private bool isSpawn;
	private bool isOver;
	
	public GUITexture redWinTex;
	public GUITexture blueWinTex;
	public AudioSource winGame;
	public AudioSource battleGame;
	public AudioSource[] aSource;
	private UserInterface ui;

	public GameObject laserPrefab;
	public GameObject[] laserCache;
	private int activeObj;
	private int maxLaser;

	void Awake()
	{
		ui = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UserInterface>();
		aSource = GetComponents<AudioSource>();
	}
	
	void Start()
	{
		redWinTex.enabled = false;
		blueWinTex.enabled = false;

		redscore = 0;
		bluescore = 0;
		counter = 0;
		maxRobots = 5;
		maxFloaters = 5;
		numRobots = 0;
		numFloaters = 0;

		isSpawn = false;
		isOver = false;
		newGame = false;

		winGame = aSource[0];
		battleGame = aSource[1];

		battleGame.Play ();

		activeObj = 0;
		maxLaser = 50;
		
		laserCache = new GameObject[maxLaser];
		
		for (int i = 0; i < laserCache.Length; i++) {
			laserCache[i] = Instantiate(laserPrefab, transform.position, transform.rotation) as GameObject;
			laserCache[i].SetActive (false);
		}
	}

	void Update()
	{
		counter += 1;
		if(counter > 60) {
			isSpawn = true;
			counter = 0;
		}
		else {
			isSpawn = false;
		}

		if(isSpawn == true && numRobots < maxRobots) {
			GameObject robots = Instantiate(robotPrefab, redSpawnPoint.position, redSpawnPoint.rotation) as GameObject;
			numRobots += 1;
		}

		if(isSpawn == true && numFloaters < maxFloaters) {
			GameObject floaters = Instantiate(floaterPrefab, blueSpawnPoint.position, blueSpawnPoint.rotation) as GameObject;
			numFloaters += 1;
		}

		if(numRobots >= maxRobots) {
			ui.gameInProgress = true;
		}

		if(Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("main_menu");
			Time.timeScale = 1;
		}
	}

	void FixedUpdate()
	{
		if(bluescore >= 2000) {
			blueWinTex.enabled = true;
			Time.timeScale = 0;
			battleGame.Stop ();
			isOver = true;
		}
		
		if(redscore >= 2000) {
			redWinTex.enabled = true;
			Time.timeScale = 0;
			battleGame.Stop ();
			isOver = true;
		}
		
		if(isOver) {
			winGame.Play ();
		}

		if(newGame) {
			battleGame.Play ();
			Time.timeScale = 1;
		}
	}

	public void Shoot(Vector3 _target)
	{
		laserCache [0].GetComponent<LaserBehavior>().isShooting = true;
		laserCache [0].SetActive (true);
		activeObj += 1;
		
		if (activeObj > maxLaser) {
			activeObj = 0;
		}
	}

	public void AddRedScore (int newScoreValue)
	{
		if (redscore <= 2000) {
			redscore += newScoreValue;
		}
	}
	
	public void AddBlueScore (int newScoreValue)
	{
		if (bluescore <= 2000) {
			bluescore += newScoreValue;
		}
	}
}