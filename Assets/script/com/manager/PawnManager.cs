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
            if(pawns.Count == 2)
            {
                DialogManager.Instance().ShowSpecialMessage("onlyTwoLeft");
            }

            // Register book
            foreach (var pair in Game.Instance().Book.PawnInfoList)
            {
                var pawnInfo = pair.Value;
                if (pawn.info == pawnInfo &&
                    Game.Instance().Book.UnlockedList.ContainsKey(pawnInfo.index) == false)
                {
                    Game.Instance().Book.Unlock(pawnInfo.index);
                }
            }
        }
    }

    public PawnRank GetMaxUnlockedRank()
    {
       // S rank special condition
       if(false) // Watch ad video once and it becomes true
       {
            {
                return PawnRank.S;
            }
        }

        var levels = new List<int>();
        levels.Add(FacilityManager.Instance().TankLevel);
        levels.Add(FacilityManager.Instance().FilterLevel);
        levels.Add(FacilityManager.Instance().HeaterLevel);

        int minimumLevelA = 10;
        int minimumLevelB = 5;
        int minimumLevelC = 3;

        // A
        PawnRank maxRank = PawnRank.D;
        bool passedA = true;
        bool passedB = true;
        bool passedC = true;

        foreach (int level in levels)
        {
            if(level < minimumLevelA)
            {
                passedA = false;
            }
            if(level < minimumLevelB)
            {
                passedB = false;
            }
            if(level < minimumLevelC)
            {
                passedC = false;
            }
        }

        if(passedA == true)
        {
            maxRank = PawnRank.A;
        }
        else if(passedB == true)
        {
            maxRank = PawnRank.B;
        }
        else if(passedC == true)
        {
            maxRank = PawnRank.C;
        }
        else
        {
            maxRank = PawnRank.D;
        }

        return maxRank;
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

