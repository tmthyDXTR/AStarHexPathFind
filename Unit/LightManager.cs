using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This class handles the light emitted by units
/// and buildings. It holds a dictionary with all
/// the tiles that receive light and checks if the 
/// light source still exists, otherwise it hides the
/// tile in darkness
/// </summary>
public class LightManager : MonoBehaviour
{
    // Key: Tile, Value: light source counter. If counter = 0 Remove Item from
    // dictionary and hide it.
    public Dictionary<Tile, int> litTiles = new Dictionary<Tile, int>();

    void Start()
    {
        EventHandler.current.onLightSourcesUpdated += UpdateLightList;
    }


    private void UpdateLightList()
    {
        foreach (var item in litTiles)
        {
            SelectionStatusHandler vH = item.Key.GetComponent<SelectionStatusHandler>();
            if (item.Value > 0)
            {
                item.Key.IsDark = false;
                vH.darkness.SetActive(false);
                vH.ChangeSelectionStatus(Tile.SelectionStatus.Default);
                item.Key.IsDiscoveredFog = false;
            }
            if (item.Value < 1)
            {
                item.Key.IsDiscoveredFog = true;
                //item.Key.IsDark = true;
                //vH.darkness.SetActive(true);
                vH.ChangeSelectionStatus(Tile.SelectionStatus.Fog);
            }
        }
        var toRemove = litTiles.Where(x => x.Value == 0).Select(x => x.Key).ToList();
        foreach (var item in toRemove)
        {
            litTiles.Remove(item);
        }
        Debug.Log("Updated light sources");
    }
}
