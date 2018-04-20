using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour 
{
	public float speed; // speed of stars

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Get current position of star
		Vector2 position = transform.position;

		// compute the star's new position
		position = new Vector2(position.x, position.y + speed * Time.deltaTime);

		// update stars position
		transform.position = position;

		// bottom-left point of screen
		Vector2 min =  Camera.main.ViewportToWorldPoint(new Vector2(0,0));

		// top-right point of screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		// Once the star reaches the bottom of the screen reset its position
		// to a random spot on the top of the screen
		if (transform.position.y < min.y) 
		{
			transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
		}

	}
}
