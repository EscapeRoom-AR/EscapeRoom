using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{

    public class Hint
    {
        private string hint;
        private Item item;

        public Item Item { get => item; set => item = value; }
        public string Hint { get => hint; set => hint = value; }
    }
}
