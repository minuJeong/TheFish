using LitJson;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PawnManager
{
	// data
	public List<Pawn> pawns = new List<Pawn> ();
	public float MaxPawnCount = 6;
	private static JsonData PriceData = null;

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
			Debug.Log ("You will need at least 2 pawns");
			return;
		}

		if (pawn.growthIndex > 0) {
			GameObject.Destroy (pawn.gameObject, 0.1f);
			pawns.Remove (pawn);


			Game.Instance ().money += (int)PriceData [pawn.rankName];

			// Register book
			foreach (var pawnInfo in Game.Instance ().Book.PawnInfoList) {
				if (pawn.name == pawnInfo.name &&
					Game.Instance ().Book.UnlockedList.IndexOf (pawnInfo) == -1) {
					Game.Instance ().Book.Unlock (pawnInfo.index);
				}
			}
		}
	}
	
	// Singleton
	private PawnManager ()
	{
		if (null == PriceData) {
			PriceData = JsonMapper.ToObject (Resources.Load<TextAsset> ("info/SellPrice").text);
		}
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

