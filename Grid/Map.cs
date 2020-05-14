using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map : MonoBehaviour
{
    [SerializeField]
    private Transform hex;

    public Material grassMaterial;
    public Material rockMaterial;
    public Material dirtMaterial;

    public GameObject treePrefab;
    public GameObject rockPrefab;
    public GameObject bonfirePrefab;


    public Grid _grid;
    void Start()
    {
        hex = GameObject.Find("HexGen").transform;
        if (!_grid)
            _grid = hex.GetComponent<Grid>();

        grassMaterial = (Material)Resources.Load("Materials/GrassMat");
        rockMaterial = (Material)Resources.Load("Materials/RockMat");
        dirtMaterial = (Material)Resources.Load("Materials/DirtMat");


        // Cleanup of map and clear tils in buildings
        foreach (Transform child in hex.transform)
        {
            Tile t = child.GetComponent<Tile>();
            if (t.property == Tile.Property.Bonfire)
            {
                var buildingSizeTiles = _grid.TilesInRange(t.index, 1);
                buildingSizeTiles.Remove(t);
                SetBuildingSize(buildingSizeTiles);
                break;
            }
        }
    }
    private void SetBuildingSize(List<Tile> buildingSizeTiles)
    {
        foreach (var _t in buildingSizeTiles)
        {
            ClearTile(_t.transform, false);
            MeshRenderer t_meshRen = _t.GetComponent<MeshRenderer>();
            t_meshRen.material = (dirtMaterial) ? dirtMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            SelectionStatusHandler vH = _t.GetComponent<SelectionStatusHandler>();
            vH.originalMat = t_meshRen.material;
            _t.property = Tile.Property.Bonfire;
            _t.Passable = false;
            _t.tag = "BuildingBonfire";
        }
    }

    #region Public Methods
    public void GenerateMap()
    {
        ClearMap();

        if (hex)
        {
            foreach (Transform tile in hex)
            {
                var newProp = Tile.Property.Tree;
                SetTilePropTo(tile, newProp);
            }
        }
    }


    public void ClearMap()
    {
        if (hex)
        {
            foreach (Transform tile in hex)
            {
                ClearTile(tile, true);
            }
        }
    }

    public void SetBonfire(Transform tileTransform)
    {
        Tile tile = tileTransform.GetComponent<Tile>();
        GameObject bonfire = Instantiate(Resources.Load("Buildings/" + bonfirePrefab.name), tileTransform.position, Quaternion.identity, tileTransform) as GameObject;
        bonfire.name = bonfirePrefab.name;
        Bonfire fire = bonfire.GetComponent<Bonfire>();
        fire.currTile = tile;
        tile.Passable = false;
        MeshRenderer t_meshRen = tile.GetComponent<MeshRenderer>();
        t_meshRen.material = (dirtMaterial) ? dirtMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
        SelectionStatusHandler vH = tile.GetComponent<SelectionStatusHandler>();
        vH.originalMat = t_meshRen.material;
    }


    public void SetTilePropTo(Transform tileTransform, Tile.Property newProperty)
    {
        Tile tile = tileTransform.GetComponent<Tile>();
        MeshRenderer meshRen = tileTransform.GetComponent<MeshRenderer>();

        tile.property = newProperty;

        if (newProperty == Tile.Property.Tree)
        {
            meshRen.material = (grassMaterial) ? grassMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            SelectionStatusHandler tileMat = tileTransform.GetComponent<SelectionStatusHandler>();
            tileMat.originalMat = meshRen.material;
            tile.tag = "ResourceTree";
            var randomOffset = Random.Range(0.4f, 0.8f);
            for (int i = 0; i < 2; i++)
            {
                GameObject tree = Instantiate(Resources.Load("Nature/" + treePrefab.name), tileTransform.position, Quaternion.Euler(new Vector3(Random.Range(-5f, 5f), Random.Range(0, 360), Random.Range(-5f, 5f))), tileTransform) as GameObject;
                tree.name = treePrefab.name;
                var randomScale = Random.Range(0.75f, 1.1f);
                if (i == 0)
                {
                    tree.transform.position += new Vector3(randomOffset, 0, randomOffset);
                }
                else
                {
                    tree.transform.position -= new Vector3(randomOffset,0, randomOffset);
                }
                tree.transform.localScale += new Vector3(randomScale, randomScale, randomScale);                
            }           
            tile.Passable = false;
        }
        if (newProperty == Tile.Property.Grass)
        {
            meshRen.material = (grassMaterial) ? grassMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            SelectionStatusHandler tileMat = tileTransform.GetComponent<SelectionStatusHandler>();
            tileMat.originalMat = meshRen.material;
            tile.Passable = true;
            tile.tag = "Ground";
        }
        if (newProperty == Tile.Property.Rock)
        {
            meshRen.material = (rockMaterial) ? rockMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            SelectionStatusHandler tileMat = tileTransform.GetComponent<SelectionStatusHandler>();
            tileMat.originalMat = meshRen.material;
            tile.tag = "ResourceRock";
            GameObject rock = Instantiate(Resources.Load("Nature/" + rockPrefab.name), tileTransform.position, Quaternion.identity, tileTransform) as GameObject;
            rock.name = rockPrefab.name;
            rock.transform.localScale += new Vector3(1f, 1f, 1f);
            tile.Passable = false;
        }

        


        if (newProperty == Tile.Property.Default)
        {
            ClearTile(tileTransform, true);
            tile.tag = null;
        }
        Debug.Log("Set " + tile.gameObject.name + " to " + tile.property);
    }


    public void ClearTile(Transform tileTransform, bool setToDefault)
    {
        Tile tile = tileTransform.GetComponent<Tile>();
        MeshRenderer meshRen = tileTransform.GetComponent<MeshRenderer>();
        meshRen.material = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");

        if (setToDefault)
        {
            tile.property = Tile.Property.Default;
        }

        if (tileTransform.childCount > 0)
        {
            foreach (Transform child in tileTransform)
            { 
                if (child.name != "Darkness")
                    DestroyImmediate(child.gameObject);                
            }
        }
    }

    #endregion


    #region Private Methods



    #endregion
}
