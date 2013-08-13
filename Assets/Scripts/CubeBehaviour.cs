using UnityEngine;
using System.Collections;

public class CubeBehaviour : MonoBehaviour {
	
	public static Vector3 	cubeTarget;
	public float			speed;
	private float			easing 		= 0.1f;
	
	void Start () {
	
	}
	
	void Update () {
		Vector3 actualPos = transform.position;	
		actualPos.x = cubeTarget.x;
		actualPos.y = cubeTarget.y;
		actualPos.z += (cubeTarget.z - actualPos.z) * easing;			
		//transform.position = actualPos;		
	}
	
	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "Object")
		{
			renderer.material.color = new Color(255,0,0);
			StartCoroutine(resetColor());			
			Destroy(other.gameObject);
			Main main = GameObject.Find("Main").GetComponent("Main") as Main;
			main.addPoint(10);
			SimpleObject so = other.gameObject.GetComponent("SimpleObject") as SimpleObject;
			ObjectInfo newInfo = new ObjectInfo(so.name, so.type);
			Main.takenObjects.Add(newInfo);
		}
	}
	
	IEnumerator resetColor() 
	{
		yield return new WaitForSeconds (.2f);
		renderer.material.color = new Color(255,255,255);
	}
}
