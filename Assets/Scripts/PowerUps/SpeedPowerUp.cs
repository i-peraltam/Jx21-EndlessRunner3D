using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1.8f;
    [SerializeField] private float duration = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.ActivarTurbo(speedMultiplier, duration);
            }

            Destroy(gameObject);
        }
    }
}
