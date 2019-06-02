using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneStart : MonoBehaviour
{
    public Text Score;
    public Text Title;


    void Start()
    {
        Title.text = Data.win ? "Congratulations!" : "Time's up!";
        Score.text = "You scored " + Data.score + " points!";        
    }
}
