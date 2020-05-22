using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldTree : MonoBehaviour
{
    Rigidbody rb;
    Material leavesMat;
    // animate the game object from -1 to +1 and back
    public float minimum = 0.0001f;
    public float maximum = 0.3f;

    // starting value for the Lerp
    private float t = 0.0f;

    void Start()
    {
        leavesMat = transform.GetChild(0).GetComponent<MeshRenderer>().materials[1];
        rb = GetComponent<Rigidbody>();
        if (gameObject.name == "OldTreeDark")
        {
            rb.AddForce(Vector3.forward * 4, ForceMode.Impulse);
        }
    }

    private void OnDestroy()
    {
        if (transform.parent.tag == TagHandler.magicTreeString)
        {
            // Darkness ensues
            Debug.Log("Light Change");
            Light light = GameObject.Find("Directional Light").GetComponent<Light>();
            light.color = new Color(0.5928696f, 0.5764706f, 0.9607843f, 1);

            ItemManager itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
            itemManager.SpawnItemAtTile(transform.parent.GetComponent<Tile>(), ItemManager.ItemID.FireTreeSeed);
        }
    }

    void Update()
    {
        // Pumping redness effect of old tree leaves
        //
        // animate the metallic value
        leavesMat.SetFloat("_Metallic", Mathf.Lerp(minimum, maximum, t));

        // .. and increase the t interpolater
        t += 0.5f * Time.deltaTime;

        // now check if the interpolator has reached maximum
        // and swap maximum and minimum so value moves
        // in the opposite direction.
        if (t > 1.0f)
        {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            t = 0.0f;
        }
    }
}
