using Assets.Scripts;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject flashlight;

    [SerializeField]
    private Transform orientation;

    private float cursorSensitivity = GameInfo.Instance.CursorSensitivity;

    private float rotationX = 1.0f;
    private float rotationY = 1.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        var mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * cursorSensitivity;
        var mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * cursorSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        orientation.rotation = Quaternion.Euler(0, rotationY, 0);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        flashlight.transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);

        transform.position = orientation.position;
    }
}
