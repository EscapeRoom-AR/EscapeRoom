﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    private List<InteractiveItem> itemsInventory;
    public GameObject InventoryObject;
    public List<Image> itemsImages;

    public void Start()
    {
        itemsInventory = new List<InteractiveItem>();
        itemsImages = new List<Image>();
        for (int i = 1; i <= 5; i++)
        {
            itemsImages.Add(InventoryObject.transform
                .Find("inventorySlot" + i)
                .Find("border")
                .Find("itemImage")
                .GetComponent<Image>()
            );
        }
        
    }

    public void ButtonTapped(int index)
    {
        if (index < itemsInventory.Count)
            itemsInventory[index].Callback();
    }



    public void RemoveItemsWithTag(string tag)
    {
        for (int i = 0; i < itemsInventory.Count; i++)
        {
            if (itemsInventory[i].Tag.Equals(tag)) {
                itemsInventory.Remove(itemsInventory[i]);
            }
        }
        UpdateUI();
    }

    public void AddToInventory(InteractiveItem item) {
        itemsInventory.Add(item);
        UpdateUI();
    }

    public void removeFromInventory(InteractiveItem item) {
        itemsInventory.Remove(item);
        UpdateUI();
    }

    private void UpdateUI()
    {
       for (int i = 0; i < itemsImages.Count; i++)
        {
            if (i < itemsInventory.Count)
                itemsImages[i].sprite = itemsInventory[i].Sprite;
            else
                itemsImages[i].sprite = null;
        }
    }

}
