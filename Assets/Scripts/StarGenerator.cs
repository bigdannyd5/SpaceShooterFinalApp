using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour 
{
	public GameObject StarGO; // StarGo prefab
	public int MaxStars; // maximum number of stars

	// Array of colors
	Color[] starColors = {
		new Color (0.5f, 0.5f, 1f), // blue
		new Color(0, 1f, 1f), // green
		new Color(1f, 1f, 0), // yellow
		new Color (1f, 0, 0), // red
	};

	// Use this for initialization
	void Start () 
	{
		// bottom-left screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		// top-right screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		// loop to create the stars
		for (int i = 0; i < MaxStars; i++) 
		{
			GameObject star = (GameObject)Instantiate (StarGO);

			// set the star color
			star.GetComponent<SpriteRenderer>().color = starColors[i % starColors.Length];

			// set the position of the star (random x and random y)
			star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, min.y));

			// random speed for the stars
			star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);

			// make the star a child of the StarGeneratorGO
			star.transform.parent = transform;
		}
	}
}
