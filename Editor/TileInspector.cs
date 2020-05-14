using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEditor.Graphs;
using System.Diagnostics;
using System.Collections.Generic;

[CustomEditor(typeof(Tile))]
[CanEditMultipleObjects]
public class TileInspector : Editor
{
	[SerializeField]
	private Map map;
	private Transform tiles;
	private Grid grid;

	void OnEnable()
	{
		map = GameObject.Find("MapGen").GetComponent<Map>();
		tiles = GameObject.Find("HexGen").transform;
		grid = GameObject.Find("HexGen").GetComponent<Grid>();
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
					map.SetTilePropTo(child, t.property);
				}
				foreach (Transform child in tiles)
				{
					Tile t = child.GetComponent<Tile>();
					if (t.property == Tile.Property.Bonfire)
					{
						map.SetBonfire(child);

					}
				}
				UnityEngine.Debug.Log("Updated all Tiles");
			}

		}
		if (GUILayout.Button("Clear all Tiles"))
		{
			if (map)
			{
				foreach (Transform child in tiles)
					map.ClearTile(child, false);
				UnityEngine.Debug.Log("Cleared all Tiles");

			}
		}


		if (GUILayout.Button("Update Tile"))
		{
			if (map)
			{
				map.ClearTile(tile.transform, false);
				Tile t = tile.GetComponent<Tile>();
				SelectionStatusHandler vH = tile.GetComponent<SelectionStatusHandler>();
				vH.ChangeSelectionStatus(t.selectionStatus);
				map.SetTilePropTo(tile.transform, tile.property);
				UnityEngine.Debug.Log("Updated Tile: " + tile.gameObject.name);
			}

		}

		serializedObject.ApplyModifiedProperties();
	}

	
}



