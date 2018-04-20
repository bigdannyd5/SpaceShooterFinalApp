using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour 
{

	public GameObject enemySpawner; // reference to our enemy spawner
	public GameObject meteorSpawner; // reference to our meteor spawner

	//public GameObject BossGO; // reference to my boss game object
	public GameObject BossSpawnerGO; // reference to the boss spawner
	float spawnTimer = 60f;
	public int bossNumber = 0;// This will be reset on GameManger scrip during gameover, incase boss was there

	Text timeUI; // reference to the time counter UI Text

	float startTime; // the time when the user clicks on play
	float ellapsedTime; // ellapsed time after the user clicks on play
	bool startCounter; // flag to start the counter

	int minutes;
	int seconds;

	// Use this for initialization
	void Start () 
	{
		startCounter = false;

		// get the Text UI component from this gameObject
		timeUI = GetComponent<Text>();
	}

	// start the time counter
	public void StartTimeCounter()
	{
		startTime = Time.time;
		startCounter = true;
	}

	// stop the time counter
	public void StopTimeCounter()
	{
		startCounter = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (startCounter) 
		{
			// compute the ellapsed time
			ellapsedTime = Time.time - startTime;

			minutes = (int)ellapsedTime / 60; // compute minutes
			seconds = (int)ellapsedTime % 60; // computer seconds

			// update the time counter UI Text
			timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);

			if (ellapsedTime >= spawnTimer && bossNumber == 0) 
				Spawner ();
		}

	}

	void Spawner()
	{
		if (bossNumber == 0) 
		{
			BossSpawnerGO.GetComponent<BossSpawner>().SpawnEnemy();
			bossNumber++;

			// This will stop the both the enemy spawner and meteor spawner
			enemySpawner.GetComponent<EnemySpawner>().UnsheduleEnemySpawner();
			meteorSpawner.GetComponent<MeteorSpawner>().UnsheduleEnemySpawner();
		}
			
	}
}
