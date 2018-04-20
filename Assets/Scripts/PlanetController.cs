using System.Collections;
using System.Collections.Generic; // for queue 
using UnityEngine;

public class PlanetController : MonoBehaviour 
{
	public GameObject[] Planets; // an array of PlanetGO prefabs

	// Queue to hold the planets
	Queue<GameObject> availablePlanets = new Queue<GameObject>();

	// Use this for initialization
	void Start () 
	{
		// add the planets to the Queue 
		for (int i = 0; i < Planets.Length; i++)
			availablePlanets.Enqueue (Planets [i]);

		// Call the MovePlanetDown function every 20 seconds
		InvokeRepeating("MovePlanetDown", 0, 20f);
	}
	
	// Update is called once per frame
	void Update () 

	{
		
	}

	// dequeue planet, and set its isMoving flag to true so that
	// the planet starts scrolling down the screen
	void MovePlanetDown()
	{
		EnqueuePlanets ();

		if (availablePlanets.Count == 0)
			return;

		// get a planet from the queue
		GameObject aPlanet = availablePlanets.Dequeue();

		// set isMoving flag to true
		aPlanet.GetComponent<Planet>().isMoving = true;
	}

	// Enqueue planets that are below the screen and are not moving
	void EnqueuePlanets()
	{
		foreach (GameObject aPlanet in Planets) 
		{
			// if planet below screen, and is not moving
			if ((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent<Planet> ().isMoving)) 
			{
				// reset planet position
				aPlanet.GetComponent<Planet>().ResetPosition();

				// Enqueue planet
				availablePlanets.Enqueue(aPlanet);
			}
		}
	}
}
