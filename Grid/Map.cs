using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map : MonoBehaviour
{
    [SerializeField]
    private Transform hex;

    public Material grassMaterial;
    public Material waterMaterial;
    public Material rockMaterial;
    public Material dirtMaterial;

    public GameObject treePrefab;
    public GameObject rockPrefab;
    public GameObject bonfirePrefab;
    public GameObject bagpackPrefab;
    public GameObject woodStoragePrefab;
    public GameObject stoneStoragePrefab;
    public GameObject cauldronPrefab;
    public GameObject housePrefab;

    public GameObject oldTreePrefab;

    public Grid _grid;
    void Start()
    {
        hex = GameObject.Find("HexGen").transform;
        if (!_grid)
            _grid = hex.GetComponent<Grid>();

        grassMaterial = (Material)Resources.Load("Materials/GrassMat");
        waterMaterial = (Material)Resources.Load("Materials/WaterMat");
        rockMaterial = (Material)Resources.Load("Materials/RockMat");
        dirtMaterial = (Material)Resources.Load("Materials/DirtMat");
        

    }



    public static void SetBuildingSize(List<Tile> buildingSizeTiles, Tile.Property buildingProperty)
    {// For cleanup of tiles within a buildings tiles (spanning over several tiles)
        foreach (var _t in buildingSizeTiles)
        {
            ClearTile(_t.transform, false);
            MeshRenderer t_meshRen = _t.GetComponent<MeshRenderer>();
            var dirtMaterial = (Material)Resources.Load("Materials/DirtMat");
            t_meshRen.material = (dirtMaterial) ? dirtMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            SelectionStatusHandler vH = _t.GetComponent<SelectionStatusHandler>();
            vH.originalMat = t_meshRen.material;
            _t.property = buildingProperty;
            _t.Passable = false;
            _t.tag = TagHandler.buildingBonfireString;
            if (_t.GetComponent<Resource>())
            {
                Destroy(_t.GetComponent<Resource>());
            }
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
        tile.tag = TagHandler.buildingBonfireString;
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
            tile.tag = TagHandler.resourceWoodString;
            var randomOffset = Random.Range(0.05f, 0.15f);
            var randomDir = Random.insideUnitCircle.normalized;
            for (int i = 0; i < 2; i++)
            {
                GameObject tree = Instantiate(Resources.Load("Nature/" + treePrefab.name), tileTransform.position, Quaternion.Euler(new Vector3(Random.Range(-7f, 7f), Random.Range(0, 360), Random.Range(-7f, 7f))), tileTransform) as GameObject;
                tree.name = treePrefab.name;
                var randomScale = Random.Range(0.75f, 1.3f);
                if (i == 0)
                {
                    tree.transform.position += Vector3.forward * randomOffset + new Vector3(randomDir.x, 0, randomDir.y);
                }
                else
                {
                    tree.transform.position -= Vector3.forward * randomOffset + new Vector3(randomDir.x, 0, randomDir.y);
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
            tile.tag = TagHandler.walkGroundString;
        }
        if (newProperty == Tile.Property.Water)
        {
            meshRen.material = (waterMaterial) ? waterMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            SelectionStatusHandler tileMat = tileTransform.GetComponent<SelectionStatusHandler>();
            tileMat.originalMat = meshRen.material;
            tile.Passable = false;
        }
        if (newProperty == Tile.Property.Stone)
        {
            meshRen.material = (rockMaterial) ? rockMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            SelectionStatusHandler tileMat = tileTransform.GetComponent<SelectionStatusHandler>();
            tileMat.originalMat = meshRen.material;
            tile.tag = TagHandler.resourceStoneString;
            GameObject rock = Instantiate(Resources.Load("Nature/" + rockPrefab.name), tileTransform.position, Quaternion.identity, tileTransform) as GameObject;
            rock.name = rockPrefab.name;
            rock.transform.localScale += new Vector3(1f, 1f, 1f);
            tile.Passable = false;
        }
        if (newProperty == Tile.Property.OldTree)
        {
            meshRen.material = (grassMaterial) ? grassMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            SelectionStatusHandler tileMat = tileTransform.GetComponent<SelectionStatusHandler>();
            tileMat.originalMat = meshRen.material;
            tile.tag = TagHandler.magicTreeString;
            GameObject oldTree = Instantiate(Resources.Load("Nature/" + oldTreePrefab.name), tileTransform.position, Quaternion.identity, tileTransform) as GameObject;
            oldTree.name = oldTreePrefab.name;
            tile.Passable = false;
        }
        if (newProperty == Tile.Property.Bagpack)
        {
            meshRen.material = (grassMaterial) ? grassMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            SelectionStatusHandler tileMat = tileTransform.GetComponent<SelectionStatusHandler>();
            tileMat.originalMat = meshRen.material;
            tile.tag = TagHandler.buildingBagpackString;
            GameObject bagPack = Instantiate(Resources.Load("Buildings/" + bagpackPrefab.name), tileTransform.position, Quaternion.identity, tileTransform) as GameObject;
            bagPack.name = bagpackPrefab.name;
            tile.Passable = false;
        }

        if (newProperty == Tile.Property.WoodStorage)
        {
            meshRen.material = (dirtMaterial) ? dirtMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            SelectionStatusHandler tileMat = tileTransform.GetComponent<SelectionStatusHandler>();
            tileMat.originalMat = meshRen.material;
            tile.tag = TagHandler.buildingWoodStorageString;
            GameObject woodStorage = Instantiate(Resources.Load("Buildings/" + woodStoragePrefab.name), tileTransform.position, Quaternion.Euler(0, 120, 0), tileTransform) as GameObject;
            woodStorage.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            woodStorage.name = woodStoragePrefab.name;
            tile.Passable = false;
        }
        if (newProperty == Tile.Property.StoneStorage)
        {
            meshRen.material = (rockMaterial) ? rockMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            SelectionStatusHandler tileMat = tileTransform.GetComponent<SelectionStatusHandler>();
            tileMat.originalMat = meshRen.material;
            tile.tag = TagHandler.buildingStoneStorageString;
            GameObject woodStorage = Instantiate(Resources.Load("Buildings/" + stoneStoragePrefab.name), tileTransform.position, Quaternion.Euler(0, 120, 0), tileTransform) as GameObject;
            woodStorage.name = stoneStoragePrefab.name;
            tile.Passable = false;
        }
        if (newProperty == Tile.Property.Cauldron)
        {
            meshRen.material = (dirtMaterial) ? dirtMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            SelectionStatusHandler tileMat = tileTransform.GetComponent<SelectionStatusHandler>();
            tileMat.originalMat = meshRen.material;
            tile.tag = TagHandler.buildingCauldronString;
            GameObject cauldron = Instantiate(Resources.Load("Buildings/" + cauldronPrefab.name), tileTransform.position, Quaternion.identity, tileTransform) as GameObject;
            tile.Passable = false;
            cauldron.name = woodStoragePrefab.name;
        }
        if (newProperty == Tile.Property.House)
        {
            meshRen.material = (dirtMaterial) ? dirtMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            SelectionStatusHandler tileMat = tileTransform.GetComponent<SelectionStatusHandler>();
            tileMat.originalMat = meshRen.material;
            tile.tag = TagHandler.buildingHouseString;
            GameObject house = Instantiate(Resources.Load("Buildings/" + housePrefab.name), tileTransform.position, Quaternion.identity, tileTransform) as GameObject;
            house.name = housePrefab.name;
            tile.Passable = false;
        }
        if (newProperty == Tile.Property.Construction)
        {
            meshRen.material = (dirtMaterial) ? dirtMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            SelectionStatusHandler tileMat = tileTransform.GetComponent<SelectionStatusHandler>();
            tileMat.originalMat = meshRen.material;
            tile.Passable = false;
            tile.tag = TagHandler.buildingConstructionString;
        }

        if (newProperty == Tile.Property.Default)
        {
            ClearTile(tileTransform, true);
            tile.tag = null;
        }
        Debug.Log("Set " + tile.gameObject.name + " to " + tile.property);
    }


    public static void ClearTile(Transform tileTransform, bool setToDefault)
    {
        Tile tile = tileTransform.GetComponent<Tile>();
        MeshRenderer meshRen = tileTransform.GetComponent<MeshRenderer>();
        meshRen.material = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");

        if (setToDefault)
        {
            tile.property = Tile.Property.Default;
        }

        var tempChildList = tileTransform.Cast<Transform>().ToList();
        foreach (var child in tempChildList)
        {
            DestroyImmediate(child.gameObject);
        }
        for (int i = tileTransform.transform.childCount; i > 0; --i)
        {
            DestroyImmediate(tileTransform.transform.GetChild(0).gameObject);
        }

        //Debug.Log("Cleared Tile: " + tileTransform);
    
    }

    #endregion


    #region Private Methods



    #endregion
}
