using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class DialogManager : MonoBehaviour
{
    private static DialogManager _instance;

    public static DialogManager Instance()
    {
        if (_instance == null)
        {
            Debug.Log("Game instance should not null");
        }

        return _instance;
    }

    private UILabel label;
    private Dictionary<string, List<string>> dialogues = new Dictionary<string, List<string>>();
    private static float COOLTIME = 10.0f;
    private float displayDelay = COOLTIME;
    
    private void Start()
    {
        _instance = this;
        label = gameObject.GetComponent<UILabel>();
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        displayDelay -= Time.deltaTime;
        if (displayDelay <= 0.0f)
        {
            displayDelay += COOLTIME;
        }
        else
        {
            return;
        }

        ShowMessage("normal");
    }

    private void ShowMessage(string category)
    {
        if (dialogues.ContainsKey(category) == false)
        {
            return;
        }

        var list = dialogues[category];
        int index = Random.Range(0, list.Count - 1);
        string dialogue = list[index];

        label.text = dialogue;
    }

    private void Init()
    {
        string txt = Resources.Load<TextAsset>("info/Dialogue").text;
        var data = JsonMapper.ToObject(txt);
        foreach(string key in data.Keys)
        {
            dialogues.Add(key, new List<string>());

            var dialogueData = data[key];
            for (int j = 0; j < dialogueData.Count; ++j)
            {
                string dialogue = (string)dialogueData[j];
                dialogues[key].Add(dialogue);
            }
        }

        // Show initial message
        ShowMessage("normal");
    }    

    public void ShowSpecialMessage(string category)
    {
        displayDelay = COOLTIME;
        ShowMessage(category);
    }
}