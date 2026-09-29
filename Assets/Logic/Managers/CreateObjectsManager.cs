using Assets.Logic.Scripts;
using Assets.Logic.Scripts.Wrappers;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class CreateObjectsManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text sumOfToys;

    [SerializeField]
    private TMP_Text ñounter;

    [SerializeField]
    private TMP_Text pressC;

    [SerializeField]
    private GameObject toyPrefab;

    void Start()
    {
        TextManager.TextManagerInstance.CounterText = ñounter;
        TextManager.TextManagerInstance.PressC = pressC;
        sumOfToys.text = '/'+GameInfo.Instance.SumOfToys.ToString();
        CreateToysFromJson(GameInfo.Instance.SumOfToys);
    }

    private void CreateToysFromJson(int sumOfToys)
    {
        ArrayToyWrapper listTrees = new ArrayToyWrapper(sumOfToys);
        listTrees.ReadJson("toys.json");

        float scale = 0.3f;

        Debug.Log(sumOfToys);

        foreach (var toy in listTrees.Array)
        {
            GameObject gameObject = Instantiate(toyPrefab, new Vector3(toy.x, toy.y, toy.z), new Quaternion(0, 0, 0, 0));
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
            gameObject.transform.Rotate(new Vector3(toy.xAngle, toy.yAngle, toy.zAngle));
        }
    }
}
