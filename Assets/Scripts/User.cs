using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class User : System.IComparable<User>
{
  //  User player = GameObject.FindObjectOfType<CurrentUser>().currentUser;

    private string name;
	private int level;
	//private int score;
	private int currency;
	private int life;
	private int rateOfFire;
	private int speed;
	private int maxHealth;
    private int hard;
    private int highScore;
    private float moveMultiplier;
    private float fireMultiplier;

	// Registering a new user.
	public User(string name)
	{
		// Set the user's username.
		this.name = name;

		// Initial level will be 1.
		this.level = 1;	

		// Start score from 0.
		//this.score = 0;
        
        this.currency = 0;

		// Start life from 3.
		this.life = 3;

		// flag
		this.rateOfFire = 0;

		// flag
		this.speed = 0;

		// Start max health at 3.
		this.maxHealth = 3;

        // hardened bullet flag
        this.hard = 0;

        // will be re-assigned
        this.highScore = 0;

        this.fireMultiplier = 1.0f;

        this.moveMultiplier = 1.0f;

	}


    // this is used to sort the high scores
    public int CompareTo(User other)
    {
        if (other.highScore < this.highScore)
        {
            return -1;
        }

        else if (other.highScore > this.highScore)
        {
            return 1;
        }

        return 0;

    }



    // our favorite getters and setters
    public string getName()
	{
		return this.name;
	}

	public void setName(string name)
	{
		this.name = name;
	}

	public int getLevel()
	{
		return this.level;
	}

	public void setLevel(int level)
	{
		this.level = level;
	}

    public void setMoveMultiplier(float moveMultiplier)
    {
        if (moveMultiplier < 0.1f)
            this.moveMultiplier = 0.1f;
        this.moveMultiplier = moveMultiplier;
    }

    public void setFireMultiplier(float fireMultiplier)
    {
        if (fireMultiplier < 0.1f)
            this.fireMultiplier = 0.1f;
        this.fireMultiplier = fireMultiplier;
    }

    public float getMoveMultiplier()
    {
        if (moveMultiplier < 0.1f)
            setMoveMultiplier(1.0f);
        return this.moveMultiplier; 
    }

    public float getFireMultiplier()
    {
        return this.fireMultiplier;
    }


	public int getCurrency()
	{
		return this.currency;
	}

	public void setCurrency(int currency)
	{
		this.currency = currency;
	}

	public int getLife()
	{	
		return this.life;
	}

	public void setLife(int life)
	{
		this.life = life;
	}

	public int getRateOfFire()
	{
		return this.rateOfFire;
	}

	public void setRateOfFire(int rateOfFire)
	{
		this.rateOfFire = rateOfFire;
	}

	public int getSpeed()
	{
		return this.speed;
	}

	public void setSpeed(int speed)
	{
		this.speed = speed;
	}

	public int getMaxHealth()
	{
		return this.maxHealth;
	}

	public void setMaxHealth(int maxHealth)
	{
		this.maxHealth = maxHealth;
	}

    public void setHard(int hard)
    {
        this.hard = hard;
    }

    public int getHard()
    {
        return this.hard;
    }

    public void setHighScore(int highScore)
    {
        this.highScore = highScore;
    }

    public int getHighScore()
    {
        return this.highScore;
    }

}
