using Assets.Logic.Scripts.Wrappers;
using System.IO;
using UnityEngine;

namespace Assets.Logic.Scripts
{

    public class WalkPointsJsonConverter : MonoBehaviour
    {
        private void Start()
        {
           // JsonConverter();
        }

        public void JsonConverter()
        {
            Transform[] transforms = GetComponentsInChildren<Transform>();

            ArrayWalkPointWrapper walkPoints = new ArrayWalkPointWrapper(transforms.Length);

            for(int i = 1; i < transforms.Length; i++)
            {
                WalkPointWrapper walkPointWrapper = new WalkPointWrapper
                {
                    x = transforms[i].transform.position.x,
                    z = transforms[i].transform.position.z,
                    index = i - 1
                };

                walkPoints.WalkPoints[i-1] = walkPointWrapper;
            }

            string jsonString = JsonUtility.ToJson(walkPoints);

            using (StreamWriter sw = File.CreateText("walkPoints.json"))
            {
                sw.Write(jsonString);
            }
        }
    }
}
