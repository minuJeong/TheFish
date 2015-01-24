using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

public class Tank2
{
    private static Tank2 _instance = new Tank2();

    public static Tank2 Instance()
    {
        if (_instance == null)
        {
            Debug.Log("Game instance should not null");
        }

        return _instance;
    }

    private class BasicInfo
    {
        public string imagePrefix;
        public double priceMultiplier;
        public double maxFishCountMultiplier;
        public int maxLevel;
    }

    public class LevelInfo
    {
        public int level;
        public int price;
        public int maxFishCount;
    }

    private LevelInfo[] levelInfo;

    public LevelInfo[] Level { get { return levelInfo; } }

    public void Init()
    {
        string txt = Resources.Load<TextAsset>("info/Tank").text;
        var data = JsonMapper.ToObject(txt);

        var basicInfo = new BasicInfo();
        var level0Info = new LevelInfo();
        var level1Info = new LevelInfo();

        var basicInfoJson = data["basic_info"];
        basicInfo.imagePrefix = (string)basicInfoJson["imagePrefix"];
        basicInfo.priceMultiplier = (double)basicInfoJson["priceMultiplier"];
        basicInfo.maxFishCountMultiplier = (double)basicInfoJson["maxFishCountMultiplier"];
        basicInfo.maxLevel = (int)basicInfoJson["maxLevel"];

        var levelInfoJson = data["level_info"];

        level0Info.level = (int)levelInfoJson[0]["level"];
        level0Info.price = (int)levelInfoJson[0]["price"];
        level0Info.maxFishCount = (int)levelInfoJson[0]["maxFishCount"];

        level1Info.level = (int)levelInfoJson[1]["level"];
        level1Info.price = (int)levelInfoJson[1]["price"];
        level1Info.maxFishCount = (int)levelInfoJson[1]["maxFishCount"];

        levelInfo = new LevelInfo[basicInfo.maxLevel + 1];
        levelInfo[0] = level0Info;
        levelInfo[1] = level1Info;

        for (int i = 2; i <= basicInfo.maxLevel; ++i)
        {
            var info = new LevelInfo();
            info.level = i;
            info.price = ((int)basicInfo.priceMultiplier * levelInfo[i - 1].price / 1000) * 1000;
            info.maxFishCount = (int)basicInfo.maxFishCountMultiplier * levelInfo[i - 1].maxFishCount;

            levelInfo[i] = info;
        }
    }
}
