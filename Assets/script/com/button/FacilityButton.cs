using UnityEngine;
using System.Collections;

public class FacilityButton : SimpleButton
{
	public GameObject TargetFacility; // should set in Unity Editor
	public GameObject TargetBook; // should set in Unity Editor

	public override void Clicked ()
	{
		if (TargetFacility.activeSelf) {
			TargetFacility.SetActive (false);
		} else {
			TargetFacility.SetActive (true);

			TargetBook.SetActive (false);
		}
	}
}
