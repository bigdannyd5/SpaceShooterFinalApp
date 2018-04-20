using System.Collections;
// For queue.
using System.Collections.Generic; 
using UnityEngine;

public class PlanetController : MonoBehaviour 
{
	// An array of PlanetGO prefabs.
	public GameObject[] Planets; 

	// Queue to hold the planets.
	Queue<GameObject> availablePlanets = new Queue<GameObject>();

	// Use this for initialization.
	void Start () 
	{
		// Add the planets to the Queue .
		for (int i = 0; i < Planets.Length; i++)
			availablePlanets.Enqueue (Planets [i]);

		// Call the MovePlanetDown function every 20 seconds.
		InvokeRepeating("MovePlanetDown", 0, 20f);
	}
	
	// Update is called once per frame.
	void Update () 

	{
		
	}

	// Dequeue planet, and set its isMoving flag to true so that
	// the planet starts scrolling down the screen.
	void MovePlanetDown()
	{
		EnqueuePlanets ();

		if (availablePlanets.Count == 0)
			return;

		// Get a planet from the queue.
		GameObject aPlanet = availablePlanets.Dequeue();

		// Set isMoving flag to true.
		aPlanet.GetComponent<Planet>().isMoving = true;
	}

	// Enqueue planets that are below the screen and are not moving.
	void EnqueuePlanets()
	{
		foreach (GameObject aPlanet in Planets) 
		{
			// If planet below screen, and is not moving.
			if ((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent<Planet> ().isMoving)) 
			{
				// Reset planet position.
				aPlanet.GetComponent<Planet>().ResetPosition();

				// Enqueue planet.
				availablePlanets.Enqueue(aPlanet);
			}
		}
	}
}
