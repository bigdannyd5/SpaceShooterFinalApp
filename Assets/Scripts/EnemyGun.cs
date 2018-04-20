using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour 
{
	public GameObject EnemyBulletGO; // our enemy bullet
    public GameManager Manager;
    public float enemyBulletSpeed = 2.0f;
    private float timeOffset = 0.5f;
    private int shotsRemaining = 1;

	// Use this for initialization
	void Start () 
	{
		Invoke ("FireEnemyBullet", 1f);
        Manager = FindObjectOfType<GameManager>();
        timeOffset = Time.time + timeOffset;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ((Time.time > timeOffset) && (shotsRemaining > 0) && (Manager.GetComponent <GameManager>().level == 4))
        {
            Invoke("FireEnemyBullet", 1.5f);
            shotsRemaining--;
        }
	}

	// fire enemy bullet
	void FireEnemyBullet()
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
			Vector2 direction = playerShip.transform.position - bullet.transform.position;
            if (Manager.GetComponent<GameManager>().level == 1)
            {
                bullet.GetComponent<EnemyBullet>().speed = enemyBulletSpeed;
                direction = new Vector2(0.0f, -1.0f);
            }
            

			// set the bullet's direction
			bullet.GetComponent<EnemyBullet> ().SetDirection (direction);
		}
	}
}
