using UnityEngine;
using System.Collections;

public class UpgradeButton_Tank : UpgradeButton
{
	private void Start ()
	{
		faculty = new Tank ();
		faculty.label = transform.parent.GetComponentInChildren<UILabel> ();
		faculty.UpdatePriceText ();
	}
	
	public override void Clicked ()
	{
		faculty.Upgrade ();
	}
	
}