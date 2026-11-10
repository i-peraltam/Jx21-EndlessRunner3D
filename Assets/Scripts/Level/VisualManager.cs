using UnityEngine;

public class VisualManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Renderer playerRenderer;

    [SerializeField] private Color backgroundColor = Color.cyan;
    [SerializeField] private Color playerColor = Color.blue;

    void Start()
    {
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = backgroundColor;
        }

        if (playerRenderer != null)
        {
            playerRenderer.material.color = playerColor;
        }
    }
}