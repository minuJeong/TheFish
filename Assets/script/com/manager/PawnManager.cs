using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PawnManager
{
	// data
	public List<Pawn> pawns = new List<Pawn> ();


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

