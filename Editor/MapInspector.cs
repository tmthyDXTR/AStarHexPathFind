using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(Map))]
[CanEditMultipleObjects]
public class MapInspector : Editor
{


	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		Map map = target as Map;

		if (GUILayout.Button("Generate Map"))
			map.GenerateMap();

		if (GUILayout.Button("Clear Map"))
			map.ClearMap();
	}
}