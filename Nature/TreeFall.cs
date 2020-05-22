using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFall : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider boxCol;
    SphereCollider sphereCol;

    [SerializeField]
    private bool destroy = true; 
    [SerializeField]
    private float destroyAfter = 4f;
    [SerializeField]
    private bool deactivate = true; 
    [SerializeField]
    private float deactivateAfter = 2f; 

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        if (destroy)
        {
            StartCoroutine(SysHelper.WaitForAndExecute(destroyAfter, () => GameObject.Destroy(this.gameObject)));
        }
        if (deactivate)
        {
            StartCoroutine(SysHelper.WaitForAndExecute(deactivateAfter, Deactivate));
        }
        boxCol = GetComponent<BoxCollider>();
        sphereCol = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) // Unit Layer
        {
            boxCol.enabled = false;
            sphereCol.enabled = false;
            rb.useGravity = false;
        }
    }
    void Deactivate()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
    }
}
