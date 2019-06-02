using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService: MonoBehaviour
{
    private static AudioService instance = null;
    public static AudioService Instance { get { return instance; } }
    private AudioSource AudioSource;

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        AudioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Play()
    {
        AudioSource.Play();
    }

    public void Stop()
    {
        AudioSource.Stop();
    }

    public void Pause()
    {
        AudioSource.Pause();
    }
}
