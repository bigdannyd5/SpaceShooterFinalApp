using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour 
{
	public float speed; // bullet speed
	Vector2 _direction; // direction of bullet
	bool isReady; // once bullet direction is set

	void Awake()
	{
		isReady = false;
	}

	// Use this for initialization
	void Start () 
	{

	}

	// set bullet's direction
	public void SetDirection(Vector2 direction)
	{
		// unit vector
		_direction = direction.normalized;

		isReady = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isReady) 
		{
			// bullet's current position
			Vector2 position = transform.position;

			// find bullets new position
			position += _direction * speed * Time.deltaTime;

			// update bullets position
			transform.position = position;

			// bottom-left point of screen
			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

			// top-right point of screen
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

			// bullet outside of screen gets destroyed
			if ((transform.position.x < min.x) || (transform.position.x > max.x) ||
			    (transform.position.y < min.y) || (transform.position.y > max.y)) 
			{
				Destroy (gameObject);
			}
				
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// collision of enemy bullet with player ship.
		if (other.tag == "PlayerShipTag")
			Destroy (gameObject); // Destroy player's ship
	}
}
