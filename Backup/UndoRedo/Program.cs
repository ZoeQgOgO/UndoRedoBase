using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UndoRedo
{
    class Program
    {
        static void Main(string[] args)
        {
            IUndoable<string> stuff = new Undoable<string>("State One");
            stuff.SaveState();
            stuff.Value = "State Two";
            stuff.SaveState();
            stuff.Value = "State Three";

            stuff.Undo();   // State Two
            stuff.Undo();   // State One
            stuff.Redo();   // State Two
            stuff.Redo();   // State Three
        }
    }
}
