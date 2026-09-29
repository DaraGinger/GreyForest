using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Close : MonoBehaviour
{
    [SerializeField]
    private InputActionReference Escape;

    void Update()
    {
        if (Escape.action.IsPressed())
            SceneManager.LoadScene("Scenes/Menu");
    }
}
