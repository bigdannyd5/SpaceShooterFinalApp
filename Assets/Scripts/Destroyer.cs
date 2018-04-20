using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroys the game object, for the enemies, asteroids, bosses, and player ship.
public class Destroyer : MonoBehaviour 
{
	void DestroyGameObject()
	{
		Destroy (gameObject);
	}

}
