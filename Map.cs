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

    public GameObject treePrefab;
    public GameObject rockPrefab;
    void Start()
    {
        hex = GameObject.Find("HexGen").transform;
        grassMaterial = (Material)Resources.Load("Materials/GrassMat");
        rockMaterial = (Material)Resources.Load("Materials/RockMat");
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




    public void SetTilePropTo(Transform tileTransform, Tile.Property property)
    {
        Tile tile = tileTransform.GetComponent<Tile>();
        MeshRenderer meshRen = tileTransform.GetComponent<MeshRenderer>();

        tile.property = property;

        if (property == Tile.Property.Tree)
        {
            meshRen.material = (grassMaterial) ? grassMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            VisualHandler tileMat = tileTransform.GetComponent<VisualHandler>();
            tileMat.originalMat = meshRen.material;

            GameObject tree = Instantiate(Resources.Load("Nature/" + treePrefab.name), tileTransform.position, Quaternion.identity, tileTransform) as GameObject;
            tree.name = treePrefab.name;
            tree.transform.localScale += new Vector3(1f, 1f, 1f);
            tile.Passable = false;
        }
        if (property == Tile.Property.Grass)
        {
            meshRen.material = (grassMaterial) ? grassMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            VisualHandler tileMat = tileTransform.GetComponent<VisualHandler>();
            tileMat.originalMat = meshRen.material;
            tile.Passable = true;
        }
        if (property == Tile.Property.Rock)
        {
            meshRen.material = (rockMaterial) ? rockMaterial : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");
            VisualHandler tileMat = tileTransform.GetComponent<VisualHandler>();
            tileMat.originalMat = meshRen.material;

            GameObject rock = Instantiate(Resources.Load("Nature/" + rockPrefab.name), tileTransform.position, Quaternion.identity, tileTransform) as GameObject;
            rock.name = rockPrefab.name;
            rock.transform.localScale += new Vector3(1f, 1f, 1f);
            tile.Passable = false;
        }


        if (property == Tile.Property.Default)
        {
            ClearTile(tileTransform, true);
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
