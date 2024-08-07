using UnityEngine;

namespace KC
{
    public class Item : ScriptableObject
    {
        [Header("Item information")]
        public string itemName;
        public Sprite itemIcon;
        [TextArea] public string itemDescription;
        public int itemID;
    }
}
