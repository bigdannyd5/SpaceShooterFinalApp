using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
	public GameObject EnemyGO;

    public float spawnFloor = 10f;
	public float maxSpawnRateInSeconds = 5f;
	public float spawnRate;
    public float enemyBulletSpeed = 2.0f;

	

	// Spawn an enemy.
	void SpawnEnemy()
	{
		// Bottom-left point of screen.
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

		// Top-right point of screen.
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		// Instantiate an enemy.
		GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
		anEnemy.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);

		// Schedule when to spawn next enemy.
		ScheduleNextEnemySpawn();
	}

	void ScheduleNextEnemySpawn()
	{
		float spawnInNSeconds;

		if (maxSpawnRateInSeconds > spawnFloor) {
			spawnInNSeconds = Random.Range (spawnFloor, maxSpawnRateInSeconds);
		} else
			spawnInNSeconds = spawnFloor;

		Invoke ("SpawnEnemy", spawnInNSeconds);
	}

	// Increase dificulty of game.
	void IncreaseSpawnRate()
	{
		if (maxSpawnRateInSeconds > spawnFloor)
			maxSpawnRateInSeconds--;

		if (maxSpawnRateInSeconds <= spawnFloor)
			CancelInvoke ("IncreaseSpawnRate");
	}

	// Start enemy spawner.
	public void ScheduleEnemySpawner()
	{
		// Reset max spawn rate.
		maxSpawnRateInSeconds = spawnRate;

		Invoke ("SpawnEnemy", maxSpawnRateInSeconds);

		// Increase spawn rate every 20 seconds.
		InvokeRepeating ("IncreaseSpawnRate", 0f, spawnRate);
	}

	// Stop enemy spawner.
	public void UnsheduleEnemySpawner()
	{
		CancelInvoke ("SpawnEnemy");
		CancelInvoke ("IncreaseSpawnRate");
        print("Unscheduled");
	}
}
