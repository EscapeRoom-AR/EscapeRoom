using Model;
using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneStart : MonoBehaviour
{
    public Text Score;
    public Text Title;
    private Game _game;
    public RESTService rest;
    public ModalService modal;

    void Start()
    {
        // RealGame
        //_game = new Game
        //{
        //    Time = Data.Score,
        //    HintsUsed = Data.HintsUsed,
        //    Room = Data.RoomCode
        //};

        //MockGame
        _game = new Game
        {
            Time = 7523,
            HintsUsed = 2,
            Room = 1
        };
        // End MockGame

        UploadGame();

        Title.text = Data.Win ? "Congratulations!" : "Time's up!";
        Score.text = "You scored " + Data.Score + " points!";
    }

    public void UploadGame()
    {
        rest.PostGame(_game, (resp) =>
         {
             if (resp.IsError())
                 modal.ShowModal(resp.message);
         });
    }
}
