using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// This is a resource storage script
/// it also handles the graphical update of the storage
/// based on its filling level
/// </summary>
public class ResourceStorage : MonoBehaviour
{
    public ResourceManager.ResourceType type;
    public int maxAmount = 10;
    public int currAmount = 0;



    public void AddRemoveStoredResource(int amount, MethodHandler.Command addOrRemove)
    {
        if (addOrRemove == MethodHandler.Command.Add)
        {
            currAmount += amount;
        }
        else if (addOrRemove == MethodHandler.Command.Remove)
        {
            currAmount -= amount;
        }      

        for (int i = 0; i <= 10; i++)
        {
            var child = this.transform.GetChild(i);
            if (i == currAmount)
            {
                child.gameObject.SetActive(true);
                //Debug.Log("Set log active" + child);
            }                
            else
            {
                child.gameObject.SetActive(false);
                //Debug.Log("Set log inactive" + child);
            }
                
        }
    }

    private void Start()
    {
        AddRemoveStoredResource(0, MethodHandler.Command.Add);
    }


    void OnEnable()
    {
        if (this.tag == TagHandler.buildingWoodStorageString)
        {
            type = ResourceManager.ResourceType.Wood;
        }
        else if (this.tag == TagHandler.buildingStoneStorageString)
        {
            type = ResourceManager.ResourceType.Stone;
        }

        // Add to PlayerResources storage list at start
        ResourceManager.AddRemoveStorage(this, MethodHandler.Command.Add);

    }

    private void OnDisable()
    {
        ResourceManager.AddRemoveStorage(this, MethodHandler.Command.Remove);
    }
}
