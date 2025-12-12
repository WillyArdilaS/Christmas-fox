using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    // === Lights ===
    [SerializeField] private HouseLightController[] houseLights;
    [SerializeField] private List<int> activeLights = new();

    // === Events ===
    public event Action<List<int>> ActiveLightsUpdated;

    // === Properties ===
    public HouseLightController[] HouseLights => houseLights;

    void Awake()
    {
        houseLights = houseLights.OrderBy(light => light.HouseID).ToArray();

        foreach (var houseLight in houseLights)
        {
            houseLight.LightSwitched += UpdateActiveLights;
        }
    }

    private void UpdateActiveLights(HouseLightController currentHouseLight)
    {
        if (currentHouseLight.IsOn)
        {
            if(!activeLights.Contains(currentHouseLight.HouseID)) activeLights.Add(currentHouseLight.HouseID);
        }
        else
        {
            activeLights.Remove(currentHouseLight.HouseID);
        }

        ActiveLightsUpdated?.Invoke(new List<int>(activeLights));
    }

    public void ResetActiveLights()
    {
        foreach (var houseLight in houseLights)
        {
            houseLight.SetLight(false);
        }

        activeLights.Clear();
    }
}