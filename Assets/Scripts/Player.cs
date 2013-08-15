using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {
	
	public string 						name;
	public string						date;
	public int							points;
	public List<ObjectInfo>		takenObjects;
	
	
	public Player()
	{
		this.date = System.DateTime.Now.ToString("MM/dd/yyyy-HH:mm:ss");
	}
}
