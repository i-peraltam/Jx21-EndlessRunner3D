using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 4f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float speedIncreaseRate = 0.15f;
    [SerializeField] private float maxForwardSpeed = 12f;

    private Rigidbody rb;

    private bool touchLeft;
    private bool touchRight;
    private bool jumpRequested;
    private TrailRenderer trailRenderer;
    
    private bool turboActivo = false;

    private Renderer playerRenderer;
    private Color colorOriginal;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();

        playerRenderer = GetComponent<Renderer>();

        if (playerRenderer != null)
        {
            colorOriginal = playerRenderer.material.color;
        }

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

        forwardSpeed += speedIncreaseRate * Time.fixedDeltaTime;
        forwardSpeed = Mathf.Min(forwardSpeed, maxForwardSpeed);

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

    public void ActivarTurbo(float multiplicador, float duracion)
    {
        if (!turboActivo)
        {
            StartCoroutine(TurboCoroutine(multiplicador, duracion));
        }
    }

    private System.Collections.IEnumerator TurboCoroutine(float multiplicador, float duracion)
    {
        turboActivo = true;

        float velocidadAntesDelTurbo = forwardSpeed;

        forwardSpeed = velocidadAntesDelTurbo * multiplicador;

        if (trailRenderer != null)
        {
            trailRenderer.emitting = true;
        }

        if (playerRenderer != null)
        {
            playerRenderer.material.color = Color.magenta;
        }

        yield return new WaitForSeconds(Mathf.Min(duracion, 3f));

        forwardSpeed = velocidadAntesDelTurbo;

        if (trailRenderer != null)
        {
            trailRenderer.emitting = false;
        }

        if (playerRenderer != null)
        {
            playerRenderer.material.color = colorOriginal;
        }

        turboActivo = false;
    }
}