using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour
{
    public VideoPlayer player;

    void Start()
    {
        InvokeRepeating("checkOver", .1f, .1f);
    }

    private void checkOver()
    {
        if (player.frame >= ((long)player.frameCount - 1))
            Finished();
    }

    public void Finished()
    {
        FXService.Instance.PlayClick();
        SceneManager.LoadScene("ScapeRoomScene");
        CancelInvoke("checkOver");
    }
}
