using UnityEngine;
using System.Collections;

public class BookButton : SimpleButton
{
	public GameObject TargetFacility; // should set in Unity Editor
	public GameObject TargetBook; // should set in Unity Editor

    public override void Clicked()
    {
		if (TargetBook.activeSelf)
        {
			TargetBook.SetActive(false);
			TargetBook.transform.FindChild ("Clip/Foreground/dragged").localPosition = Vector3.zero;
        }
        else
        {
			TargetBook.transform.localPosition = new Vector3(0, 0, 0);
			TargetBook.SetActive(true);

			TargetFacility.SetActive (false);
        }
    }
}
