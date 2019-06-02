using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine.UI;

public class ActivatePlayButton : MonoBehaviour
{
    public SimpleScrollSnap ScrollSnap;
    public Button PlayButton;

    public void ActivatePlay()
    {
        if (ScrollSnap.CurrentPanel == 1)
        {
            PlayButton.interactable = true;
        }
    }
}
