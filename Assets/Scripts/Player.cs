/* 
 * Questa classe serve per salvare le informazioni del giocatore.
 * "name" è il nome del giocatore. Al momento non esiste una parte del gioco dove viene assegnato il valore a questa variabile
 * "date" è la data di inizio della partita e quindi viena assegnata subito, appena creata l'istanza di "Player". La data viene presa tramite
 * il metodo base all'interno di C#
 * "points" è la variabile dove verrà inserito il punteggio fatto dal giocatore. Questa variabile verrà assegnata solo a fine partita
 * "takenObjects" è una lista di "ObjectInfo" che serve a salvare la lista di tutti gli oggetti catturati dal giocatore. Anche questa variabile
 * verrà riempita solamente a fine partita
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {
	
	public string 						name;
	public string						date;
	public int							points;
	public List<ObjectInfo>				takenObjects;
	
	public Player()
	{
		this.date = System.DateTime.Now.ToString("MM/dd/yyyy-HH:mm:ss");
	}
}
