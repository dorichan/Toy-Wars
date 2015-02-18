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

	private int counter = 0;
	private int maxRobots = 5;
	public int numRobots = 0;
	private int maxFloaters = 5;
	public int numFloaters = 0;
	private bool isSpawn = false;
	private bool isOver = false;
	public bool newGame = false;

	public GUITexture redWinTex;
	public GUITexture blueWinTex;

	public AudioSource winGame;
	public AudioSource battleGame;

	private UserInterface ui;

	void Awake()
	{
		ui = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UserInterface>();
	}
	
	void Start()
	{
		redWinTex.enabled = false;
		blueWinTex.enabled = false;

		redscore = 0;
		bluescore = 0;

		AudioSource[] aSource = GetComponents<AudioSource>();
		winGame = aSource[0];
		battleGame = aSource[1];

//		battleGame.Play ();
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
			Spawner.Spawn (robotPrefab, redSpawnPoint.position, redSpawnPoint.rotation);
			numRobots += 1;
		}

		if(isSpawn == true && numFloaters < maxFloaters) {
			Spawner.Spawn (floaterPrefab, blueSpawnPoint.position, blueSpawnPoint.rotation);
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
//			battleGame.Stop ();
			isOver = true;
		}
		
		if(redscore >= 2000) {
			redWinTex.enabled = true;
			Time.timeScale = 0;
//			battleGame.Stop ();
			isOver = true;
		}
		
		if(isOver) {
//			winGame.Play ();
		}

		if(newGame) {
//			battleGame.Play ();
			Time.timeScale = 1;
		}
	}

	public void AddRedScore (int newScoreValue)
	{
		redscore += newScoreValue;
	}
	
	public void AddBlueScore (int newScoreValue)
	{
		bluescore += newScoreValue;
	}
}