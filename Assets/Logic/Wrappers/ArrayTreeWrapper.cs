using System;
using System.IO;
using UnityEngine;

namespace Assets.Logic.Scripts
{
    [Serializable]
    class ArrayTreeWrapper
    {
        public TreeWrapper[] Array;

        public ArrayTreeWrapper(int length)
        {
            this.Array = new TreeWrapper[length];
        }

        public void ReadJson(string fileName)
        {
            string json = "";

            using (StreamReader sw = File.OpenText(fileName))
            {
                json = sw.ReadToEnd();
            }

            var info = JsonUtility.FromJson<ArrayTreeWrapper>(json);
            Array = info.Array;
        }
    }
}
