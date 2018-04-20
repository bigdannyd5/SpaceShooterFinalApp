using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour 
{
	// Bullet speed.
	public float speed; 
	// Direction of bullet.
	Vector2 _direction; 
	// Once bullet direction is set.
	bool isReady;

	void Awake()
	{
		isReady = false;
	}

	// Use this for initialization.
	void Start () 
	{

	}

	// Set bullet's direction.
	public void SetDirection(Vector2 direction)
	{
		// Unit vector.
		_direction = direction.normalized;

		isReady = true;
	}
	
	// Update is called once per frame.
	void Update () 
	{
		if (isReady) 
		{
			// Bullet's current position.
			Vector2 position = transform.position;

			// Find bullets new position.
			position += _direction * speed * Time.deltaTime;

			// Update bullets position.
			transform.position = position;

			// Bottom-left point of screen.
			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

			// Top-right point of screen.
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

			// Bullet outside of screen gets destroyed.
			if ((transform.position.x < min.x) || (transform.position.x > max.x) ||
			    (transform.position.y < min.y) || (transform.position.y > max.y)) 
			{
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// Collision of enemy bullet with player ship.
		if (other.tag == "PlayerShipTag")
			// Destroy player's ship.
			Destroy (gameObject);
	}
}
