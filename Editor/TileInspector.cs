using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEditor.Graphs;
using System.Diagnostics;

[CustomEditor(typeof(Tile))]
[CanEditMultipleObjects]
public class TileInspector : Editor
{
	[SerializeField]
	private Map map;
	private Transform tiles;

	void OnEnable()
	{
		map = GameObject.Find("MapGen").GetComponent<Map>();
		tiles = GameObject.Find("HexGen").transform;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		Tile tile = target as Tile;
		if (GUILayout.Button("Update all Tiles"))
		{
			if (map)
			{
				foreach(Transform child in tiles)
				{
					map.ClearTile(child, false);
					Tile t = child.GetComponent<Tile>();
					//VisualHandler vH = child.GetComponent<VisualHandler>();
					//vH.ChangeVisibility(t.visibility);
					map.SetTilePropTo(t.transform, t.property);
				}
				UnityEngine.Debug.Log("Updated all Tiles");
			}

		}


		if (GUILayout.Button("Update Tile"))
		{
			if (map)
			{
				map.ClearTile(tile.transform, false);
				Tile t = tile.GetComponent<Tile>();
				VisualHandler vH = tile.GetComponent<VisualHandler>();
				vH.ChangeVisibility(t.visibility);
				map.SetTilePropTo(tile.transform, tile.property);
				UnityEngine.Debug.Log("Updated Tile: " + tile.gameObject.name);
			}

		}

		serializedObject.ApplyModifiedProperties();
	}

}



