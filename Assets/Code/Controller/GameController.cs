using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Random = System.Random;
using Services;
using Model;

public class GameController : MonoBehaviour
{
    private Text countdownValue;
    private int Countdown;
    private int Phase;
    private Dictionary<int, List<string>> Hints;
    private bool AreHintsAvailable;
    public RESTService restService;


    private void Start()
    {
        Phase = 0;
        Countdown = 1800;
        AreHintsAvailable = false;
        Hints = new Dictionary<int, List<string>>();
        restService.GetHints(1, apiResponse => {
            List<GameHint> hints = apiResponse.data;
            for (int i = 0; i < hints.Count; i++)
            {
                if (!Hints.ContainsKey(hints[i].Phase))
                    Hints[hints[i].Phase] = new List<string>();
                Hints[hints[i].Phase].Add(hints[i].Hint);
                print(hints[i].Hint);
            }
            AreHintsAvailable = true;
        });
        AudioService.Instance.Stop();
    }

    private void Update()
    {
        /* if (Countdown <= 0)
             timeUp();
         else
             CountDown();*/
    }

    public void NextPhase()
    {
        Phase++;
    }

    public int GetPhase()
    {
        return Phase;
    }

    public void CountDown()
    {
        Countdown--;
        countdownValue.text = countdownValue.ToString();
    }

    public void TimeUp()
    {
        SceneManager.LoadScene("EndScene");
    }

    public string GetHint()
    {
        if (AreHintsAvailable)
            return Hints[Phase][new Random().Next(0, Hints[Phase].Count)];
        else
            return "Downloading hints...";
    }
}