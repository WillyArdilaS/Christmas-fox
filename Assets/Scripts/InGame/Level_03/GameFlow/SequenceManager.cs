using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    // === Managers ===
    [SerializeField] private LightManager lightManager;
    
    // === Sequence ===
    [SerializeField] private int size;
    [SerializeField] private int min;
    [SerializeField] private int max;
    [SerializeField] private List<int> sequenceList = new();

    void Awake()
    {
        lightManager.ActiveLightsUpdated += CompareSequence;

        CreateSequence();
    }

    private void CreateSequence()
    {
        int randomNumber = Random.Range(min, max + 1);

        for (int i = 0; i < size; i++)
        {
            while (sequenceList.Contains(randomNumber))
            {
                randomNumber = Random.Range(min, max + 1);
            }

            sequenceList.Add(randomNumber);
        }
    }

    public void ResetSequence()
    {
        sequenceList.Clear();
        CreateSequence();
    }

    private void CompareSequence(List<int> activeLights)
    {
        bool isEqual = sequenceList.SequenceEqual(activeLights);
        Debug.Log(isEqual);
    }
}