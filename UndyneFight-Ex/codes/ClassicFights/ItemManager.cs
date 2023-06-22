using System;

namespace UndyneFight_Ex.Fight
{
    public class GameAction
    {
        public GameAction(string name, Action usingEvent)
        {
            Name = name;
            UsingEvent = usingEvent;
        }
        public string Name { internal get; set; }
        public Action UsingEvent { internal get; set; }
    }
    public class Item
    {
        public Item(string name, Action usingEvent)
        {
            Name = name;
            UsingEvent = usingEvent;
        }
        public string Name { internal get; set; }
        public Action UsingEvent { internal get; set; }
        public bool IsConsumable { internal get; set; } = true;
    }
}