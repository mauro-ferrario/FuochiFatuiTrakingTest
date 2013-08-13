using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Emitter : MonoBehaviour {
	private float 		angle  			= 0;
	public float 		speed			= 10.0f;
	public int			solidDensity	= 37;
	public List<GameObject>	objects;
	private int			countObject 	= 0;
	private float		fireRate		= .5f;
	private float		nextFire		= .0f;
	
	void Start () {
	}
	
	void Update () {
		float posZ = Random.Range(-5, 15);
		float posX = Mathf.Cos(angle) * 5.0f;
		
		Main main = GameObject.Find("Main").GetComponent("Main") as Main;
		
		//if(!main.freeDepth)
		//	posZ = Main.getRangeDepth(posZ);
		
		//transform.position = new Vector3(posX, 10, posZ);
		if(Input.GetButton("Fire1")&&Time.time>nextFire)
		{
			Debug.Log("FIRE!");
		//	if(countObject%solidDensity == 0)
		//	{
				for(int a = 0; a < 1; a++)
				{
					int pos; // = (int)Utility.ofMap(Random.Range(0,1), 0, 1, 0, objects.Count - 1, true);
					
					if(Random.Range(0.0f,1.0f) < .5f)
						pos = 0;
					else 
						pos = 1;
					GameObject go = Instantiate(objects[pos], this.transform.position, this.transform.rotation) as GameObject;
					SimpleObject simpleObject = go.GetComponent("SimpleObject") as SimpleObject;
					
					simpleObject.name += Random.Range(0, 1000);
				
					nextFire = Time.time + fireRate;
				}
		//	}
		}
		
		
		angle += speed;
		countObject++;
		if(countObject > 10000000)
			countObject = 0;
	}	
}
