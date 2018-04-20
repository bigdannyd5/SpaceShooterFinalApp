using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour 
{
	// Reference to our enemy spawner.
	public GameObject enemySpawner;
	// Reference to our meteor spawner.
	public GameObject meteorSpawner; 

	// Reference to the boss spawner
	public GameObject BossSpawnerGO;
	float spawnTimer = 60f;
	// This will be reset on GameManger scrip during gameover, incase boss was there.
	public int bossNumber = 0;

	// Reference to the time counter UI Text.
	Text timeUI;

	// The time when the user clicks on play.
	float startTime; 
	// Ellapsed time after the user clicks on play.
	float ellapsedTime;
	// Flag to start the counter.
	bool startCounter;

	int minutes;
	int seconds;

	// Use this for initialization.
	void Start () 
	{
		startCounter = false;

		// Get the Text UI component from this gameObject.
		timeUI = GetComponent<Text>();
	}

	// Start the time counter.
	public void StartTimeCounter()
	{
		startTime = Time.time;
		startCounter = true;
	}

	// Stop the time counter.
	public void StopTimeCounter()
	{
		startCounter = false;
	}
	
	// Update is called once per frame.
	void Update () 
	{
		if (startCounter) 
		{
			// Compute the ellapsed time.
			ellapsedTime = Time.time - startTime;

			// Compute minutes.
			minutes = (int)ellapsedTime / 60; 
			// Computer seconds.
			seconds = (int)ellapsedTime % 60; 

			// Update the time counter UI Text.
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

			// This will stop the both the enemy spawner and meteor spawner.
			enemySpawner.GetComponent<EnemySpawner>().UnsheduleEnemySpawner();
			meteorSpawner.GetComponent<MeteorSpawner>().UnsheduleEnemySpawner();
		}
			
	}
}
