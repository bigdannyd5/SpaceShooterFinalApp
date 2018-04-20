using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour 
{
	// StarGo prefab.
	public GameObject StarGO; 
	// Maximum number of stars.
	public int MaxStars;

	// Array of colors.
	Color[] starColors = {
		// Blue.
		new Color (0.5f, 0.5f, 1f), 
		// Green.
		new Color(0, 1f, 1f), 
		// Yellow.
		new Color(1f, 1f, 0),
		// Red.
		new Color (1f, 0, 0),
	};

	// Use this for initialization.
	void Start () 
	{
		// Bottom-left screen.
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		// Top-right screen.
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		// Loop to create the stars.
		for (int i = 0; i < MaxStars; i++) 
		{
			GameObject star = (GameObject)Instantiate (StarGO);

			// Set the star color.
			star.GetComponent<SpriteRenderer>().color = starColors[i % starColors.Length];

			// Set the position of the star (random x and random y).
			star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, min.y));

			// Random speed for the stars.
			star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);

			// Make the star a child of the StarGeneratorGO.
			star.transform.parent = transform;
		}
	}
}
