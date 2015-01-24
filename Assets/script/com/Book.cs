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
        public int index;
        public string name;
        public string rank;
    }

    private Dictionary<int, PawnInfo> pawnInfoList = new Dictionary<int, PawnInfo>();
    private Dictionary<int, PawnInfo> unlockedList = new Dictionary<int, PawnInfo>();

    private string dbFilename;

    public Dictionary<int, PawnInfo> PawnInfoList { get { return pawnInfoList; } }
    public Dictionary<int, PawnInfo> UnlockedList { get { return unlockedList; } }

    private void LoadStaticData()
    {
        string txt = Resources.Load<TextAsset>("info/fish_list").text;
        JsonReader reader = new JsonReader(txt);

        var people = JsonMapper.ToObject<JsonPawnInfo[]>(reader);

        foreach (var person in people)
        {
            var info = new PawnInfo();
            info.index = person.index;
            info.name = person.name;
            info.rank = (PawnRank)Enum.Parse(typeof(PawnRank), person.rank, true);

            pawnInfoList.Add(info.index, info);
        }
    }

    private void LoadDB()
    {
        // DB data
        dbFilename = Application.persistentDataPath + "/db/" + "fish_unlocked.json";
        if (File.Exists(dbFilename) == false)
        {
            return;
        }

        var txt = File.ReadAllText(dbFilename);
        if (txt.Length != 0)
        {
            var indices = JsonMapper.ToObject<int[]>(txt);
            foreach (var index in indices)
            {
                PawnInfo info;
                if (pawnInfoList.TryGetValue(index, out info) == false)
                {
                    Debug.LogError("Index " + index + "doesn't exist.");
                    continue;
                }

                if (unlockedList.ContainsKey(index) == true)
                {
                    Debug.LogError("Index " + index + " already unlocked.");
                    continue;
                }

                unlockedList.Add(index, info);
            }
        }
    }

    private void SaveToDB()
    {
        string dbStr = "[";

        foreach (var pair in unlockedList)
        {
            var info = pair.Value;
            dbStr += info.index + ", ";
        }

        dbStr += "]";

        File.WriteAllText(dbFilename, dbStr);
    }

    // Use this for initialization
    public void Init()
    {
        LoadStaticData();
        LoadDB();
    }

    public void Unlock(int index)
    {
        PawnInfo info;
        if (pawnInfoList.TryGetValue(index, out info) == false)
        {
            Debug.LogError("Index " + index + " doesn't exist.");
            return;
        }

        if (unlockedList.ContainsKey(index) == true)
        {
            Debug.LogError("Index " + index + " already unlocked");
            return;
        }

        SoundManager.Instance().Play("book_register");
        unlockedList.Add(index, info);

        SaveToDB();
    }
}