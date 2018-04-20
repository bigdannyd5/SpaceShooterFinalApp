using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// this class is an instance of the User class, where we store the information of the 
// user that is playing currently
public class CurrentUser : MonoBehaviour {

	public User currentUser;

	// List of users
	public  List<User> savedUsers;

	public CurrentUser(User user)
	{
		this.currentUser = user;
	}

	void Awake() 
	{
		DontDestroyOnLoad (this.gameObject);
	}
		
	//it's static so we can call it from anywhere
	public void Save(User user) 
	{
		Load ();
		// Find the original instance of the user from the saved users list.
		User overwriteUser = savedUsers.Find(searchUser => searchUser.getName() == user.getName());

		// Remove the original copy of the user with the original values.
		savedUsers.Remove (overwriteUser);

		// Add the user with the updated information.
		savedUsers.Add (user);

		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames3.gd"); //you can call it anything you want
		bf.Serialize(file, savedUsers);
		file.Close();
	}   

	public  void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/savedGames3.gd")) 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames3.gd", FileMode.Open);
			savedUsers = (List<User>)bf.Deserialize(file);
			file.Close();
		}
	}

	


}
