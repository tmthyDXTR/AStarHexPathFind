using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles the movement on a 
/// list of tiles (a path), it gets enabled and starts moving
/// the object, the moved object itself has to check if the 
/// target is reached and then disable this script
/// </summary>
public class PathFollow : MonoBehaviour
{
    public List<CubeIndex> pathNodes;
    public GameObject obj;
    public float moveSpeed;
    float timer;
    static Vector3 currentPosHolder;
    int currentNode;

    private Grid _grid;

    // Use this for initialization
    void OnEnable()
    {
        Debug.Log("Path Follow enabled");
        obj = this.gameObject;
        _grid = GameObject.Find("HexGen").GetComponent<Grid>();
        CheckNode();
    }

    private void OnDisable()
    {
        currentNode = 0;
        pathNodes.Clear();
        Debug.Log("Path Follow disabled");
    }

    void CheckNode()
    {
        timer = 0;
        currentPosHolder = _grid.TileAt(pathNodes[currentNode]).transform.position;
    }


    void Update()
    {
        timer += Time.deltaTime * moveSpeed;
        if (Vector3.Distance(obj.transform.position, currentPosHolder) > 0.05f)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, currentPosHolder, timer);
        }
        else
        {
            if (currentNode < pathNodes.Count - 1)
            {
                currentNode++;
                CheckNode();
            }
        }
    }
}
