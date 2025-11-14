using System;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // === Events ===
    public event Action ObstacleHit;
    public event Action PowerupCollected;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            ObstacleHit?.Invoke();
        }
        else if (collision.CompareTag("Powerup"))
        {
            PowerupCollected?.Invoke();
        }
        else
        {
            Debug.LogWarning("El objeto con el que se ha colisionado no tiene un tag definido");
        }

        collision.gameObject.SetActive(false);
    }
}