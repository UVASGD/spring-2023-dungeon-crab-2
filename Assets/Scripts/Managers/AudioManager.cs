using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

// This script is for the Audio Manager, the prefab that handles playing sounds and music. Notably, this is directly from Brackey's video on Sound in Unity.
// For now, this also has the logic for the theme music playing- I've made the volume 0 on these for testing purposes, and we might swap them out for new stuff later
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    public bool muted = false;
    private bool wait = false;
    private bool onTheme2 = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    void Start()
    {
        //Play("Theme1");        //uncomment this to play Theme song once we have a theme song lol (must add the song to the prefab in the inspector to start)
    }
    private void Update()
    {
        //functionality to mute the music when pressing m- uncomment when theme is implemented
        if (Input.GetKeyDown(KeyCode.M) && !wait)
        {
            wait = true;
            if (!muted)
            {
                muted = true;
                if (onTheme2)
                {
                    Stop("Theme2");
                }
                else
                {
                    Stop("Theme1");
                }
                
            }
            else
            {
                muted = false;
                if (onTheme2)
                {
                    Play("Theme2");
                }
                else
                {
                    Play("Theme1");
                }
                
            }
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            wait = false;
        }
        if (Input.GetKeyDown(KeyCode.N) && !wait)
        {
            switchTheme();
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
        }
    }

    public void PlayTheme(string name)
    {
        Stop("Theme1");
        Stop("Theme2");
        Stop("Ocean");
        if (!muted && name != "None")
        {
            Play(name);
        }
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("sound: " + name + "not found.");
            return;
        }
        s.source.Play();
    }

    public void switchTheme()
    {
        if (onTheme2)
        {
            if (!muted)
            {
                Stop("Theme2");
                Play("Theme1");
            }
            onTheme2 = false;
        }
        else
        {
            if (!muted)
            {
                Stop("Theme1");
                Play("Theme2");
            }
            onTheme2 = true;
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("sound: " + name + "not found.");
            return;
        }
        s.source.Stop();
    }
}
