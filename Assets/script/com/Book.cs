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
    private List<PawnInfo> unlockedList = new List<PawnInfo>();

    public List<PawnInfo> PawnInfoList { get { return pawnInfoList; } }
    public List<PawnInfo> UnlockedList { get { return unlockedList; } }

    // Use this for initialization
    public void Init()
    {
        string filename = Application.dataPath + "/Resources/info/" + "fish_list.json";
        string txt = File.ReadAllText(filename);
        JsonReader reader = new JsonReader(txt);

        var people = JsonMapper.ToObject<JsonPawnInfo[]>(reader);

        foreach (var person in people)
        {
            var info = new PawnInfo();
            info.index = pawnInfoList.Count;
            info.name = person.name;
            info.rank = (PawnRank)Enum.Parse(typeof(PawnRank), person.rank, true);

            pawnInfoList.Add(info);
        }
    }

    public void Unlock(int index)
    {
        if(index >= pawnInfoList.Count)
        {
            Debug.LogError("Index {0} doesn't exist.");
            return;
        }

        foreach (var unlockedInfo in unlockedList)
        {
            if (unlockedInfo.index == index)
            {
                Debug.LogError("Index {0} already unlocked");
                return;
            }
        }

        unlockedList.Add(pawnInfoList[index]);
    }
}