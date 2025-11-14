using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class SleighController : MonoBehaviour
{
    // === Input ===
    private PlayerInput playerInput;

    // === Positions ===
    [SerializeField] private Transform[] positions;
    [SerializeField]  private int currentPositionIndex;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.onActionTriggered += OnActionTriggered;

        positions = positions.OrderBy(road => road.transform.position.x).ToArray();
        
        currentPositionIndex = Array.FindIndex(positions, position => Mathf.Approximately(position.position.x, transform.position.x));
        if (currentPositionIndex == -1)
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
                default:
                    Debug.LogWarning($"La acción '{ctx.action.name}' no existe en el action map '{ctx.action.actionMap.name}'");
                    break;
            }
        } 
    }

    private void ChangeTrack(int direction)
    {
        if ((direction == -1 && currentPositionIndex > 0) || (direction == 1 && currentPositionIndex < positions.Length - 1))
        {
            currentPositionIndex += direction;
            transform.position = new Vector2(positions[currentPositionIndex].position.x, transform.position.y);
            //Debug.Log(positions[currentPositionIndex].localPosition.x);
        }
    }
}