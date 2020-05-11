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
