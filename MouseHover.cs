using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the mouse hover over 
/// the specific object and triggers events
/// </summary>
public class MouseHover : MonoBehaviour
{
    private void OnMouseEnter()
    {
        if (gameObject.layer == 8) // Tile Layer 
        {
            Tile tile = GetComponent<Tile>();
            EventHandler.current.HoverOverTile(tile.index);
        }
    }
}
