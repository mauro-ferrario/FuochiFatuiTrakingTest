using UnityEngine;
using System.Collections;

public class TimerHandler : MonoBehaviour {

	private float timeLeft		= 0.0f;		
	public float totTimeLeft	= 10.0f;
	private bool	active		= false;
	
	
	void Start () {
		stopTimer();
	}
	
	void Update () {
		if(active)
		{
			timeLeft += Time.deltaTime;
			if(timeLeft >= totTimeLeft)
				endTimer();
		}
	}
	
	public float getPercTimerLeft()
	{
		return 1 - ((timeLeft == 0) ? 1 : timeLeft/totTimeLeft);
	}
	
	private void endTimer()
	{
		// Qui bisognerebbe usare gli eventi di unity, per ora richiamo solamente la funzione che mi interessa	
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
