using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// The building menu
/// </summary>
public class BuildingTab : MonoBehaviour
{
    SelectionManager _selection;

    private BuildingManager _buildingManager;
    public GameObject buttonPrefab;

    private void OnEnable()
    {
        _selection = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();        
        _buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        buttonPrefab = (GameObject)Resources.Load("UI/Button");
        EventHandler.current.HoverOverUIStart();
    }

    public void UpdateBuildingTab()
    {
        // Create a Button for every building in the building manager list
        foreach (var buildingEntry in _buildingManager.buildings)
        {
            var buttonObj = Instantiate(buttonPrefab, this.transform);
            var buttonText = buttonObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            buttonText.text = buildingEntry.Key.ToString();
            var button = buttonObj.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => _buildingManager.SelectBuildingToBuild(buildingEntry.Key, buildingEntry.Value));
        }
        
        
        Debug.Log("Building tab updated");
    }



    private void OnDestroy()
    {
        EventHandler.current.HoverOverUIEnd();
    }
}
