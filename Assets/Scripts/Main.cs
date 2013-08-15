using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {
	
	public static List<float>		steps;
	public static float				limitBack 			= 15;
	public static float				limitFront 			= -5;
	public bool						freeDepth 			= true;
	public int 						totLevel			= 5;
	public int						stepLevel			= 100;			// Numero di punti che ci vogliono per raggiungere un nuovo livello
	private int						points				= 0;
	private int						actualLevel			= 1;
	private int						prevLevel			= 1;
	private AnimationHandler		animationHandler;
	public Vector3					gravity				= new Vector3(-100, 0, -10);
	public static List<ObjectInfo>	takenObjects;
	public static List<Player>		players;
	public bool						isPlay				= false;
	public int						singlePointLeftValue = 1;
	private Player					actualPlayer;
	private TimerHandler			timerHandler;
	
	void Start () {
		takenObjects = new List<ObjectInfo>();
		players = new List<Player>();
		steps = new List<float>();
		steps.Add(-4);
		steps.Add(4);
		steps.Add(13);	
		steps.Add(13);	
		animationHandler = this.GetComponent("AnimationHandler") as AnimationHandler;
		timerHandler = this.GetComponent("TimerHandler") as TimerHandler;
		Physics.gravity = new Vector3(gravity.x, gravity.y, gravity.z);
	}
	
	public void addPoint(int newPoint)
	{
		points += newPoint;
	}
	
	void Update () {
		if(isPlay)
		{
			int levelPos = (int)Utility.ofMap(points, 0, totLevel * stepLevel, 1, totLevel, true);
			
			if(levelPos != prevLevel)
			{
				changeLevel(levelPos);
			}
			
			prevLevel = levelPos;
		}
		else
		{
			if(Input.GetKey("return"))
			{
				Debug.Log ("START");
				startNewGame();
			}				
		} 
	}
	
	public void startNewGame()
	{
		resetGame();
		actualPlayer = new Player();
		isPlay = true;
		timerHandler.startTimer();
	}
	
	private void resetGame()
	{
		timerHandler.stopTimer();
		takenObjects.Clear();
		actualLevel = 1;
		prevLevel = 1;
		points = 0;
	}
	
	public void debugAllPlayers()
	{
		foreach(Player p in players)
		{
			Debug.Log ("*********************");
			Debug.Log (p.date);
			Debug.Log (p.points);
			Debug.Log ("*********************");
		}
	}
	
	public void timeEnded()
	{
		isPlay = false;
		savePlayerInfo();
		debugAllPlayers();
		removeAllObjects();
	}
	
	private void savePlayerInfo()
	{
		actualPlayer.points = points;
		actualPlayer.takenObjects = takenObjects;
		players.Add(actualPlayer);
	}
	
	public void removeAllObjects()
	{
		// Rimuovo tutti gli oggetti all'interno del gioco
	}
	
	public void gameComplete()
	{
		int morePoints = (int)timerHandler.getPercTimerLeft() * singlePointLeftValue;
	}
	
	private void changeLevel(int newLevel)
	{
		actualLevel = newLevel;
		Debug.Log ("LEVEL = " + actualLevel);
		animationHandler.startAnimationLevel(actualLevel);
		// Fare partire le animazioni di cambio livello
	}
	
	public static float getRangeDepth(float z) 
	{
		int pos = (int)Utility.ofMap(z, limitFront, limitBack, 0, steps.Count - 1, true);
		if(pos >= steps.Count)
			pos = steps.Count-1;
		if(pos < 0)
			pos = 0;
		return steps[pos];
	}
}
