using UnityEngine;
using System.Collections;

public class InputControl : MonoBehaviour
{
	private int block = 0;

	private void Update ()
	{
		if (Input.GetMouseButtonUp (0)) {
			Vector3 wp = Game.Instance ().UICamera.ScreenToWorldPoint (Input.mousePosition);
			foreach (var button in GameObject.FindObjectsOfType<SimpleButton> ()) {
				if (button.collider2D.bounds.Contains (wp)) {
					button.OnClick ();
				}
			}
		}
	}
}

