using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public List<OrderedItem> items;
    private int amountOfSuccess;

    private void Start()
    {
        items = new List<OrderedItem>();
        amountOfSuccess = 0;
    }

    public void ItemTapped(OrderedItem item, Success success, Complete complete)
    {
        if (items.Contains(item)) { return; }

        items.Add(item);

        if (items.Count == 3)
        {
            if (items[2].Tag == "third" && items[1].Tag == "second" && items[0].Tag == "first")
            {
                success();
                amountOfSuccess += 1;
                if (amountOfSuccess == 2) complete();
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].AutoRemove();
                }
                items.Clear();
            }

        } else
        
        item.Display();
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
