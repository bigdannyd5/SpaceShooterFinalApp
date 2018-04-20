using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour 
{
	// Refrence to our game manager.
	GameObject GameManagerGO; 

	// Reference to the text UI game object.
	GameObject scoreUITextGO; 

	// Explosion prefab.
	public GameObject ExplosionGO; 

	public float speed;
	private int facing;

	// Current enemy hp.
	int life; 

	// Use this for initialization.
	void Start () 
	{
		life = 75;
		facing = 0;
		scoreUITextGO = GameObject.FindGameObjectWithTag ("ScoreTextTag");
		GameManagerGO = GameObject.FindGameObjectWithTag ("GameManagerTag");
	}

	// Update is called once per frame.
	void Update () 
	{
		// Bottom-left point of screen.
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0)); 

		// Top-right point of screen.
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1)); 

		// 0 = facing right, 1 = facing left. 
		if (facing == 1 && min.x+1 > gameObject.transform.position.x)
			facing = 0;

		if (facing == 0 && max.x-1 < gameObject.transform.position.x)
			facing = 1;

		if (facing == 0)
			transform.Translate (Vector2.right * speed * Time.deltaTime);
		else if (facing == 1)
			transform.Translate (-Vector2.right * speed * Time.deltaTime);

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// Collision of enemy ship with player ship, or player bullet. 
		if (other.tag == "PlayerBulletTag")
		{
			life--;
			if (life <= 0)
			{
				PlayExplosion ();

				scoreUITextGO.GetComponent<GameScore>().Score += 10000;

				// Change game manager state to game over state.
				GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.EndLevel);

				// Destroy boss's ship.
				Destroy (gameObject); 
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
