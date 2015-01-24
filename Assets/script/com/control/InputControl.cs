using UnityEngine;
using System.Collections;

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
				Vector3 wp = Game.Instance ().UICamera.ScreenToWorldPoint (Input.mousePosition);
				foreach (var button in GameObject.FindObjectsOfType<SimpleButton> ()) {
					if (button.collider2D.bounds.Contains (wp)) {
						button.Clicked ();
						blockDelay = BLOCK_DELAY;
						return;
					}
				}
			}
		}
	}
}

