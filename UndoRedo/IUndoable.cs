using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UndoRedo
{
    public interface IUndoable<T>
    {
        bool CanRedo { get; }
        bool CanUndo { get; }
        T Value { get; set; }
        void SaveState();
        void Undo();
        void Redo();
    }
}
