using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SequenceManager))]
public class SequenceGenerator : MonoBehaviour
{
    private SequenceManager sequenceManager;

    // === Sequence Events Management ===
    [SerializeField] private SequenceEventData[] sequenceEvents;
    private SequenceEventData currentSequenceEvent;
    private int currentSequenceIndex = 0;

    void Awake()
    {
        sequenceManager = GetComponent<SequenceManager>();
        sequenceManager.SequenceMatched += NextRound;

        CreateSequence();
    }

    private void NextRound()
    {
        if (currentSequenceIndex < sequenceEvents.Length)
        {
            CreateSequence();
        }
        else
        {
            GameManagerLevel3.instance.State = GameManagerLevel3.GameState.Finishing;
        }
    }

    private void CreateSequence()
    {
        currentSequenceEvent = sequenceEvents[currentSequenceIndex];

        sequenceManager.Size = currentSequenceEvent.NewSize;
        sequenceManager.Min = currentSequenceEvent.NewMin;
        sequenceManager.Max = currentSequenceEvent.NewMax;
        
        sequenceManager.ResetSequence();

        currentSequenceIndex++;
        GameManagerLevel3.instance.State = GameManagerLevel3.GameState.ShowingSequence;
    }
}