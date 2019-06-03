using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public GameObject inventory;
    public bool IsOpen;

    private void Start()
    {
        IsOpen = false;
    }

    public void manageInventory()
    {
        FXService.Instance.PlayClick();
        IsOpen = !IsOpen;
        inventory.SetActive(IsOpen);
    }
}

  
