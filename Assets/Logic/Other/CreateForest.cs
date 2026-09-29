using Assets.Logic.Scripts;
using System.IO;
using UnityEngine;

public class CreateForest : MonoBehaviour
{
    [SerializeField]
    public GameObject[] gameObjects = new GameObject[4];

    private Terrain terrain;
    // Start is called before the first frame update
    void Start()
    {
        terrain = Terrain.activeTerrain;
        CreateForestFromJson();
    }

    private void CreateForestFromJson()
    {
        ArrayTreeWrapper listTrees = new ArrayTreeWrapper(terrain.terrainData.treeInstances.Length);
        listTrees.ReadJson("trees.json");

        foreach (var tree in listTrees.Array)
        {
            GameObject gameObject = Instantiate(gameObjects[tree.prototypeIndex], new Vector3(tree.x, tree.y, tree.z), new Quaternion(0, 0, 0, 0));
            gameObject.transform.localScale = new Vector3(tree.widthScale, tree.heightScale, tree.widthScale);
        }
    }

    private void CreateJsonFile()
    {
        var trees = terrain.terrainData.treeInstances;
        string jsonString = "{\"Trees\":[";
        foreach (var tree in trees)
        {
            Vector3 localPos = new Vector3(
                tree.position.x * terrain.terrainData.size.x,
                tree.position.y * terrain.terrainData.size.y,
                tree.position.z * terrain.terrainData.size.z
            );

            Vector3 worldPos = terrain.transform.position + localPos;

            var item = new TreeWrapper
            {
                x = worldPos.x,
                y = worldPos.y,
                z = worldPos.z,
                heightScale = tree.heightScale,
                widthScale = tree.widthScale,
                prototypeIndex = tree.prototypeIndex
            };

            jsonString += JsonUtility.ToJson(item) + ",";
        }

        jsonString += "]}";

        using (StreamWriter sw = File.CreateText("trees.json"))
        {
            sw.Write(jsonString);
        }
    }
}
