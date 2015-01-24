using UnityEngine;
using System.Collections;

public class UpgradeButton_Filter : SimpleButton
{
    public override void Clicked()
    {
        GameObject.Find("UI Root")
            .transform
            .FindChild("Facilities")
            .gameObject
            .GetComponent<FacilityUI>()
            .UpgradeFilter();
    }

}