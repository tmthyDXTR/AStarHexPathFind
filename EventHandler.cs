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
    public event Action onFireConsumed;
    public void ConsumeFire()
    {
        if (onFireConsumed != null)
        {
            onFireConsumed();
        }
    }

    public event Action onFireFed;
    public void FireFed()
    {
        if (onFireFed != null)
        {
            onFireFed();
        }
    }

    public event Action onMoveAreaCalculated;
    public void MoveAreaCalculated()
    {
        if (onMoveAreaCalculated != null)
        {
            onMoveAreaCalculated();
        }
    }

    public event Action onResourceDestroyed;
    public void ResourceDestroyed()
    {
        if (onResourceDestroyed != null)
        {
            onResourceDestroyed();
        }
    }

    public event Action<Unit, ResourceManager.ResourceType> onResourceGathered;
    public void ResourceGathered(Unit unit, ResourceManager.ResourceType type)
    {
        if (onResourceGathered != null)
        {
            onResourceGathered(unit, type);
        }
    }

    public event Action onLightSourcesUpdated;
    public void LightSourcesUpdated()
    {
        if (onLightSourcesUpdated != null)
        {
            onLightSourcesUpdated();
        }
    }


    public event Action<Tile> onTileSelected;
    public void TileSelected(Tile tile)
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

    public event Action<Tile> onHoverOverTile;
    public void HoverOverTile(Tile tile)
    {
        if (onHoverOverTile != null)
        {
            onHoverOverTile(tile);
        }
    }

    public event Action<CubeIndex, Tile.SelectionStatus> onVisibilityChanged;
    public void ChangeVisibility(CubeIndex index, Tile.SelectionStatus visibility)
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
