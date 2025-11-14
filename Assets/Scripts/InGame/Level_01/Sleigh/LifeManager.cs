using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CollisionDetector))]
public class LifeManager : MonoBehaviour
{
    // === ===
    private CollisionDetector collisionDetector;

    // === ===
    [SerializeField] private GameObject[] livesUI;
    private int lifeCounter;

    // === Properties ===
    public int LifeCounter => lifeCounter;

    void Awake()
    {
        collisionDetector = GetComponent<CollisionDetector>();

        collisionDetector.ObstacleHit += SubstractLife;

        lifeCounter = livesUI.Count();
    }

    public void AddLife()
    {
        livesUI.LastOrDefault(life => life.activeSelf == false).SetActive(true);
        lifeCounter++;
    }

    private void SubstractLife()
    {
        if (lifeCounter > 0)
        {
            GameObject lostLife = livesUI.LastOrDefault(life => life.activeSelf == true);
            lostLife.SetActive(false);
            lifeCounter--;
        }
    }

    private void ResetLives()
    {
        foreach (var life in livesUI)
        {
            life.SetActive(true);
        }

        lifeCounter = livesUI.Count();
    }
}