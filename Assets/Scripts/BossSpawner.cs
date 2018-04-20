using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour 
{

	public GameObject BossGO; 

	private Component[] guns;
    public float fireRateStart = 1.0f;
    public float fireRateRange = 1.0f;

	// Spawn an enemy.
	public void SpawnEnemy()
	{
		// Top-right point of screen.
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		// Instantiate an enemy.
		GameObject aBoss = (GameObject)Instantiate(BossGO);

		aBoss.transform.position = new Vector2 (0, max.y-1);
		guns = aBoss.GetComponentsInChildren<BossGun>();

        // We want to change this to a better randomizer. 
        foreach (BossGun gun in guns)
        {
            gun.fireRateRange = fireRateRange;
            gun.fireRateStart = fireRateStart;
        }
	}

	public void DestroyBoss()
	{
		// If there's a boss destroy it.
		if (GameObject.FindGameObjectWithTag("BossShipTag") != null) 
		{
			Destroy (GameObject.FindGameObjectWithTag("BossShipTag"));
		}
	}
}
