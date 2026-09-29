using Assets.Logic.Scripts;
using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Toy : MonoBehaviour
{
    private bool inArea = false;

    [SerializeField]
    private InputActionReference PickUp;

    [SerializeField]
    private GameObject prefab;

    private GameObject gateCamera;

    public void Start()
    {
        var fence = GameObject.Find("Fence");
        gateCamera = fence.transform.Find("GateCamera").GameObject();
    }

    public void OnTriggerEnter(Collider other)
    {
        inArea = true;
        TextManager.TextManagerInstance.PressC.gameObject.SetActive(true);
    }

    void Update()
    {
        if (inArea && PickUp.action.IsPressed())
        {
            GameInfo.Instance.CollectToy();

            TextManager.TextManagerInstance.CounterText.text = GameInfo.Instance.CollectedToys.ToString();

            Destroy(prefab);

            TextManager.TextManagerInstance.PressC.gameObject.SetActive(false);

            if (GameInfo.Instance.CollectedToys == GameInfo.Instance.SumOfToys && !gateCamera.activeSelf)
            {
                OpenGate();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        inArea = false;
        TextManager.TextManagerInstance.PressC.gameObject.SetActive(false);
    }

    private void OpenGate()
    {
        gameObject.SetActive(false);
        gateCamera.SetActive(true);

        GameObject gate1 = GameObject.Find("Fence/Gates/Gate1");
        OpenGate openGate1 = gate1.GetComponent<OpenGate>();
        openGate1.enabled = true;

        GameObject gate2 = GameObject.Find("Fence/Gates/Gate2");
        OpenGate openGate2 = gate2.GetComponent<OpenGate>();
        openGate2.enabled = true;
    }
}
