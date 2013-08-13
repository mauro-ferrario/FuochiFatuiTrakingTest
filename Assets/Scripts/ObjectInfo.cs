using UnityEngine;
using System.Collections;

public class ObjectInfo {
	
	public string 	name;
	public int		type;
	
	
	public ObjectInfo(string name, int type)
	{
		this.name = name;
		this.type = type;
		Debug.Log ("INFO " + name);
	}
}
