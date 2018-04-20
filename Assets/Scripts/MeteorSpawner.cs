using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour 
{
	public GameObject MeteorGo;

	float maxSpawnRateInSeconds = 5f;
	public float spawnRate = 20;

	// Spawn an meteor
	void SpawnMeteor()
	{
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		GameObject anEnemy = (GameObject)Instantiate(MeteorGo);
		anEnemy.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);

		// Schedule when to spawn next meteor
		ScheduleNextMeteorSpawn();
	}

	void ScheduleNextMeteorSpawn()
	{
		float spawnInNSeconds;

		if (maxSpawnRateInSeconds > 1f) {
			spawnInNSeconds = Random.Range (1f, maxSpawnRateInSeconds);
		} else
			spawnInNSeconds = 1f;

		Invoke ("SpawnMeteor", spawnInNSeconds);
	}

	// increase dificulty of game
	void IncreaseSpawnRate()
	{
		if (maxSpawnRateInSeconds > 1f)
			maxSpawnRateInSeconds--;

		if (maxSpawnRateInSeconds == 1f)
			CancelInvoke ("IncreaseSpawnRate");
	}

	// start enemy spawner
	public void ScheduleMeteorSpawner()
	{
		// reset max spawn rate
		maxSpawnRateInSeconds = 5f;

		Invoke ("SpawnMeteor", maxSpawnRateInSeconds);

		// increase spawn rate every 20 seconds
		InvokeRepeating ("IncreaseSpawnRate", 0f, spawnRate);
	}

	// stop enemy spawner
	public void UnsheduleEnemySpawner()
	{
		CancelInvoke ("SpawnMeteor");
		CancelInvoke ("IncreaseSpawnRate");
	}
}
