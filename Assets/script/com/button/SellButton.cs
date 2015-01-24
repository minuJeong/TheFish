using UnityEngine;
using System.Collections;

public class SellButton : SimpleButton
{
	public override void Clicked ()
	{
		foreach (var pawn in PawnManager.Instance().pawns) {
			PawnManager.Instance ().SellPawn (pawn);
			break;
		}
	}
}
