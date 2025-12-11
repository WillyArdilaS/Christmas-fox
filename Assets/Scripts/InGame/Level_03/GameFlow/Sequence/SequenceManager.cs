using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    // === Managers ===
    [SerializeField] private LightManager lightManager;

    // === Sequence ===
    private int size = 0;
    private int min = 0;
    private int max = 0;
    [SerializeField] private List<int> sequenceList = new();

    // === Events ===
    public event Action SequenceMatched;

    // === Properties ===
    public int Size { set => size = value; }
    public int Min { set => min = value; }
    public int Max { set => max = value; }
    public List<int> SequenceList => sequenceList;

    void Awake()
    {
        lightManager.ActiveLightsUpdated += CompareSequence;
    }

    private void CreateSequence()
    {
        int randomNumber = UnityEngine.Random.Range(min, max + 1);

        for (int i = 0; i < size; i++)
        {
            while (sequenceList.Contains(randomNumber))
            {
                randomNumber = UnityEngine.Random.Range(min, max + 1);
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

        if (isEqual) SequenceMatched?.Invoke();
    }
}