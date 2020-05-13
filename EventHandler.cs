using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static EventHandler current;

    private void Awake()
    {
        current = this;
    }


    // Events
    public event Action<Tile> onTileSelected;
    public void SelectTile(Tile tile)
    {
        if (onTileSelected != null)
        {
            onTileSelected(tile);
        }
    }

    public event Action<Unit> onUnitSelected;
    public void SelectUnit(Unit unit)
    {
        if (onUnitSelected != null)
        {
            onUnitSelected(unit);
        }
    }

    public event Action onDeselectedAll;
    public void DeselectAll()
    {
        if (onDeselectedAll != null)
        {
            onDeselectedAll();
        }
    }

    public event Action<CubeIndex> onHoverOverTile;
    public void HoverOverTile(CubeIndex index)
    {
        if (onHoverOverTile != null)
        {
            onHoverOverTile(index);
        }
    }

    public event Action<CubeIndex, Tile.Visibility> onVisibilityChanged;
    public void ChangeVisibility(CubeIndex index, Tile.Visibility visibility)
    {
        if (onVisibilityChanged != null)
        {
            onVisibilityChanged(index, visibility);
        }
    }

    public event Action<CubeIndex> onTileDestinationChanged;
    public void ChangeTileDestination(CubeIndex index)
    {
        if (onTileDestinationChanged != null)
        {
            onTileDestinationChanged(index);
        }
    }

}
