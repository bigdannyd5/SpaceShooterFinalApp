using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour 
{
	GameObject GameManagerGO; // refrence to our game manager
	GameObject scoreUITextGO; // reference to the text UI game object
	public GameObject ExplosionGO; // explosion prefab

	public float speed;
	private int facing;

	int life; // current enemy hp

	// Use this for initialization
	void Start () 
	{
		life = 75;
		facing = 0;
		scoreUITextGO = GameObject.FindGameObjectWithTag ("ScoreTextTag");
		GameManagerGO = GameObject.FindGameObjectWithTag ("GameManagerTag");
	}

	// Update is called once per frame
	void Update () 
	{
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0)); // bottom-left point of screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1)); // top-right point of screen

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
		// collision of enemy ship with player ship, or player bullet. 
		if (other.tag == "PlayerBulletTag")
		{
			life--;
			if (life <= 0)
			{
				PlayExplosion ();

				scoreUITextGO.GetComponent<GameScore>().Score += 10000;

				// Change game manager state to game over state
				GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.EndLevel);

				
				Destroy (gameObject); // Destroy boss's ship
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
