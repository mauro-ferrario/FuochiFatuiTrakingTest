/* 
 * Classe per la gestione del tempo che passa all'interno del gioco. 
 * Il timer parte ogni volta che inizia una nuova partita e viene bloccato quando il gioco finisce.
 * In caso in cui il timer arrivi a 0, viene richiamato un metodo (timerEnded) che appartiene allo Script "Main.cs"
 * Il timer effettua un conto alla rovescia.
 * La variabile "totTimeLeft" è di default uguale a 10 secondi ma può essere reimpostata tramite il pannello dello script.
 * La variabile "timeLeft" memorizza il tempo passato.
 * Con il metodo "getPercTimerLeft" è possibile ricevere un valore compreso fra 0 e 1 che rappresenta il tempo rimanente
 * rispetto al tempo massimo impostato. 0 significa che non c'è più tempo e 1 significa che c'è ancora tutto il tempo disponibile. *
 */


using UnityEngine;
using System.Collections;

public class TimerHandler : MonoBehaviour {
			
	public float 	totTimeLeft		= 10.0f;
	private float 	timeLeft		= 0.0f;
	private bool	active			= false;
	
	void Start () 
	{
		stopTimer();
	}
	
	void Update () 
	{
		if(active)
		{
			timeLeft += Time.deltaTime;
			if(timeLeft >= totTimeLeft)
				endTimer();
		}
	}
	
	public float getPercTimerLeft()
	{
		// Quello sotto è un if/else fatto con l'operatore ternario: 
		// http://www.html.it/pag/15404/controlli-condizionali-switch-e-operatori-ternari/
		return 1 - ((timeLeft == 0) ? 1 : timeLeft/totTimeLeft);
	}
	
	private void endTimer()
	{
		active = false;
		Main main = GameObject.Find("Main").GetComponent("Main") as Main;
		main.timeEnded();
		stopTimer();
	}
	
	public void resetTimer()
	{
		timeLeft = 0.0f;
	}
	
	public void startTimer()
	{
		active = true;
	}
	
	public void pauseTimer()
	{
		active = false;
	}
	
	public void stopTimer()
	{
		pauseTimer();
		resetTimer();
	}
}
