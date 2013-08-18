/*
 * Classe per salvare le informazioni degli oggetti presi dal giocatore.
 * Al momento viene solo salvato il nome e il tipo dell'oggetto.
 * Questa classe viene istanziata quando un oggetto viene preso e l'istanza create viene subito messa
 * all'interno della lista degli oggetti presi durante la partita atttuale
 */

using UnityEngine;
using System.Collections;

public class ObjectInfo {
	
	public string 	name;
	public int		type;
	
	
	public ObjectInfo(string name, int type)
	{
		this.name = name;
		this.type = type;
	}
}
