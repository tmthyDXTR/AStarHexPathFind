using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    [SerializeField]
    private bool gatherResources = false;
    public bool GatherResources
    {
        get { return gatherResources; }
        set
        {
            gatherResources = value;
            EventHandler.current.AbilitiesChanged();
        }
    }
    [SerializeField]
    private bool lightSource = false;
    public bool LightSource
    {
        get { return lightSource; }
        set
        {
            lightSource = value;
            EventHandler.current.AbilitiesChanged();
        }
    }
    [SerializeField]
    private bool feedFire = false;
    public bool FeedFire
    {
        get { return feedFire; }
        set
        {
            feedFire = value;
            EventHandler.current.AbilitiesChanged();
        }
    }
    [SerializeField]
    private bool consumeFire = false;
    public bool ConsumeFire
    {
        get { return consumeFire; }
        set
        {
            consumeFire = value;
            EventHandler.current.AbilitiesChanged();
        }
    }
    [SerializeField]
    private bool cookSoup = false;
    public bool CookSoup
    {
        get { return cookSoup; }
        set
        {
            cookSoup = value;
            EventHandler.current.AbilitiesChanged();
        }
    }
    [SerializeField]
    private bool collectItems = false;
    public bool CollectItems
    {
        get { return collectItems; }
        set
        {
            collectItems = value;
            EventHandler.current.AbilitiesChanged();
        }
    }
    [SerializeField]
    private bool constructBuilding = false;
    public bool ConstructBuilding
    {
        get { return constructBuilding; }
        set
        {
            constructBuilding = value;
            EventHandler.current.AbilitiesChanged();
        }
    }




    private void Start()
    {
        InitializeAbilities();

        EventHandler.current.onAbilitiesChanged += InitializeAbilities;
    }
    private void OnDisable()
    {
        EventHandler.current.onAbilitiesChanged -= InitializeAbilities;
    }


    private void InitializeAbilities()
    {
        if (gatherResources)
        {
            if (this.gameObject.GetComponent<GatherResource>() == null)
                this.gameObject.AddComponent<GatherResource>();
        }
        else
        {
            if (this.gameObject.GetComponent<GatherResource>())            
                Destroy(this.gameObject.GetComponent<GatherResource>());            
        }

        if (lightSource)
        {
            if (this.gameObject.GetComponent<LightSource>() == null)
                this.gameObject.AddComponent<LightSource>();

        }
        else
        {
            if (this.gameObject.GetComponent<LightSource>())
                Destroy(this.gameObject.GetComponent<LightSource>());
        }

        if (feedFire)
        {
            if (this.gameObject.GetComponent<FeedFire>() == null)
                this.gameObject.AddComponent<FeedFire>();

        }
        else
        {
            if (this.gameObject.GetComponent<FeedFire>())
                Destroy(this.gameObject.GetComponent<FeedFire>());
        }

        if (consumeFire)
        {
            if (this.gameObject.GetComponent<ConsumeFire>() == null)
                this.gameObject.AddComponent<ConsumeFire>();

        }
        else
        {
            if (this.gameObject.GetComponent<ConsumeFire>())
                Destroy(this.gameObject.GetComponent<ConsumeFire>());
        }

        if (cookSoup)
        {
            if (this.gameObject.GetComponent<CookSoup>() == null)
                this.gameObject.AddComponent<CookSoup>();

        }
        else
        {
            if (this.gameObject.GetComponent<CookSoup>())
                Destroy(this.gameObject.GetComponent<CookSoup>());
        }

        if (collectItems)
        {
            if (this.gameObject.GetComponent<CollectItems>() == null)
                this.gameObject.AddComponent<CollectItems>();

        }
        else
        {
            if (this.gameObject.GetComponent<CollectItems>())
                Destroy(this.gameObject.GetComponent<CollectItems>());
        }

        if (constructBuilding)
        {
            if (this.gameObject.GetComponent<ConstructBuilding>() == null)
                this.gameObject.AddComponent<ConstructBuilding>();

        }
        else
        {
            if (this.gameObject.GetComponent<ConstructBuilding>())
                Destroy(this.gameObject.GetComponent<ConstructBuilding>());
        }
    }
}
