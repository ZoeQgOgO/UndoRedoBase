using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UndoRedo
{
    internal interface IUndoState<T>
    {
        T State { get; }

        void printState();
    }
}
