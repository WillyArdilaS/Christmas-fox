using System;
using UnityEngine;

[Serializable]
public class SequenceEventData
{
    // === Settings ===
    [SerializeField] private int newSize;
    [SerializeField] private int newMin;
    [SerializeField] private int newMax;

    // === Properties ===
    public int NewSize => newSize;
    public int NewMin => newMin;
    public int NewMax => newMax;
}