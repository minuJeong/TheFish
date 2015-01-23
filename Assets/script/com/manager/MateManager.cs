using UnityEngine;
using System.Collections;

public class MateManager
{
	// 
	public static void Mate ()
	{
		if (Random.value > _successRate) {
			Pawn pawn = Pawn.SprayPawn (Game.Instance ().transform);
			return;
		}
	}

	public const float MATE_INTERVAL = 0.4f;
	private static bool _interrupt = false;
	private static float _successRate = 0.4f;

	// Start this coroutine from game after game initialized
	public static IEnumerator StartMate ()
	{
		while (! _interrupt) {

			Mate ();

			yield return new WaitForSeconds (MATE_INTERVAL);
		}
	}
}

