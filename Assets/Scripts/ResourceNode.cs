using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class ResourceNode
{
    private Transform resourcesNodeTransform;
    private int resourceAmount; // Declare the resourceAmount field

    public ResourceNode(Transform resourceNodeTransform) 
    {
        this.resourcesNodeTransform = resourceNodeTransform;
        resourceAmount = 1; // Initialize with some default value, or modify as needed
    }

    public Vector3 GetPosition() 
    {
        return resourcesNodeTransform.position;
    }

    public Transform GetTransform()
    {
        return resourcesNodeTransform;
    }

    public void GrabResources() 
    {
        if (resourceAmount > 0)
        {
            resourceAmount -= 1; // Decrease resource amount
            CMDebug.TextPopupMouse("ResourceAmount:" + resourceAmount);
        }
    }

    public bool HasResources() 
    {
        return resourceAmount > 0;
    }
}