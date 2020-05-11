﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// This class handles the materials and effects
/// during different selection and ui states as 
/// "selected", "highlighted", "path" ...
/// </summary>
public class VisualHandler : MonoBehaviour
{
    private Tile tile;
    private Selectable select;
    private MeshRenderer meshRen;

    public Material originalMat;
    public Material hoverMat;
    public Material selectedMat;
    public Material highlightMat;
    public Material pathMat;


    private void Start()
    {
        tile = GetComponent<Tile>();
        meshRen = GetComponent<MeshRenderer>();
        select = GetComponent<Selectable>();
        // Load the materials
        originalMat = meshRen.material;
        hoverMat = (Material)Resources.Load("Materials/HoverMat");
        selectedMat = (Material)Resources.Load("Materials/SelectedMat");
        highlightMat = (Material)Resources.Load("Materials/MoveMat");
        pathMat = (Material)Resources.Load("Materials/HoverMat");



        // Events
    }

    public void ChangeVisibility(Tile.Visibility visibility)
    {
        
        if (visibility == Tile.Visibility.Hovered && !select.IsSelected)
        {
            tile.visibility = Tile.Visibility.Hovered;
            meshRen.material = hoverMat;
        }
        if (visibility == Tile.Visibility.Selected)
        {
            tile.visibility = Tile.Visibility.Selected;
            meshRen.material = selectedMat;
        }
        if (visibility == Tile.Visibility.Highlighted)
        {
            tile.visibility = Tile.Visibility.Highlighted;
            meshRen.material = highlightMat;
        }
        if (visibility == Tile.Visibility.Path)
        {
            tile.visibility = Tile.Visibility.Path;
            meshRen.material = pathMat;
        }
        if (visibility == Tile.Visibility.Default && !select.IsSelected)
        {
            tile.visibility = Tile.Visibility.Default;
            meshRen.material = originalMat;
        }

        //else
        //{
        //    if (!select.IsSelected)
        //    {
        //        tile.visibility = Tile.Visibility.Default;
        //        meshRen.material = originalMat;
        //    }            
        //}
    }
}