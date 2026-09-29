using System.Collections;
using UnityEngine;

public class OpenGate : MonoBehaviour
{
    [SerializeField]
    private float angle;

    [SerializeField]
    private float deltaRotate;

    [SerializeField]
    private GameObject MainCamera;

    [SerializeField]
    private GameObject GateCamera;

    void Update()
    {
        StartCoroutine(RotateGate());
    }

    private IEnumerator RotateGate()
    {
        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, deltaRotate);

        yield return StartCoroutine(Coroutine(5));

        DeactivateScript();
    }

    private IEnumerator Coroutine(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }

    private void DeactivateScript()
    {
        MainCamera.SetActive(true);
        GateCamera.SetActive(false);

        var ascript = GetComponent<OpenGate>();
        ascript.enabled = false;
    }
}
