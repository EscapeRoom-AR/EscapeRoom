using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveItem 
{
    public Sprite Sprite;
    public string Tag;
    public InventoryTapCallback Callback;

    public InteractiveItem(Sprite sprite, string tag, InventoryTapCallback callback)
    {
        Sprite = sprite;
        Tag = tag;
        Callback = callback;
    }

    public delegate void InventoryTapCallback();
}
