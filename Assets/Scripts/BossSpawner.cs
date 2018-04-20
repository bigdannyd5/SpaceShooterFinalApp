using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour 
{

	public GameObject BossGO; 

	private Component[] guns;
    public float fireRateStart = 1.0f;
    public float fireRateRange = 1.0f;

	// Spawn an enemy
	public void SpawnEnemy()
	{
		// bottom-left point of screen
		//Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

		// top-right point of screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		// instantiate an enemy
		GameObject aBoss = (GameObject)Instantiate(BossGO);

		aBoss.transform.position = new Vector2 (0, max.y-1); // I'm not sure if this will work;
		//aBoss.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);

		guns = aBoss.GetComponentsInChildren<BossGun>();

        // we want to change this to a better randomizer 
        foreach (BossGun gun in guns)
        {
            gun.fireRateRange = fireRateRange;
            gun.fireRateStart = fireRateStart;
        }
	}

	public void DestroyBoss()
	{
		// if there's a boss destroy it
		if (GameObject.FindGameObjectWithTag("BossShipTag") != null) 
		{
			//GameObject cloneBoss = Instantiate (BossGO);
			Destroy (GameObject.FindGameObjectWithTag("BossShipTag"));
		}
	}
}
