using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MySquare.Utilities
{
    public class Serializer
    {
        public static byte[] Serialize(object obj)
        {
            byte[] r = null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    binaryFormatter.Serialize(ms, obj);
                    r = new byte[ms.Length];
                    ms.Seek(0, SeekOrigin.Begin);
                    int c = ms.Read(r, 0, r.Length);
                }
                catch
                { }
            }
            return r;
        }
        public static T Deserialize<T>(byte[] buf) where T : class
        {
            T r = default(T);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(buf, 0, buf.Length);
                ms.Seek(0, SeekOrigin.Begin);
                try
                {
                    r = binaryFormatter.Deserialize(ms) as T;
                }
                catch
                { }
            }
            return r;
        }
    }
}
