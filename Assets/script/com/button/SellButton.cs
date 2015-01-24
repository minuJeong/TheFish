using UnityEngine;
using System.Collections;

public class SellButton : SimpleButton
{
	public override void Clicked ()
	{
		foreach (var pawn in PawnManager.Instance().pawns) {
			if (pawn.growthIndex > 0) {
				Destroy (pawn.gameObject, 0.1f);
				PawnManager.Instance().pawns.Remove (pawn);
				Game.Instance ().money += 100;
				break;
			}
		}
	}
}
