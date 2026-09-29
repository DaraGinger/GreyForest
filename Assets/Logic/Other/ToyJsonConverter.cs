using Assets.Logic.Scripts.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Logic.Other
{
    class ToyJsonConverter : MonoBehaviour
    {
        private void Start()
        {
            CreateJsonFile();    
        }

        private void CreateJsonFile()
        {
            Transform[] transforms = GetComponentsInChildren<Transform>();

            Debug.Log(transforms.Length);

            ArrayToyWrapper arrayWrapper = new ArrayToyWrapper(20);

            for (int i = 1; i < transforms.Length; i++)
            {
                ToyWrapper toyWrapper = new ToyWrapper();
                toyWrapper.x = transforms[i].transform.position.x;
                toyWrapper.z = transforms[i].transform.position.z;
                toyWrapper.y = transforms[i].transform.position.y;
                toyWrapper.yAngle = transforms[i].transform.eulerAngles.y;
                toyWrapper.xAngle = transforms[i].transform.eulerAngles.x;
                toyWrapper.zAngle = transforms[i].transform.eulerAngles.z;
                toyWrapper.index = i-1;
                arrayWrapper.Array[i-1] = toyWrapper;
            }

            string jsonString = JsonUtility.ToJson(arrayWrapper);

            using (StreamWriter sw = File.CreateText("toys.json"))
            {
                sw.Write(jsonString);
            }
        }
    }
}
