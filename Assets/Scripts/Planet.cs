using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour 
{
	// Speed of planet.
	public float speed; 
	// Planet scroll down.
	public bool isMoving; 

	// Bottom-left screen.
	Vector2 min;
	// Top-right screen.
	Vector2 max; 

	void Awake()
	{
		isMoving = false;

		min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		// Add the planet sprite half height to max y.
		max.y = max.y + GetComponent<SpriteRenderer>().sprite.bounds.extents.y;

		// Subtract the planet sprite half height to min y.
		min.y = min.y - GetComponent<SpriteRenderer>().sprite.bounds.extents.y;

	}

	// Use this for initialization.
	void Start () 
	{
		
	}
	
	// Update is called once per frame.
	void Update () 
	{
		if (!isMoving)
			return;

		// Get the current position of the planet.
		Vector2 position = transform.position;

		// Compute planet's new position.
		position = new Vector2(position.x, position.y + speed * Time.deltaTime);

		// Update planet's position.
		transform.position = position;

		// Once planet gets to min y position, stop moving the planet.
		if (transform.position.y < min.y)
			isMoving = false;
	}

	// Reset planet's position.
	public void ResetPosition()
	{
		// Reset the position of the planet to random x, and max y.
		transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
	}
}
