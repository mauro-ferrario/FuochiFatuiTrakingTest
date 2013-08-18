/*
 * Questa è la classe base per gli oggetti che dovranno essere presi dal carrello.
 * Ogni oggetto avrà determinate caratteristiche.
 * "name" identifica il nome di questo oggetto. Il nome può essere un nome qualsiasi e è fatto per differenziare i vari oggetti fra loro
 * "type" identifica il tipo di oggetto. Se ad esempio dovessero esserci demoni, folletti, angeli, questi dovrebbero avere ognuno un tipo diverso
 * e il tipo è identificato con un intero. 
 * "pointValue" è il valore di punti che il giocatore guadagna se prende questo oggetto.
 * "ObjectCameraPath" è il riferimento a un Prefab di tipo "GameObject" che contiene i Component di "CameraPathBezierAnimator" e "CameraPathBezier".
 * Questo riferimento viene associato da pannello di controllo dei componenti agli oggetti in "Prefab" che dovranno apparire e essere presi dal
 * giocatore. In questo modo da pannello di controllo è possibile associare a ogni oggetto di tipo diverso, diversi tracciati da seguire.
 * Volendo in futuro si potrebbe anche impostare una lista di "ObjectCameraPath", per fare in modo che oggetti dello stesso tipo possano seguire
 * tracciati diversi. 
 * "myPath" è un'istanza di "ObjectCameraPath". E' necessario creare un'istanza per ogni "SimpleObject" perchè ogni "GameObject" con associato lo
 * Script di "CameraPathBezierAnimator" può gestire il percorso per un solo oggetto alla volta. In questo modo, creando un'istanza dei tracciati
 * creati in precedenza, ogni "SimbleObject" potrà avere la proprio copia del tracciato che verrà poi distrutta quando anche l'oggetto verrà rimosso
 * dallo stage.
 * "cameraPathBezierAnimator" è il riferimanto a "CameraPathBezierAnimator" grazie al quale possiamo controllare l'animazione del noostro oggetto e 
 * settare quale sarà la matrice di trasformazione da prendere come riferimento da associare all'animazione. Per far questo basta associare la matrice
 * dell'oggetto alla variabile "animationTarget" contenuta all'interno dello di "CameraPathBezierAnimator".
 * Oltre a questo, grazie al riferimento a "CameraPathBezierAnimator", siamo anche in grado di intercettare tutti gli eventi che vengono generati durante
 * l'animaizone dell'oggetto.
 */

using UnityEngine;
using System.Collections;

public class SimpleObject : MonoBehaviour {
	
	public string 						name;
	public int 							type;
	public int							pointValue = 10;
	public GameObject					ObjectCameraPath;
	private GameObject					myPath;
	private CameraPathBezierAnimator 	cameraPathBezierAnimator;
	
	// Prima di tutto vengono associati i listener per gli eventi di "CameraPathBezierAnimator" e viene settato il valore a "animationTarget"
	
	void Awake()
	{		
		myPath = Instantiate(ObjectCameraPath, ObjectCameraPath.transform.position, ObjectCameraPath.transform.rotation) as GameObject;
		cameraPathBezierAnimator = myPath.GetComponent("CameraPathBezierAnimator") as CameraPathBezierAnimator;
		cameraPathBezierAnimator.animationTarget = this.transform;
		cameraPathBezierAnimator.AnimationStarted += OnAnimationStarted;
        cameraPathBezierAnimator.AnimationPaused += OnAnimationPaused;
        cameraPathBezierAnimator.AnimationStopped += OnAnimationStopped;
        cameraPathBezierAnimator.AnimationFinished += OnAnimationFinished;
        cameraPathBezierAnimator.AnimationLooped += OnAnimationLooped;
        cameraPathBezierAnimator.AnimationPingPong += OnAnimationPingPonged;

        cameraPathBezierAnimator.AnimationPointReached += OnPointReached;
        cameraPathBezierAnimator.AnimationPointReachedWithNumber += OnPointReachedByNumber;
	}
	
	// Questo metodo distrugge prima il tracciato riferito all'oggetto e in seguito distrugge l'oggetto stesso
	
	public void DestroyMe()
	{
		Destroy(myPath);
		Destroy(gameObject);
	}
	
	void Start () 
	{
		cameraPathBezierAnimator.Play();		
	}
	
	void Update () {
	
	}
	
	private void OnAnimationStarted()
    {
    }

    private void OnAnimationPaused()
    {
    }

    private void OnAnimationStopped()
    {
    }

    private void OnAnimationFinished()
    {
		DestroyMe();
    }

    private void OnAnimationLooped()
    {
    }

    private void OnAnimationPingPonged()
    {
    }

    private void OnPointReached()
    {
    }

    private void OnPointReachedByNumber(int pointNumber)
    {
    }
}
