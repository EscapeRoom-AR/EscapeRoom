using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXService : MonoBehaviour
{
    private static FXService instance = null;
    public static FXService Instance { get { return instance; } }
    private AudioSource AudioSource;
    public AudioClip click;
    public AudioClip chest;
    public AudioClip door;
    public AudioClip saveItem;
    public AudioClip success;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        AudioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        AudioSource.clip = clip;
        AudioSource.Play();
    }

    public void PlayClick()
    {
        AudioSource.clip = click;
        AudioSource.Play();
    }

    public void PlayOpenChest()
    {
        AudioSource.clip = chest;
        AudioSource.Play();
    }

    public void PlayOpenDoor()
    {
        AudioSource.clip = door;
        AudioSource.Play();
    }

    public void PlaySaveItem()
    {
        AudioSource.clip = saveItem;
        AudioSource.Play();
    }

    public void PlaySuccess()
    {
        AudioSource.clip = success;
        AudioSource.Play();
    }
}
