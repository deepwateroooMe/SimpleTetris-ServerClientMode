using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySquare.DataGram
{
    [Serializable]
    public class DataGram<T>
    {
        public string cmd;
        public T data;
    }
}
