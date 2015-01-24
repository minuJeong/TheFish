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
        }
        else
        {
			TargetBook.transform.localPosition = new Vector3(0, 0, 0);
			TargetBook.SetActive(true);

			TargetFacility.SetActive (false);
        }
    }
}
