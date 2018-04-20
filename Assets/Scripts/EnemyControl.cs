using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour 
{
	// Reference to the text UI game object.
	GameObject scoreUITextGO; 
	// Explosion prefab.
	public GameObject ExplosionGO; 
	public float speed;
    public GameManager Manager;

	// Use this for initialization.
	void Start () 
	{
		scoreUITextGO = GameObject.FindGameObjectWithTag ("ScoreTextTag");
        Manager = FindObjectOfType<GameManager>();

        if (Manager.GetComponent<GameManager>().level == 1)
            speed = 0.75f;
    }

    // Update is called once per frame.
    void Update () 
	{
		// Get enemy current position.
		Vector2 position = transform.position;

		// New enemy position.
		position = new Vector2(position.x, position.y - speed * Time.deltaTime);

		transform.position = position;

		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

		// Destroy when outside of screen view.
		if (transform.position.y < min.y)
			Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// Collision of enemy ship with player ship, or player bullet. 
		if ((other.tag == "PlayerShipTag") || (other.tag == "PlayerBulletTag"))
		{
			PlayExplosion ();

			// Add 100 points to the score.
			scoreUITextGO.GetComponent<GameScore>().Score += 100;

			// Destroy player's ship.
			Destroy (gameObject); 
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
