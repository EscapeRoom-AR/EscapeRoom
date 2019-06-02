using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public List<OrderedItem> items;

    private void Start()
    {
        items = new List<OrderedItem>();
    }

    public void ItemTapped(OrderedItem item, Success success)
    {
        if (items.Contains(item)) return;

        items.Add(item);

        if (items.Count == 3)
        {
            print("1:" + items[0].Tag + "2:" + items[1].Tag + "3:" + item.Tag);
            if (item.Tag == "third" && items[1].Tag == "second" && items[0].Tag == "first")
            {
                success();
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    item.AutoRemove();
                }
                items.Clear();
            }

        }
        
        item.Display();
    }

    public delegate void AutoRemove();
    public delegate void Display();
    public delegate void Success();

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
