using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : MonoBehaviour 
{
	public GameObject EnemyBulletGO; // our enemy bullet
    public float fireRateRange = 1.0f;
    public float fireRateStart = 1.0f;
	private float nextFire = 0.0f;
    public GameManager Manager;

	// Use this for initialization.
	void Start () 
	{
        
        Manager = FindObjectOfType<GameManager>();
	}

	// Update is called once per frame.
	void Update ()
	{
		if (Time.time > nextFire) 
		{
            // Fire next at a random value within the range.
			nextFire = Time.time + (fireRateStart + Random.value * fireRateRange);
			FireBossBullet ();
		}
	}

	// Fire enemy bullet.
	void FireBossBullet()
	{
		// Get reference to player's ship.
		GameObject playerShip = GameObject.Find("PlayerGO");

		// If player isn't dead.
		if (playerShip != null) 
		{
			// Instantiate enemy bullet.
			GameObject bullet = (GameObject)Instantiate (EnemyBulletGO);

			// Bullet's initial position.
			bullet.transform.position = transform.position;

			// Compute the bullet's direction towards the player's ship.
			// This way the bullets only go straight down.
			Vector2 direction = new Vector2(0,-1);  
            if ((Manager.GetComponent <GameManager>().level == 4) && (Random.value > 0.5f))
                direction = playerShip.transform.position - bullet.transform.position;

			// Set the bullet's direction.
			bullet.GetComponent<EnemyBullet> ().SetDirection (direction);
		}
	}
}
