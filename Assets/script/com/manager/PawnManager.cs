using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PawnManager
{
	// data
	public List<Pawn> pawns = new List<Pawn> ();
	public int MaxPawnCount = 3;

	public bool isPawnMax ()
	{
		if (MaxPawnCount > pawns.Count) {
			return false;
		}
		return true;
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

