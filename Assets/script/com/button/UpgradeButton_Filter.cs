using UnityEngine;
using System.Collections;

public class UpgradeButton_Filter : UpgradeButton
{
	private void Start ()
	{
		faculty = new Filter ();
		faculty.label = transform.parent.GetComponentInChildren<UILabel> ();
		faculty.UpdatePriceText ();
	}
	
	public override void Clicked ()
	{
		faculty.Upgrade ();
	}
	
}