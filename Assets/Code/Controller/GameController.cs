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
    public Text countdownValue;

    private int Countdown;
    private int Phase;
    private Dictionary<int, List<string>> Hints;
    private bool AreHintsAvailable;
    public RESTService restService;
    private bool PhaseHintShown;
    private int HintsUsed;

    private void Start()
    {
        PhaseHintShown = true;
        HintsUsed = 0;
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
        StartCoroutine(CountDown());
    }

    private void Update()
    {
         if (Countdown <= 0)
            GameOver();
    }

    public void NextPhase()
    {
        PhaseHintShown = false;
        Phase++;
    }

    public int GetPhase()
    {
        return Phase;
    }

  public IEnumerator CountDown()
    {
        while (Countdown > 0)
        {
            countdownValue.text = (Countdown / 60) + " : " + (Countdown % 60);
            yield return new WaitForSeconds(1);
            Countdown--;
        }


    }

    public void GameOver()
    {
        Data.HintsUsed = HintsUsed;
        Data.Time = 1800 - Countdown;
        SceneManager.LoadScene("EndScene");
    }

    public string GetHint()
    {
        if (AreHintsAvailable) {
            if (!PhaseHintShown) HintsUsed += 1;
            PhaseHintShown = true;
            return Hints[Phase][new Random().Next(0, Hints[Phase].Count)];
        } else return "Downloading hints...";
    }


}