/*
 * Questa classe ha il compito di gestire le funzioni principali del gioco e contiene le variabili principali che verranno utilizzate
 * anche all'interno di altri script.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {
	
	public static List<float>		depthSteps;												// Lista utilizzata per definire i diversti livelli di profondità quando il movimento sull'asse z non è libero
	public static List<ObjectInfo>	takenObjects;											// Lista utilizzata per memorizzare tutti gli oggetti presi dal giocatore nella partita attuale
	public static List<Player>		players;												// Lista di tutti i giocatori che hanno effetuato delle partite
	public static float				limitBack 				= 15;							// Limite posteriore di profondità per il movimento del cubo
	public static float				limitFront 				= -5;							// Limite anteriore di profondità per il movimento del cubo
	public bool						freeDepth 				= true;							// Variabile che abilita/disabilita lo spostamento libero o a "step" del movimento del cubo sull'asse z
	public int 						totLevel				= 5;							// Numero totale dei livelli del gioco
	public int						stepLevel				= 100;							// Numero di punti che ci vogliono per raggiungere un nuovo livello
	public Vector3					gravity					= new Vector3(-100, 0, -10);	// Coordinate per la direzione della forza di gravità
	public bool						isPlay					= false;						// Variabile che memorizza se c'è una partita in corso o no
	public int						singlePointLeftValue 	= 10;							// Punteggio che verrà attribuito a un punto di percentuale del tempo che è rimasto quando si è finito il gioco. Se il tempo rimasto è .5 rispetto al totale, allora verranno aggiunti .5 * 10 = 5 punti
	private int						points					= 0;							// Punteggio della partita attuale
	private int						actualLevel				= 1;							// Livello attuale
	private int						prevLevel				= 1;							// Livello precedente
	private AnimationHandler		animationHandler;										// Istanza della classe che gestische le animazioni della struttura del livello	
	private Player					actualPlayer;											// Istanza per memorizzare i dati del giocatore attuale
	private TimerHandler			timerHandler;											// Istanza del gestore del tempo
	
	void Start () {
		takenObjects = new List<ObjectInfo>();
		players = new List<Player>();
		initDepthSteps();
		animationHandler = this.GetComponent("AnimationHandler") as AnimationHandler;
		timerHandler = this.GetComponent("TimerHandler") as TimerHandler;
		Physics.gravity = new Vector3(gravity.x, gravity.y, gravity.z);
	}
	
	private void initDepthSteps()
	{
		depthSteps = new List<float>();
		depthSteps.Add(-4);
		depthSteps.Add(4);
		depthSteps.Add(13);	
	}
	
	public void addPoints(int newPoint)
	{
		points += newPoint;
	}
	
	void Update () {
		if(isPlay)
		{
			// Viene calcolpato il livello attuale controllando quanti punti ci sono. In caso ci sia un cambiamento di livello
			// viene richiamato il metodo changeLevel
			
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
	
	// Ogni volta che si inzia una nuoa partita il gioco dev'essere resettato e deve essere creato un nuovo giocatore rimuovendo quello vecchio
	// Subito dopo il timer deve ripartire con il conto alla rovescia
	
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
	
	// Metodo per visualizzare in console la lista e il punteggio di tutti i giocatori
	
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
		// Rimuovo tutti gli oggetti all'interno del gioco. To do...
	}
	
	public void gameComplete()
	{
		int morePoints = (int)timerHandler.getPercTimerLeft() * singlePointLeftValue;
	}
	
	private void changeLevel(int newLevel)
	{
		actualLevel = newLevel;
		animationHandler.startAnimationLevel(actualLevel);
	}
	
	// Questo metodo statico viene richiamato quando non si è in modalità libera della posizione z del cubo.
	// Il metodo restituisce la z con solamente alcuni valori prestabiliti all'inizio e settati nella lista depthSteps
	
	public static float getRangeDepth(float z) 
	{
		int pos = (int)Utility.ofMap(z, limitFront, limitBack, 0, depthSteps.Count - 1, true);
		if(pos >= depthSteps.Count)
			pos = depthSteps.Count-1;
		if(pos < 0)
			pos = 0;
		return depthSteps[pos];
	}
}
