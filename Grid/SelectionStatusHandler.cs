using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// This class handles the materials and effects
/// during different selection and ui states as 
/// "selected", "highlighted", "path" ...
/// </summary>
public class SelectionStatusHandler : MonoBehaviour
{
    private Tile tile;
    private Selectable select;
    private MeshRenderer meshRen;

    public Material originalMat;
    public Material hoverMat;
    public Material selectedMat;
    public Material highlightMat;
    public Material pathMat;
    public GameObject darknessPrefab;

    public GameObject darkness;
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
        darknessPrefab = (GameObject)Resources.Load("Nature/Darkness");


        darkness = (GameObject)Instantiate(Resources.Load("Nature/" + darknessPrefab.name), this.transform);
        darkness.name = darknessPrefab.name;
        tile.IsDark = true;
        //darkness.SetActive(false);
        //tile.Passable = false;

        // Events
    }

    public void ChangeSelectionStatus(Tile.SelectionStatus selectionStatus)
    {
        
        if (selectionStatus == Tile.SelectionStatus.Hovered && !select.IsSelected)
        {
            tile.selectionStatus = Tile.SelectionStatus.Hovered;
            meshRen.material = hoverMat;
        }
        if (selectionStatus == Tile.SelectionStatus.Selected && select.IsSelected)
        {
            tile.selectionStatus = Tile.SelectionStatus.Selected;
            meshRen.material = selectedMat;
        }
        if (selectionStatus == Tile.SelectionStatus.Highlighted)
        {
            tile.selectionStatus = Tile.SelectionStatus.Highlighted;
            meshRen.material = highlightMat;
        }
        if (selectionStatus == Tile.SelectionStatus.Path)
        {
            tile.selectionStatus = Tile.SelectionStatus.Path;
            meshRen.material = pathMat;
        }
        if (selectionStatus == Tile.SelectionStatus.Default && !select.IsSelected) //  
        {
            tile.selectionStatus = Tile.SelectionStatus.Default;
            meshRen.material = originalMat;
        }
    }
}