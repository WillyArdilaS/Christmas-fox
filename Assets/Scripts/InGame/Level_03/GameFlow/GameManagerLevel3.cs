using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManagerLevel3 : MonoBehaviour
{
    // === Singleton ===
    public static GameManagerLevel3 instance;

    // === Managers ===
    private GameObject mapManager;

    // === States ===
    public enum GameState { ShowingSequence, Playing, InPause, Finishing }
    [SerializeField] private GameState gameState;

    // === Player ===
    [SerializeField] private FoxController foxController;

    // === Tree ===

    // === Properties ===
    public GameState State { get => gameState; set => gameState = value; }
    public GameObject MapManager => mapManager;

    void Awake()
    {
        Time.timeScale = 1;

        // Singleton
        if (instance == null)
        {
            instance = this;
            InitializeManagers();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        foxController.CanMove = gameState == GameState.Playing;

        if (gameState == GameState.Finishing) FinishGame();
    }

    private void InitializeManagers()
    {
        if (mapManager == null) mapManager = transform.Find("MapManager").gameObject;
    }

    private void FinishGame()
    {
        Debug.Log("Juego finalizado...");
        Time.timeScale = 0;
    }
}