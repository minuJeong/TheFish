using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputControl : MonoBehaviour
{
	private const int BLOCK_DELAY = 10;
	private int blockDelay = BLOCK_DELAY;

	private void Update ()
	{
		if (blockDelay > 0) {
			blockDelay--;
		} else {
			if (Input.GetMouseButtonUp (0)) {
				Vector3 mousePoint = Game.Instance ().UICamera.ScreenToWorldPoint (Input.mousePosition);

				foreach (var button in GameObject.FindObjectsOfType<SimpleButton> ()) {
					if (button.collider2D.bounds.Contains (mousePoint)) {
						button.Clicked ();
						blockDelay = BLOCK_DELAY;
						return;
					}
				}

				List<Pawn> selectedPawns = new List<Pawn> ();
				foreach (var pawn in PawnManager.Instance ().pawns) {
					if (pawn.collider2D.bounds.Contains (mousePoint)) {
						selectedPawns.Add (pawn);
					}
				}

				foreach (var pawn in selectedPawns) {
					PawnManager.Instance ().SellPawn (pawn);
				}
			}
		}
	}
}

