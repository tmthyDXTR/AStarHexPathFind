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
    public Material discoveredFogMat;
    public GameObject darknessPrefab;

    public GameObject darkness;


    private Dictionary<Transform, Material[]> origRendMats;
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
        discoveredFogMat = (Material)Resources.Load("Materials/DiscoveredFogMat");
        darknessPrefab = (GameObject)Resources.Load("Nature/Darkness");

        origRendMats = new Dictionary<Transform, Material[]>();
        
        darkness = (GameObject)Instantiate(Resources.Load("Nature/" + darknessPrefab.name), this.transform);
        darkness.name = darknessPrefab.name;
        tile.IsDark = true;
        //darkness.SetActive(false);
        //tile.Passable = false;
        // Events
        ChangeSelectionStatus(Tile.SelectionStatus.Fog);
    }

    private void GetOriginalMaterialArrays()
    {
        var rends = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < rends.Length; i++)
        {
            if (rends[i].gameObject.layer != 22)
            {
               if (!origRendMats.ContainsKey(rends[i].transform)) 
                    origRendMats.Add(rends[i].transform, rends[i].materials);
            }
        }
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
            foreach (var rend in origRendMats)
            {
                rend.Key.GetComponent<MeshRenderer>().materials = rend.Value;
            }
            origRendMats.Clear();
        }
        if (selectionStatus == Tile.SelectionStatus.Fog)
        {
            GetOriginalMaterialArrays();
            tile.selectionStatus = Tile.SelectionStatus.Fog;
            var rends = GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < rends.Length; i++)
            {
                var mats = rends[i].materials;
                for (int k = 0; k < mats.Length; k++)
                {
                    mats[k] = discoveredFogMat;
                }
                rends[i].materials = mats;
            }
        }
    }
}