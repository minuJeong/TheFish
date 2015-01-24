using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class Book
{
    private class JsonPawnInfo
    {
        public string name;
        public string rank;
    }

    private List<PawnInfo> pawnInfoList = new List<PawnInfo>();
    public List<PawnInfo> PawnInfoList { get { return pawnInfoList; } }

    // Use this for initialization
    public void Init()
    {
        string filename = Application.dataPath + "/Data/" + "fish_list.json";
        string txt = File.ReadAllText(filename);
        JsonReader reader = new JsonReader(txt);

        var people = JsonMapper.ToObject<JsonPawnInfo[]>(reader);

        foreach (var person in people)
        {
            var info = new PawnInfo();
            info.name = person.name;
            info.rank = (PawnRank)Enum.Parse(typeof(PawnRank), person.rank, true);

            pawnInfoList.Add(info);
        }
    }
}