using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    private Text countdownValue;
    private int Countdown;
    private int Status;

    private void Start()
    {
        Status = 0;
        Countdown = 1800;
    }

    private void Update()
    {
        if (Countdown <= 0)
            timeUp();
        else
            CountDown();
    }

    public void NextPhase()
    {
        Status++;
    }

    public int GetStatus()
    {
        return Status;
    }

    public void CountDown()
    {
        Countdown--;
        countdownValue.text = countdownValue.ToString();
    }

    public void timeUp()
    {
        SceneManager.LoadScene("EndScene");
    }
}