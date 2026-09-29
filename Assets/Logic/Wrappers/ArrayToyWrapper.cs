using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Logic.Scripts.Wrappers
{
    [Serializable]
    class ArrayToyWrapper
    {
        public ToyWrapper[] Array;

        public ArrayToyWrapper(int length)
        {
            Array = new ToyWrapper[length];
        }

        public void ReadJson(string fileName)
        {
            string json = "";

            using (StreamReader sw = File.OpenText(fileName))
            {
                json = sw.ReadToEnd();
            }

            var info = JsonUtility.FromJson<ArrayToyWrapper>(json).Array.Take(Array.Length);

            Array = info.ToArray();
        }
    }
}
