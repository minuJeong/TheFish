using UnityEngine;
using System.Collections;

public class InputControl : MonoBehaviour
{
	private void Update ()
	{
		if (Input.GetMouseButton (0)) {
			Ray ray = new Ray (Input.mousePosition, Vector3.forward);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {

			}
		}
	}
}

