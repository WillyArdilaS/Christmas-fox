using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class SleighController : MonoBehaviour
{
    // === Input ===
    private PlayerInput playerInput;

    // === Tracks ===
    [SerializeField] private GameObject[] tracks;
    private int currentTrackIndex;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.onActionTriggered += OnActionTriggered;

        tracks = tracks.OrderBy(road => road.transform.position.x).ToArray();
        
        currentTrackIndex = Array.FindIndex(tracks, track => Mathf.Approximately(track.transform.position.x, transform.position.x));
        if (currentTrackIndex == -1)
        {
            Debug.LogWarning($"No se encontró un road con la misma posición X que {gameObject.name}");
        }
    }

    private void OnActionTriggered(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            switch (ctx.action.name)
            {
                case "Move Left":
                    ChangeTrack(-1);
                    break;
                case "Move Right":
                    ChangeTrack(1);
                    break;
                case "Jump / Go Inside":
                    break;
            }
        } 
    }

    private void ChangeTrack(int direction)
    {
        if ((direction == -1 && currentTrackIndex > 0) || (direction == 1 && currentTrackIndex < tracks.Length - 1))
        {
            currentTrackIndex += direction;
            transform.position = new Vector2(tracks[currentTrackIndex].transform.position.x, transform.position.y);
        }
    }
}