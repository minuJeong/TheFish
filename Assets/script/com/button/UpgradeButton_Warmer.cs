using UnityEngine;
using System.Collections;

public class UpgradeButton_Warmer : SimpleButton
{
    public override void Clicked()
    {
        GameObject.Find("UI Root")
            .transform
            .FindChild("Facilities")
            .gameObject
            .GetComponent<FacilityUI>()
            .UpgradeHeater();
    }
}