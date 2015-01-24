using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PawnManager
{
	// data
	public List<Pawn> pawns = new List<Pawn> ();
	public int MaxPawnCount = 6;

	public bool isPawnMax ()
	{
		if (MaxPawnCount > pawns.Count) {
			return false;
		}
		return true;
	}

	public void SellPawn (Pawn pawn)
	{
		if (pawns.Count < 3) {
			Debug.Log ("You have to leave at least 2 pawns");
			return;
		}

		if (pawn.growthIndex > 0) {
			GameObject.Destroy (pawn.gameObject, 0.1f);
			pawns.Remove (pawn);

			Game.Instance ().money += 100;

			// TODO: register book
		}
	}
	
	// Singleton
	private PawnManager ()
	{
	}

	private static PawnManager _instance = null;

	public static PawnManager Instance ()
	{
		if (_instance == null) {
			_instance = new PawnManager ();
		}

		return _instance;
	}
}

