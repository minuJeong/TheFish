using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class FacilityUI : MonoBehaviour
{
    private UIGrid grid;
    private GameObject tank, filter, heater;

    public void Init()
    {
        grid = transform.FindChild("Clip").FindChild("Foreground").FindChild("grid").gameObject.GetComponent<UIGrid>();

        tank = grid.gameObject.transform.FindChild("tank").gameObject;
        filter = grid.gameObject.transform.FindChild("filter").gameObject;
        heater = grid.gameObject.transform.FindChild("heater").gameObject;

        var tankPrice = tank.transform.FindChild("price_label").gameObject.GetComponent<UILabel>();
        var filterPrice = filter.transform.FindChild("price_label").gameObject.GetComponent<UILabel>();
        var heaterPrice = heater.transform.FindChild("price_label").gameObject.GetComponent<UILabel>();

        tankPrice.text = Tank2.Instance().Level[1].price + " Won";
        filterPrice.text = Filter2.Instance().Level[1].price + " Won";
        heaterPrice.text = Heater2.Instance().Level[1].price + " Won";
    }

    public void UpgradeTank()
    {
        var result = FacilityManager.Instance().UpgradeTank();
        switch (result)
        {
            case FacilityUpgradeResult.Success:
                {
                    UpdateTank();
                }
                break;
            case FacilityUpgradeResult.MaxLevel:
                {

                }
                break;
            case FacilityUpgradeResult.NotEnoughMoney:
                {

                }
                break;
        }
    }

    public void UpgradeFilter()
    {
        var result = FacilityManager.Instance().UpgradeFilter();
        switch (result)
        {
            case FacilityUpgradeResult.Success:
                {
                    UpdateFilter();
                }
                break;
            case FacilityUpgradeResult.MaxLevel:
                {

                }
                break;
            case FacilityUpgradeResult.NotEnoughMoney:
                {

                }
                break;
        }
    }

    public void UpgradeHeater()
    {
        var result = FacilityManager.Instance().UpgradeHeater();
        switch (result)
        {
            case FacilityUpgradeResult.Success:
                {
                    UpdateHeater();
                }
                break;
            case FacilityUpgradeResult.MaxLevel:
                {

                }
                break;
            case FacilityUpgradeResult.NotEnoughMoney:
                {

                }
                break;
        }
    }

    public void UpdateTank()
    {
        var tankPrice = tank.transform.FindChild("price_label").gameObject.GetComponent<UILabel>();
        int nextLevel = FacilityManager.Instance().TankLevel + 1;
        int maxLevel = Tank2.Instance().Basic.maxLevel;
        if (nextLevel >= maxLevel)
        {
            tankPrice.text = "Max Level Reached";
        }
        else
        {
            tankPrice.text = Tank2.Instance().Level[nextLevel].price + " Won";
        }
    }

    public void UpdateFilter()
    {
        var filterPrice = filter.transform.FindChild("price_label").gameObject.GetComponent<UILabel>();
        int nextLevel = FacilityManager.Instance().FilterLevel + 1;
        int maxLevel = Filter2.Instance().Basic.maxLevel;
        if (nextLevel >= maxLevel)
        {
            filterPrice.text = "Max Level Reached";
        }
        else
        {
            filterPrice.text = Filter2.Instance().Level[nextLevel].price + " Won";
        }
    }

    public void UpdateHeater()
    {
        var heaterPrice = heater.transform.FindChild("price_label").gameObject.GetComponent<UILabel>();
        int nextLevel = FacilityManager.Instance().HeaterLevel + 1;
        int maxLevel = Heater2.Instance().Basic.maxLevel;
        if (nextLevel >= maxLevel)
        {
            heaterPrice.text = "Max Level Reached";
        }
        else
        {
            heaterPrice.text = Heater2.Instance().Level[nextLevel].price + " Won";
        }
    }
}
