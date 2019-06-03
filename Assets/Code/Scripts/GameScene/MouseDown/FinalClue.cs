﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalClue : MonoBehaviour
{
    private GameObject modal;
    private bool isModalShown;
    public GameObject modalPrefab;
    public Canvas canvas;
    public Inventory inventory;
    public Sprite sprite;

    public void Start()
    {
        isModalShown = false;
    }
    private void OnMouseDown()
    {
        inventory.AddToInventory(new InteractiveItem(sprite, "finalClue", () => ShowModal()));
        gameObject.SetActive(false);
    }


    private void ShowModal()
    {
        if (isModalShown)
            return;
        isModalShown = true;
        modal = Instantiate(modalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        modal.transform.SetParent(canvas.transform, false);
        modal.transform.Find("Content").Find("Image").GetComponent<Image>().sprite = sprite;
        modal.transform.Find("ButtonHolder").Find("Button").GetComponent<Button>().onClick.AddListener(() => {
            Destroy(modal);
            isModalShown = false;
        });
    }
}
