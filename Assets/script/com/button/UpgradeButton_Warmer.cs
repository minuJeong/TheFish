using UnityEngine;
using System.Collections;

public class UpgradeButton_Warmer : UpgradeButton
{
	private void Start ()
	{
		faculty = new Warmer ();
		faculty.label = transform.parent.GetComponentInChildren<UILabel> ();
		faculty.UpdatePriceText ();
	}
	
	public override void Clicked ()
	{
		faculty.Upgrade ();
	}
	
}