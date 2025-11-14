using UnityEngine;
using UnityEngine.Splines;

public class TrackManager : MonoBehaviour
{
    [SerializeField] private SplineContainer[] tracks;

    // === Properties ===
    public SplineContainer[] Tracks => tracks;

    public SplineContainer GetTrack(int index)
    {
        if (tracks == null || tracks.Length == 0) return null;

        index = Mathf.Clamp(index, 0, tracks.Length - 1);
        return tracks[index];
    }
}