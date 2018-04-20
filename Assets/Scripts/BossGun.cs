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

	// Use this for initialization
	void Start () 
	{
        
        Manager = FindObjectOfType<GameManager>();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Time.time > nextFire) 
		{
            // fire next at a random value within the range
			nextFire = Time.time + (fireRateStart + Random.value * fireRateRange);
			FireBossBullet ();
		}
	}

	// fire enemy bullet
	void FireBossBullet()
	{
		// get reference to player's ship
		GameObject playerShip = GameObject.Find("PlayerGO");

		if (playerShip != null) // if player isn't dead
		{
			// instantiate enemy bullet
			GameObject bullet = (GameObject)Instantiate (EnemyBulletGO);

			// bullet's initial position
			bullet.transform.position = transform.position;

			// compute the bullet's direction towards the player's ship
			
			Vector2 direction = new Vector2(0,-1); // This way the bullets only go straight down. 
            if ((Manager.GetComponent <GameManager>().level == 4) && (Random.value > 0.5f))
                direction = playerShip.transform.position - bullet.transform.position;

			// set the bullet's direction
			bullet.GetComponent<EnemyBullet> ().SetDirection (direction);
		}
	}
}
