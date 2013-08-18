/*
 * Questa classe si occupa della creazione degli oggetti che il giocatore dovrà prendere.
 * L'oggetto verrà creato solamente quando il bottone di "Fire1" verrà premuto.
 * E' possibile passare uno o più "GameObject" alla variabile "objects", tramite il pannello dei componenti. Al momento 
 * vengono passati solo due "GameObject".
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Emitter : MonoBehaviour {
	public List<GameObject>	objects; 
	private float			fireRate		= .5f;
	private float			nextFire		= .0f;
	
	void Start () {
	}
	
	/* La prima cosa che viene effettuata all'interno di questo metodo è il controllo per capire se è in corso una paritata o no.
	 * Nel caso non ci sia nessuna partita in corso, non viene eseguito nulla.
	 * Nel caso venga premuto il bottone di "Fire1" e nel caso sia passato abbastanza tempo dalla creazione dell'ultimo oggetto, allora
	 * viene creato un altro oggetto. Il nuovo oggetto creato sarà un oggetto a caso fra quelli presenti nella lista "objects". Dato che 
	 * al momento ci sono solo 2 oggetti, l'if/else viene utilizzato per scegliere quale dei 2 oggetti creare.
	 */
	
	void Update () {
		Main main = GameObject.Find("Main").GetComponent("Main") as Main;
		if(!main.isPlay)
			return;
		
		if(Input.GetButton("Fire1")&&Time.time>nextFire)
		{
			int pos;
			if(Random.Range(0.0f,1.0f) < .5f)
				pos = 0;
			else 
				pos = 1;
				
			GameObject go = Instantiate(objects[pos], this.transform.position, this.transform.rotation) as GameObject;
			SimpleObject simpleObject = go.GetComponent("SimpleObject") as SimpleObject;
			simpleObject.name += Random.Range(0, 1000);
			// http://docs.unity3d.com/Documentation/ScriptReference/Time-time.html
			nextFire = Time.time + fireRate;
			
		}
	}	
}
