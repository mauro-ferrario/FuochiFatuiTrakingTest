/*
 * Questa classe si occupa della gestione del movimento e delle collisioni del cubo con altri oggetti.
 * "cubeTarget" è una variabile statica dove vengono inseriti i valori della posizioni in cui dovrebbe trovarsi il cubo
 * "enableTracking" abilitata/disabilita il tracking del cubo tramite i dati che arrivano esternamente via OSC.
 * Se questa variabile non è attiva, il cubo sarà fermo e potrà essere mosso solamente agendo direttametne sulla scena.
 * "easing" serve per settare "il ritardo" di movimento del cubo quando si muove lungo l'asse z
 */

using UnityEngine;
using System.Collections;

public class CubeBehaviour : MonoBehaviour {
	
	public static Vector3 	cubeTarget;
	public bool				enableTracking 	= false;
	private float			easing 			= 0.1f;
	
	void Start () {
	}
	
	/* La prima cosa che viene effettuata all'interno di questo metodo è il controllo per capire se è in corso una paritata o no.
	 * Nel caso non ci sia nessuna partita in corso, non viene eseguito nulla.
	 * Nella variabile "actualPos" vengono salvate le coordinate x e y di dove dev'essere il cubo. Alla z non viene assegnata la varibile
	 * direttamente perchè si è scelto di muoverla utilizzando un effetto di easing.
	 */
	
	void Update () {
		Main main = GameObject.Find("Main").GetComponent("Main") as Main;
		if(!main.isPlay)
			return;
		
		Vector3 actualPos = transform.position;	
		actualPos.x = cubeTarget.x;
		actualPos.y = cubeTarget.y;
		actualPos.z += (cubeTarget.z - actualPos.z) * easing;	
		
		if(enableTracking)
		{
			transform.position = actualPos;	
		}	
	}
	/*
	 * Metoto richiamato quando avviene una collisione. 
	 * Il codice entra nell'if solamente quando la collisione è avvenuta con un oggetto che ha tag "Object".
	 * Una volta entrata nell'if, viene cambiato il colore del cubo che poi tramite una coroutine viene reimpostata a bianco poco dopo.
	 * Quando un oggetto viene preso, il valore assegnato al suo punteggio viene aggiunto al punteggio generale, tramite "main.addPoints".
	 * Subito dopo viene creata un'istanza di ObjectInfo dove vengono salvete le informazioni dell'oggetto appena preso. L'istanza così creata
	 * viene poi inserita all'interno della lista degli oggetti presi "takenObjects".
	 * Alla fine di tutto l'oggetto viene distrutto richiamando il suo metodo "DestroyMe";
	 */
	
	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "Object")
		{
			renderer.material.color = new Color(255,0,0);
			StartCoroutine(resetColor());
			Main main = GameObject.Find("Main").GetComponent("Main") as Main;
			SimpleObject so = other.gameObject.GetComponent("SimpleObject") as SimpleObject;
			main.addPoints(so.pointValue);
			ObjectInfo newInfo = new ObjectInfo(so.name, so.type);
			Main.takenObjects.Add(newInfo);
			so.DestroyMe();
		}
	}
	
	IEnumerator resetColor() 
	{
		yield return new WaitForSeconds (.2f);
		renderer.material.color = new Color(255,255,255);
	}
}
