using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the mouse hover over 
/// the specific object and triggers events
/// </summary>
public class MouseHover : MonoBehaviour
{
    SelectionManager _selection;
    private void Start()
    {
        _selection = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
    }
    private void OnMouseEnter()
    {
        if (_selection.IsActive)
        {
            if (gameObject.layer == 8) // Tile Layer 
            {
                Tile tile = GetComponent<Tile>();
                if (tile.selectionStatus != Tile.SelectionStatus.Fog)
                    EventHandler.current.HoverOverTile(tile);
                else
                    EventHandler.current.HoverOverDarkness(tile);
            }
            else if (gameObject.layer == 22) // Darkness Layer 
            {
                Tile tile = gameObject.transform.parent.GetComponent<Tile>();
                Debug.Log("Darkness");
                EventHandler.current.HoverOverDarkness(tile);
            }
        }       

    }
    private void OnMouseOver()
    {
        if (_selection.IsActive)
        {
            if (gameObject.layer == 8 && SelectionManager.hoveredTile != GetComponent<Tile>()) // Tile Layer 
            {
                Tile tile = GetComponent<Tile>();
                if (tile.selectionStatus != Tile.SelectionStatus.Fog)
                    EventHandler.current.HoverOverTile(tile);
                else
                    EventHandler.current.HoverOverDarkness(tile);
            }
            else if (gameObject.layer == 22 && SelectionManager.hoveredTile != gameObject.transform.parent.GetComponent<Tile>()) // Darkness Layer 
            {
                Tile tile = gameObject.transform.parent.GetComponent<Tile>();
                Debug.Log("Darkness");
                EventHandler.current.HoverOverDarkness(tile);
            }
        }
    }
}
