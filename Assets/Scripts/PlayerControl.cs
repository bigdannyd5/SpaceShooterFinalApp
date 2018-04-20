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
	public GameObject GameManagerGO; // refrence to our game manager
	public GameObject PlayerBulletGO; // this is the player's bullet prefab
	public GameObject bulletPosition01;
	public GameObject bulletPosition02;
	public GameObject ExplosionGO; // explosion prefab
    public GameObject UserC; // refrence the users information

    User currentUser;

	public float fireRate = 0.5f; // This is a public, so you con modify it on unity 
	private float nextFire = 0.0f;

	// Reference to the lives ui text
	public Text LivesUIText;

    // LoginScript.savedUsers.Find(user => user.getName() == enterUsername.text);

    

    int MaxLives = 3; // maximum player lives

	int lives; // current player lives

	public float speed;

	// added for controls GUI
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

		// update the lives UI text
		LivesUIText.text = lives.ToString();

		// reset the player position to the center of the screen
		transform.position = new Vector2(0,-2.3f); 

		// set player object to active
		gameObject.SetActive(true);
	}

	// Use this for initialization
	void Start () 
	{
		// I think we can put the fire rate here, from store setup.
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

		// fire bullets when the spacebar is pressed
		//if (Input.GetButtonDown("ButtonShoot"))
		if (Input.GetKeyDown ("space") && Time.time > nextFire * currentUser.getFireMultiplier()) 
		{
			nextFire = Time.time + fireRate;

			// laser sound effects
			//audio.Play(); // Doesn't work 
			gameObject.GetComponent<AudioSource>().Play();

			GameObject bullet01 = (GameObject)Instantiate (PlayerBulletGO);
			bullet01.transform.position = bulletPosition01.transform.position; // bullet initial position

			GameObject bullet02 = (GameObject)Instantiate (PlayerBulletGO);
			bullet02.transform.position = bulletPosition02.transform.position;
		}

		/*
		if (Input.GetButton ("ButtonShoot")) 
		{
			gameObject.GetComponent<AudioSource>().Play();

			GameObject bullet01 = (GameObject)Instantiate (PlayerBulletGO);
			bullet01.transform.position = bulletPosition01.transform.position; // bullet initial position

			GameObject bullet02 = (GameObject)Instantiate (PlayerBulletGO);
			bullet02.transform.position = bulletPosition02.transform.position;
		}
		*/


        
		// This is for keyboard use, disable to allow arrows only
	/*	if (Input.GetKey (KeyCode.LeftArrow))
			moveLeft ();
		else if (Input.GetKey (KeyCode.RightArrow))
			moveRight ();
        else
        	Left = Right = false;
    */
        print(currentUser.getMoveMultiplier());

        // print(Left);

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

			//rb.position = new Vector2 (Mathf.Clamp (rb.position.x, min.x, max.x), 0.0f);
		}
			
	}

	public void Shoot()
	{
		if (Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;

			// laser sound effects
			//audio.Play(); // Doesn't work 
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

		//rb.velocity = new Vector2 (speed, rb.velocity.y);
	}

	public void moveLeft()
	{
		Right = false;
		Left = true;

		//rb.velocity = new Vector2 (-speed, rb.velocity.y);
	}

	public void releaseLeft()
	{
		Left = false;
	}

	public void releaseRight()
	{
		Right = false;
	}

	// This was for moving in every direction
	/*
	void Move (Vector2 direction)
	{

		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1)); 

		max.x = max.x - 0.225f;
		min.x = min.x + 0.225f;

		max.y = max.y - 0.285f;
		min.y = min.y + 0.285f;

		Vector2 pos = transform.position;

		pos += direction * speed * Time.deltaTime;

		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);

		transform.position = pos;
	}
	*/

	void OnTriggerEnter2D(Collider2D other)
	{
		// collision of player ship with enemy ship, or enemy bullet. 
		if ((other.tag == "EnemyShipTag") || (other.tag == "EnemyBulletTag"))
		{
			//PlayExplosion ();

			lives--;
			LivesUIText.text = lives.ToString (); // update lives UI text

			if (lives == 0)
			{
				PlayExplosion ();
				// Change game manager state to game over state
				GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);

				//Destroy (gameObject); // Destroy player's ship
				// instead of destroying hide the player's ship
				gameObject.SetActive(false);
			}
		}
	}

	// instantiate explosion
	void PlayExplosion()
	{
		GameObject explosion = (GameObject)Instantiate (ExplosionGO);

		// position of explosion
		explosion.transform.position = transform.position;
	}
}
