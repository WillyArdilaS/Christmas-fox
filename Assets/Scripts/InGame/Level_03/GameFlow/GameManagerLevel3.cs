using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManagerLevel3 : MonoBehaviour
{
    // === Singleton ===
    public static GameManagerLevel3 instance;

    // === Managers ===
    private GameObject sequenceManager;
    private GameObject mapManager;
    private GameObject lightManager;
    private GameObject treeManager;

    // === States ===
    public enum GameState { ShowingSequence, ShowingTree, Playing, InPause, Finishing }
    [SerializeField] private GameState gameState;

    // === Camera ===
    private CameraFollow cameraFollow;

    // === Player ===
    [SerializeField] private FoxController foxController;

    // === Tree ===
    private TreeAnimator treeAnimator;

    // === Events ===
    public event Action RoundFinished;

    // === Properties ===
    public GameState State { get => gameState; set => gameState = value; }
    public GameObject SequenceManager => sequenceManager;
    public GameObject MapManager => mapManager;
    public GameObject LightManager => lightManager;
    public GameObject TreeManager => treeManager;

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

        // Initialization
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        treeAnimator = treeManager.GetComponent<TreeAnimator>();

        treeAnimator.LightedTree += ResetRound;
    }

    void Update()
    {
        foxController.CanMove = gameState == GameState.Playing;

        if (gameState == GameState.Finishing) FinishGame();
    }

    private void InitializeManagers()
    {
        if (sequenceManager == null) sequenceManager = transform.Find("SequenceManager").gameObject;
        if (mapManager == null) mapManager = transform.Find("MapManager").gameObject;
        if (lightManager == null) lightManager = transform.Find("LightManager").gameObject;
        if (treeManager == null) treeManager = transform.Find("TreeManager").gameObject;
    }

    private void ResetRound()
    {
        RoundFinished?.Invoke();

        if (gameState == GameState.Finishing) return;

        cameraFollow.ResetPosition();
        foxController.ResetPosition();
        lightManager.GetComponent<LightManager>().ResetActiveLights();
        treeAnimator.TurnOffTree();
    }

    private void FinishGame()
    {
        Debug.Log("Juego finalizado...");
        Time.timeScale = 0;
    }
}