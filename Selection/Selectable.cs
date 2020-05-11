using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    /// <summary>
    /// This class gives an object the feature to be
    /// selected by mouse click, that input is further handled 
    /// by the SelectionManager class
    /// </summary>
    /// 
    [SerializeField]
    private bool isSelected = false;
    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            isSelected = value;
        }
    }

}
