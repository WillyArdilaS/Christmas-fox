using TMPro;
using UnityEngine;

public class RaceUI : MonoBehaviour
{
    // === Laps ===
    [SerializeField] private TextMeshProUGUI lapsCounter;
    [SerializeField] private int totalLaps;
    private int currentLap = 0;

    void Update()
    {
        if (currentLap == GameManager.instance.CurrentLap) return;

        currentLap = GameManager.instance.CurrentLap;
        if (currentLap == 0)
        {
            lapsCounter.text = "";
        }
        else
        {
            lapsCounter.text = $"{currentLap}/{totalLaps}";
        }
    }
}