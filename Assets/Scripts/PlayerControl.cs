using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using UnityEngine.Audio;

public class PlayerControl : MonoBehaviour 
{
	// Refrence to our game manager.
	public GameObject GameManagerGO; 
	// This is the player's bullet prefab.
	public GameObject PlayerBulletGO; 
	public GameObject bulletPosition01;
	public GameObject bulletPosition02;
	// Explosion prefab.
	public GameObject ExplosionGO;
	// Refrence the users information.
    public GameObject UserC; 

    User currentUser;

	// This is a public, so you con modify it on unity.
	public float fireRate = 0.5f; 
	private float nextFire = 0.0f;

	// Reference to the lives ui text.
	public Text LivesUIText; 

	// Maximum player lives.
    int MaxLives = 3; 

	// Current player lives.
	int lives; 

	public float speed;

	// Added for controls GUI.
	public bool Left = false;
	public bool Right = false;
	public bool shoot = false;

	public Rigidbody2D rb;

	public void Init()
	{
        currentUser = FindObjectOfType<CurrentUser>().GetComponent<CurrentUser>().currentUser;

        int playerCurrency = currentUser.getCurrency();
        int playerMaxLives = currentUser.getMaxHealth();
        int fire = currentUser.getRateOfFire();
        int moveSpeed = currentUser.getSpeed();
        int hard = currentUser.getHard();

        MaxLives = playerMaxLives;


        lives = MaxLives;

		// Update the lives UI text.
		LivesUIText.text = lives.ToString();

		// Reset the player position to the center of the screen.
		transform.position = new Vector2(0,-2.3f); 

		// Set player object to active.
		gameObject.SetActive(true);
	}

	// Use this for initialization.
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame.
	void Update () 
	{

		// Fire bullets when the spacebar is pressed.
		if (Input.GetKeyDown ("space") && Time.time > nextFire * currentUser.getFireMultiplier()) 
		{
			nextFire = Time.time + fireRate;

			// Laser sound effects
			gameObject.GetComponent<AudioSource>().Play();

			GameObject bullet01 = (GameObject)Instantiate (PlayerBulletGO);
			// Bullet initial position.
			bullet01.transform.position = bulletPosition01.transform.position; 

			GameObject bullet02 = (GameObject)Instantiate (PlayerBulletGO);
			bullet02.transform.position = bulletPosition02.transform.position;
		}
			
        print(currentUser.getMoveMultiplier());


        if (Right) 
		{
			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1)); 

			max.x = max.x - 0.225f;
			min.x = min.x + 0.225f;

			Vector2 position = transform.position;
			position = new Vector2(Mathf.Clamp(position.x + speed * currentUser.getMoveMultiplier() * Time.deltaTime, min.x, max.x), position.y);
			transform.position = position;
		}

		if (Left) 
		{
            print(currentUser.getMoveMultiplier());
            print(currentUser.getFireMultiplier());
            print(currentUser.getCurrency());
            print(currentUser.getName());
            

			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1)); 

			max.x = max.x - 0.225f;
			min.x = min.x + 0.225f;

			Vector2 position = transform.position;
			position = new Vector2 (Mathf.Clamp(position.x - speed * currentUser.getMoveMultiplier() * Time.deltaTime, min.x, max.x), position.y);
			transform.position = position;
		}
			
	}

	public void Shoot()
	{
		if (Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;

			// Laser sound effects
			gameObject.GetComponent<AudioSource> ().Play ();

			GameObject bullet01 = (GameObject)Instantiate (PlayerBulletGO);
			bullet01.transform.position = bulletPosition01.transform.position; // bullet initial position

			GameObject bullet02 = (GameObject)Instantiate (PlayerBulletGO);
			bullet02.transform.position = bulletPosition02.transform.position;
		}
	}

	public void moveRight()
	{
		Right = true;
		Left = false;
	}

	public void moveLeft()
	{
		Right = false;
		Left = true;
	}

	public void releaseLeft()
	{
		Left = false;
	}

	public void releaseRight()
	{
		Right = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// Collision of player ship with enemy ship, or enemy bullet. 
		if ((other.tag == "EnemyShipTag") || (other.tag == "EnemyBulletTag"))
		{
			lives--;
			LivesUIText.text = lives.ToString (); // update lives UI text

			if (lives == 0)
			{
				PlayExplosion ();
				// Change game manager state to game over state.
				GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);

				// Instead of destroying hide the player's ship.
				gameObject.SetActive(false);
			}
		}
	}

	// Instantiate explosion.
	void PlayExplosion()
	{
		GameObject explosion = (GameObject)Instantiate (ExplosionGO);

		// Position of explosion.
		explosion.transform.position = transform.position;
	}
}
