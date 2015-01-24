using UnityEngine;
using System.Collections;

public class MateManager
{
	// 
	public static IEnumerator Mate ()
	{
		yield return 0;

		if (Random.value <= _successRate) {
			Pawn.SprayPawn (Game.Instance ().transform);
		}
	}
	
	private static float _successRate = 1.0f;
}

