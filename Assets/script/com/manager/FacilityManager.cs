using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum FacilityUpgradeResult
{
    Success,
    NotEnoughMoney,
    MaxLevel
}

public class FacilityManager
{
    private static FacilityManager _instance = new FacilityManager();

    public static FacilityManager Instance()
    {
        if (_instance == null)
        {
            Debug.Log("Game instance should not null");
        }

        return _instance;
    }

    public FacilityUpgradeResult UpgradeTank()
    {
        if (TankLevel == Tank2.Instance().Basic.maxLevel)
        {
            return FacilityUpgradeResult.MaxLevel;
        }

        int nextLevel = TankLevel + 1;
        int currentMoney = Game.Instance().money;
        int requiredMoney = Tank2.Instance().Level[nextLevel].price;

        if (currentMoney < requiredMoney)
        {
            return FacilityUpgradeResult.NotEnoughMoney;
        }

        ++TankLevel;
		Game.Instance ().money -= requiredMoney;
        return FacilityUpgradeResult.Success;
    }

    public FacilityUpgradeResult UpgradeFilter()
    {
        if (TankLevel == Filter2.Instance().Basic.maxLevel)
        {
            return FacilityUpgradeResult.MaxLevel;
        }

        int nextLevel = FilterLevel + 1;
        int currentMoney = Game.Instance().money;
        int requiredMoney = Filter2.Instance().Level[nextLevel].price;

        if (currentMoney < requiredMoney)
        {
            return FacilityUpgradeResult.NotEnoughMoney;
        }

        ++FilterLevel;
		Game.Instance ().money -= requiredMoney;
        return FacilityUpgradeResult.Success;
    }

    public FacilityUpgradeResult UpgradeHeater()
    {
        if (TankLevel == Heater2.Instance().Basic.maxLevel)
        {
            return FacilityUpgradeResult.MaxLevel;
        }

        int nextLevel = HeaterLevel + 1;
        int currentMoney = Game.Instance().money;
        int requiredMoney = Heater2.Instance().Level[nextLevel].price;

        if (currentMoney < requiredMoney)
        {
            return FacilityUpgradeResult.NotEnoughMoney;
        }

        ++HeaterLevel;
		Game.Instance ().money -= requiredMoney;
        return FacilityUpgradeResult.Success;
    }

    public int TankLevel = 0;
    public int FilterLevel = 0;
    public int HeaterLevel = 0;
}