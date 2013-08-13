using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {
	
	public static List<float>		steps;
	public static float				limitBack 			= 15;
	public static float				limitFront 			= -5;
	public bool						freeDepth 			= true;
	public int 						totLevel			= 5;
	public int						stepLevel			= 100;
	private int						points				= 0;
	private int						actualLevel			= 1;
	private int						prevLevel			= 1;
	private AnimationHandler		animationHandler;
	public Vector3					gravity				= new Vector3(-100, 0, -10);
	public static List<ObjectInfo>	takenObjects;
	
	void Start () {
		takenObjects = new List<ObjectInfo>();
		steps = new List<float>();
		steps.Add(-4);
		steps.Add(4);
		steps.Add(13);	
		steps.Add(13);	
		animationHandler = this.GetComponent("AnimationHandler") as AnimationHandler;
		Physics.gravity = new Vector3(gravity.x, gravity.y, gravity.z);
	}
	
	public void addPoint(int newPoint)
	{
		points += newPoint;
	}
	
	void Update () {
		int levelPos = (int)Utility.ofMap(points, 0, totLevel * stepLevel, 1, 5, true);
		
		if(levelPos != prevLevel)
		{
			changeLevel(levelPos);
		}
		
		prevLevel = levelPos;
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
		Debug.Log (pos);
		return steps[pos];
	}
}
