using UnityEngine;
using System.Collections;

public class FacilityButton : SimpleButton
{
	// should set in Unity Editor
	public GameObject TargetFacility;
	public GameObject TargetBook;

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
