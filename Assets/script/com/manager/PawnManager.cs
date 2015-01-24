using LitJson;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PawnManager
{
    // data
    public List<Pawn> pawns = new List<Pawn>();
    private static JsonData PriceData = null;

    public bool isPawnMax()
    {
        int maxPawnCount = Tank2.Instance().Level[FacilityManager.Instance().TankLevel].maxFishCount;
        if (maxPawnCount > pawns.Count)
        {
            return false;
        }
        return true;
    }

    public void SellPawn(Pawn pawn)
    {
        if (pawns.Count < 3)
        {
            Debug.Log("You will need at least 2 pawns");
            return;
        }

        if (pawn.growthIndex > 0)
        {
            SoundManager.Instance().Play("sell");
            GameObject.Destroy(pawn.gameObject, 0.1f);
            pawns.Remove(pawn);


            Game.Instance().money += (int)PriceData[pawn.rankName];

            // Register book
            foreach (var pair in Game.Instance().Book.PawnInfoList)
            {
                var pawnInfo = pair.Value;
                if (pawn.name == pawnInfo.name &&
                    Game.Instance().Book.UnlockedList.ContainsKey(pawnInfo.index) == false)
                {
                    Game.Instance().Book.Unlock(pawnInfo.index);
                }
            }
        }
    }

    public bool IsRankAvailable(PawnRank rank)
    {
        if(rank == PawnRank.S)
        {
            if(false) // Watch ad video once and it becomes false
            {
                return false;
            }
        }

        var levels = new List<int>();
        levels.Add(FacilityManager.Instance().TankLevel);
        levels.Add(FacilityManager.Instance().FilterLevel);
        levels.Add(FacilityManager.Instance().HeaterLevel);

        int minimumLevel = 100000;

        switch (rank)
        {
            case PawnRank.S:
            case PawnRank.A:
                {
                    minimumLevel = 10;
                }
                break;
            case PawnRank.B:
                {
                    minimumLevel = 5;
                }
                break;
            case PawnRank.C:
                {
                    minimumLevel = 3;
                }
                break;
            case PawnRank.D:
                {
                    minimumLevel = -1;
                }
                break;
        }

        bool passed = true;
        foreach (var level in levels)
        {
            if(level < minimumLevel)
            {
                passed = false;
                break;
            }
        }

        return passed;
    }

    // Singleton
    private PawnManager()
    {
        if (null == PriceData)
        {
            PriceData = JsonMapper.ToObject(Resources.Load<TextAsset>("info/SellPrice").text);
        }
    }

    private static PawnManager _instance = null;

    public static PawnManager Instance()
    {
        if (_instance == null)
        {
            _instance = new PawnManager();
        }

        return _instance;
    }
}

