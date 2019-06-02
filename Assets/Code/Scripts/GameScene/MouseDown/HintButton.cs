using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintButton : MonoBehaviour
{
    public ModalService ModalService;
    public GameController GameController;
    private bool IsModalOpen;

    void Start()
    {
        IsModalOpen = false;
    }

    public void OnClick()
    {
        if (!IsModalOpen)
        {
            ModalService.ShowModal(GameController.GetHint());
        }
            
    }
}
