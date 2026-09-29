using System;
using System.IO;
using UnityEngine;

namespace Assets.Logic.Scripts.Wrappers
{
    [Serializable]
    class ArrayWalkPointWrapper
    {
        public ArrayWalkPointWrapper(int length)
        {
            WalkPoints = new WalkPointWrapper[length];
        }

        public WalkPointWrapper[] WalkPoints;

        public void ReadJson(string fileName)
        {
            string json = "";

            using (StreamReader sw = File.OpenText(fileName))
            {
                json = sw.ReadToEnd();
            }

            WalkPoints = JsonUtility.FromJson<ArrayWalkPointWrapper>(json).WalkPoints;
        }
    }
}
