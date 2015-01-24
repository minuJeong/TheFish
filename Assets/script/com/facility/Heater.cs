using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

public class Heater2
{
    private static Heater2 _instance = new Heater2();

    public static Heater2 Instance()
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
        public double hatchTimeMultiplier;
        public int maxLevel;
    }

    public class LevelInfo
    {
        public int level;
        public int price;
        public double hatchTime;
    }

    private BasicInfo basicInfo;
    private LevelInfo[] levelInfo;

    public BasicInfo Basic { get { return basicInfo; } }
    public LevelInfo[] Level { get { return levelInfo; } }

    public void Init()
    {
        string txt = Resources.Load<TextAsset>("info/Heater").text;
        var data = JsonMapper.ToObject(txt);

        basicInfo = new BasicInfo();
        var level0Info = new LevelInfo();
        var level1Info = new LevelInfo();

        var basicInfoJson = data["basic_info"];
        basicInfo.imagePrefix = (string)basicInfoJson["imagePrefix"];
        basicInfo.priceMultiplier = (double)basicInfoJson["priceMultiplier"];
        basicInfo.hatchTimeMultiplier = (double)basicInfoJson["hatchTimeMultiplier"];
        basicInfo.maxLevel = (int)basicInfoJson["maxLevel"];

        var levelInfoJson = data["level_info"];

        level0Info.level = (int)levelInfoJson[0]["level"];
        level0Info.price = (int)levelInfoJson[0]["price"];
        level0Info.hatchTime = (double)levelInfoJson[0]["hatchTime"];

        level1Info.level = (int)levelInfoJson[1]["level"];
        level1Info.price = (int)levelInfoJson[1]["price"];
        level1Info.hatchTime = (double)levelInfoJson[1]["hatchTime"];

        levelInfo = new LevelInfo[basicInfo.maxLevel + 1];
        levelInfo[0] = level0Info;
        levelInfo[1] = level1Info;

        for (int i = 2; i <= basicInfo.maxLevel; ++i)
        {
            var info = new LevelInfo();
            info.level = i;
            info.price = ((int)(basicInfo.priceMultiplier * levelInfo[i - 1].price) / 1000) * 1000;
            info.hatchTime = basicInfo.hatchTimeMultiplier * levelInfo[i - 1].hatchTime;

            levelInfo[i] = info;
        }
    }
}