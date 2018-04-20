using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;


public class LoadAndSave : MonoBehaviour {

	// Used for setting font sizes.
	GUIStyle smallFont;
	GUIStyle largeFont;

	// Parent window to contian all components.
	private Rect windowRect;

	// List of users
	public static List<User> savedUsers;

	private string usernameString = string.Empty;
	private string profileString = string.Empty;

	//it's static so we can call it from anywhere
	public static void Register(User user) 
	{
		LoadAndSave.savedUsers.Add(user);
		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
		bf.Serialize(file, LoadAndSave.savedUsers);
		file.Close();
	}   

	public static void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
			LoadAndSave.savedUsers = (List<User>)bf.Deserialize(file);
			file.Close();
		}
	}
		
	void Start ()
	{
		// Create overall window
		windowRect = new Rect (0, 0, Screen.width, Screen.height);

		// Create list of users that will be populated. 
		savedUsers = new List<User> ();

		// Set fontsizes depending on the screen size of the device. 
		smallFont = new GUIStyle ();
		largeFont = new GUIStyle ();
		smallFont.fontSize = Screen.height / 30;
		largeFont.fontSize = Screen.height / 20;
	}

	void OnGUI(){
		GUI.Window (0, windowRect, windowFunction, "Login");
		GUI.skin.textField.fontSize = Screen.height / 20;
		GUI.skin.button.fontSize = Screen.height / 35;
	}
		
	void windowFunction(int Window) 
	{
		// Space Shooter Label.
		GUI.Label (new Rect (Screen.width / 4, 20 * Screen.height / 100, Screen.width, Screen.height / 8), "Space Shooter!!!", largeFont);

		// Username prompt and textbox.
		GUI.Label (new Rect (32 * Screen.width / 100, 30 * Screen.height / 100, Screen.width, Screen.height / 8),
			"Enter Username", smallFont);
		usernameString = GUI.TextField(new Rect(Screen.width/ 4, 35 * Screen.height / 100, Screen.width / 2, 
			Screen.height / 10), usernameString, 10);

		// Testing Purpose textarea.
		profileString = GUI.TextArea (new Rect (Screen.width / 4, 4 * Screen.height / 5, Screen.width / 2, Screen.height / 10), profileString);

		// If pressed the "Register" button.
		if(GUI.Button(new Rect(20 * Screen.width / 100, 65 * Screen.height /100, 3 * Screen.width / 5, Screen.height / 10), "Register as New Player")){

			// Load currently registered users.
			Load();

			// Attempt to find a user with the same name from currently regisetered users.
			User player = LoadAndSave.savedUsers.Find(user => user.getName() == usernameString);

			// If a valid username is entered, and a user with the same name is not found, create new user.
			if (usernameString != "" && player == null) {
				User user = new User (usernameString);
				Register (user);
				profileString = "Registered New User: " +  usernameString;	
			} 
			// If a user with same username is entered, indicate.
			else {
				profileString = "Could not register: " +  usernameString;
			}
		}

		// If pressed the "Log-in" button.
		if(GUI.Button(new Rect(37 * Screen.width / 100, 50 * Screen.height / 100, Screen.width / 4, Screen.height / 10), "Log-in")){

			// Load currently registered users.
			Load();
			// Attempt to find a user by the username entered.
			User player = LoadAndSave.savedUsers.Find(user => user.getName() == usernameString);

			// If player was found, set user.
			if (player != null) 
			{
				GameObject.FindObjectOfType<CurrentUser>().currentUser = player;
				//profileString = "Found " +  currentUser.currentUser.getName() + "\nLevel: " + currentUser.currentUser.getLevel() +
				//	"\nCurrency: " + currentUser.currentUser.getCurrency() + "\nScore: " + currentUser.currentUser.getScore();	
				//profileString = "Found " +  player.getName() + 
			//		"\nLevel: " + GameObject.FindObjectOfType<CurrentUser>().currentUser.getLevel() +
		//			"\nCurrency: " + GameObject.FindObjectOfType<CurrentUser>().currentUser.getCurrency() +
	//				"\nScore: " + GameObject.FindObjectOfType<CurrentUser>().currentUser.getScore();	
				//SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
			}

			// If a valid player is not found, indicate. 
			else 
				profileString = "Could not find " +  usernameString;
				
		}
	}
}