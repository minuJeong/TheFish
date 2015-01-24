using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance()
    {
        if (_instance == null)
        {
            Debug.Log("Game instance should not null");
        }

        return _instance;
    }

    void Start()
    {
        _instance = this;
    }

    public void Play(string soundName)
    {
        var child = transform.FindChild(soundName);
        if (child == null)
        {
            return;
        }

        child.gameObject.GetComponent<AudioSource>().audio.Play();
    }
}
