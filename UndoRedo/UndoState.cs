using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace UndoRedo
{
    internal class UndoState<T> : IUndoState<T>
    {
        BinaryFormatter _formatter;
        byte[] _stateData;

        internal UndoState(T state)
        {
            _formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                _formatter.Serialize(stream, state);
                _stateData = stream.ToArray();
            }
        }

        public T State
        {
            get
            {
                using (MemoryStream stream = new MemoryStream(_stateData))
                {
                    return (T)_formatter.Deserialize(stream);
                }
            }
        }

        public void printState()
        {
            foreach(var state in _stateData)
            {
                Console.Write(state);
            }
        }
    }
}
