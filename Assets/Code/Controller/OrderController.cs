using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public List<OrderedItem> items;
    private int amountOfSuccess;
    private bool PaintingActive;

    private void Start()
    {
        items = new List<OrderedItem>();
        amountOfSuccess = 0;
        PaintingActive = true;
    }

    public void ItemTapped(OrderedItem item, Success success, Complete complete, bool isPainting)
    {
        if (PaintingActive) {
            if (!isPainting)
            {
                Clearitems();
                PaintingActive = isPainting;
            }
                
        } else {
            if (isPainting)
            {
                Clearitems();
                PaintingActive = isPainting;
            }
               
        }

        if (items.Contains(item)) return;

        items.Add(item);

        if (items.Count >= 3)
        {
            if (items[2].Tag == "third" && items[1].Tag == "second" && items[0].Tag == "first")
            {
                amountOfSuccess += 1;
                if (amountOfSuccess == 2)
                {
                    Clearitems();
                    complete();
                }
                success();
            }
            else Clearitems();
        } else item.Display();
    }

    private void Clearitems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].AutoRemove();
        }
        items.Clear();
    }

    public delegate void AutoRemove();
    public delegate void Display();
    public delegate void Success();
    public delegate void Complete();

    public class OrderedItem
    {
        public string Tag;
        public AutoRemove AutoRemove;
        public Display Display;

        public OrderedItem(string tag, Display display, AutoRemove autoRemove)
        {
            Tag = tag;
            AutoRemove = autoRemove;
            Display = display;
        }
        

    }
}
