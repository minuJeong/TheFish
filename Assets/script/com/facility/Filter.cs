using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

public class Filter2
{
    private static Filter2 _instance = new Filter2();

    public static Filter2 Instance()
    {
        if (_instance == null)
        {
            Debug.Log("Game instance should not null");
        }

        return _instance;
    }

    public class BasicInfo
    {
        public string imagePrefix;
        public double priceMultiplier;
        public int maxLevel;
        public string[] percentageMultiplier;
    }

    public class LevelInfo
    {
        public int level;
        public int price;
        public double[] percentage;
    }

    private BasicInfo basicInfo;
    private LevelInfo[] levelInfo;

    public BasicInfo Basic { get { return basicInfo; } }
    public LevelInfo[] Level { get { return levelInfo; } }

    public void Init()
    {
        string txt = Resources.Load<TextAsset>("info/Filter").text;
        var data = JsonMapper.ToObject(txt);
        int numOfRanks = Enum.GetNames(typeof(PawnRank)).Length;

        basicInfo = new BasicInfo();
        var level0Info = new LevelInfo();
        var level1Info = new LevelInfo();

        var basicInfoJson = data["basic_info"];
        basicInfo.imagePrefix = (string)basicInfoJson["imagePrefix"];
        basicInfo.priceMultiplier = (double)basicInfoJson["priceMultiplier"];
        basicInfo.maxLevel = (int)basicInfoJson["maxLevel"];
        basicInfo.percentageMultiplier = new string[numOfRanks];
        foreach (var rankObj in Enum.GetValues(typeof(PawnRank)))
        {
            var rank = (PawnRank)rankObj;
            if((int)rank == (int)PawnRank.D)
            {
                continue;
            }

            basicInfo.percentageMultiplier[(int)rank - 1] = (string)basicInfoJson["percentageMultiplier"][rank.ToString()];
        }

        double percentageSum = 0.0;

        var levelInfoJson = data["level_info"];

        level0Info.level = (int)levelInfoJson[0]["level"];
        level0Info.price = (int)levelInfoJson[0]["price"];
        level0Info.percentage = new double[numOfRanks];
        foreach (var rankObj in Enum.GetValues(typeof(PawnRank)))
        {
            var rank = (PawnRank)rankObj;
            if ((int)rank == (int)PawnRank.D)
            {
                continue;
            }

            double percentage = (double)levelInfoJson[0]["percentage"][rank.ToString()];
            level0Info.percentage[(int)rank - 1] = percentage;
            percentageSum += percentage;
        }
        level0Info.percentage[(int)PawnRank.D - 1] = 100.0 - percentageSum;

        percentageSum = 0.0;
        level1Info.level = (int)levelInfoJson[1]["level"];
        level1Info.price = (int)levelInfoJson[1]["price"];
        level1Info.percentage = new double[numOfRanks];
        foreach (var rankObj in Enum.GetValues(typeof(PawnRank)))
        {
            var rank = (PawnRank)rankObj;
            if ((int)rank == (int)PawnRank.D)
            {
                continue;
            }

            double percentage = (double)levelInfoJson[1]["percentage"][rank.ToString()];
            level1Info.percentage[(int)rank - 1] = percentage;
            percentageSum += percentage;
        }
        level1Info.percentage[(int)PawnRank.D - 1] = percentageSum;

        levelInfo = new LevelInfo[basicInfo.maxLevel + 1];
        levelInfo[0] = level0Info;
        levelInfo[1] = level1Info;

        for (int i = 2; i <= basicInfo.maxLevel; ++i)
        {
            percentageSum = 0.0;

            var info = new LevelInfo();
            info.level = i;
            info.price = ((int)(basicInfo.priceMultiplier * levelInfo[i - 1].price) / 1000) * 1000;
            info.percentage = new double[numOfRanks];
            foreach (var rankObj in Enum.GetValues(typeof(PawnRank)))
            {
                var rank = (PawnRank)rankObj;
                int multiplier = 0;
                double percentage = 0.0;

                if (basicInfo.percentageMultiplier[(int)rank - 1] == "square")
                {
                    multiplier = i * i;
                }
                else if (basicInfo.percentageMultiplier[(int)rank - 1] == "linear")
                {
                    multiplier = i;
                }

                percentage = level1Info.percentage[(int)rank - 1] * multiplier;
                info.percentage[(int)rank - 1] = percentage;
                percentageSum += percentage;
            }
            info.percentage[(int)PawnRank.D - 1] = 100.0 - percentageSum;

            levelInfo[i] = info;
        }
    }
}