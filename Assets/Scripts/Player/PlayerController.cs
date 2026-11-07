using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 4f;
    [SerializeField] private float jumpForce = 6f;

    private Rigidbody rb;

    private bool touchLeft;
    private bool touchRight;
    private bool jumpRequested;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame ||
                Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                jumpRequested = true;
            }
        }
    }

    void FixedUpdate()
    {
        bool keyboardLeft = false;
        bool keyboardRight = false;

        if (Keyboard.current != null)
        {
            keyboardLeft =
                Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed;

            keyboardRight =
                Keyboard.current.dKey.isPressed ||
                Keyboard.current.rightArrowKey.isPressed;
        }

        float horizontal = 0f;

        if (keyboardLeft || touchLeft)
        {
            horizontal = -1f;
        }

        if (keyboardRight || touchRight)
        {
            horizontal = 1f;
        }

        rb.linearVelocity = new Vector3(
            horizontal * horizontalSpeed,
            rb.linearVelocity.y,
            forwardSpeed
        );

        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }
    }

    public void IzquierdaPresionada()
    {
        touchLeft = true;
    }

    public void IzquierdaSoltada()
    {
        touchLeft = false;
    }

    public void DerechaPresionada()
    {
        touchRight = true;
    }

    public void DerechaSoltada()
    {
        touchRight = false;
    }

    public void Saltar()
    {
        jumpRequested = true;
    }
}