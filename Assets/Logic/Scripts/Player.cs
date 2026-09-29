using Assets.Enumerables;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform orientation;

    [SerializeField]
    private InputActionReference moveAction;

    [SerializeField]
    private InputActionReference runAction;

    private CharacterController characterController;

    private AudioSource audioSource;

    private float speed = GameInfo.Instance.PlayerSpeed;

    private  float runningSpeed = GameInfo.Instance.PlayerRunningSpeed;

    private float staminaCapacity = GameInfo.Instance.PlayerStaminaCapacity;

    private float timeStamina = GameInfo.Instance.PlayerStaminaCapacity;

    private float gravity = -9.81f;

    private float velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.rotation = orientation.rotation;

        var action = moveAction.action.ReadValue<Vector2>();

        var movementVector = orientation.forward * action.y + orientation.right * action.x;

        Gravity();

        movementVector.y = velocity;

        if (action.y != 0f || action.x != 0f)
        {
            characterController.Move(movementVector.normalized * speed);

            if(!audioSource.isPlaying)
                 audioSource.Play();

            Stamina();

            if (GameInfo.Instance.DifficultyType != DifficultyType.Hard)
            {
                StaminaRecharging();
            }

        }
        else if(action.y == 0f && action.x == 0f)
        {
            audioSource.Pause();
            StaminaRecharging();
        }
    }

    private void Stamina()
    {
        if (runAction.action.IsPressed() && timeStamina > 0)
        {
            timeStamina -= 0.01f;
            speed = runningSpeed;
            audioSource.pitch = 0.9f;
        }
        else
        {
            speed = GameInfo.Instance.PlayerSpeed;
            audioSource.pitch = 0.75f;
        }
    }

    private void StaminaRecharging()
    {
        if (!runAction.action.IsPressed())
        {
            speed = GameInfo.Instance.PlayerSpeed;

            if (timeStamina < staminaCapacity)
            {
                timeStamina += 0.01f;
            }
        }
    }

    private void Gravity()
    {
        if (characterController.isGrounded && velocity < 0.0f)
        {
            velocity = -0.1f;

        }
        else
            velocity += gravity * Time.deltaTime;
    }    
}
