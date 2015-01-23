using UnityEngine;
using System.Collections;

public class MateManager
{
	// 
	public static void Mate ()
	{
		if (Random.value <= _successRate) {
			Pawn.SprayPawn (Game.Instance ().transform);
		}
	}

	public const float MATE_INTERVAL_BASE = 2.0f;
	public const float MATE_INTERVAL_RATE = 0.4f;
	private static bool _interrupt = false;
	private static float _successRate = 1.0f;

	// Start this coroutine from game after game initialized
	public static IEnumerator StartMate ()
	{
		while (! _interrupt) {

			Mate ();

			float _interval = 0f;

			if (PawnManager.Instance ().pawns.Count > 0) {
				_interval = MATE_INTERVAL_BASE + (MATE_INTERVAL_RATE / PawnManager.Instance ().pawns.Count);
			} else {
				_interval = MATE_INTERVAL_BASE;
			}

			yield return new WaitForSeconds (_interval);
		}
	}
}

