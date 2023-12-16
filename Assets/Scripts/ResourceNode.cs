using System;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class ResourceNode
{
    private Transform resourcesNodeTransform;
    private int resourceAmount;

    public event EventHandler OnResourceNodeClicked;

    public ResourceNode(Transform resourceNodeTransform) 
    {
        this.resourcesNodeTransform = resourceNodeTransform;
        resourceAmount = 6; // Initialize with some default value, or modify as needed
        resourceNodeTransform.GetComponent<Button_Sprite>().ClickFunc = () => {
            OnResourceNodeClicked?.Invoke(this, EventArgs.Empty);
        };
    }

    public Vector3 GetPosition() 
    {
        return resourcesNodeTransform.position;
    }

    public bool HasResources() 
    {
        return resourceAmount > 0;
    }

    public void GrabResources() 
    {
        if (resourceAmount > 0)
        {
            resourceAmount -= 1; // Decrease resource amount
            if (resourceAmount <= 0)
            {
                // Handle resource depletion
                Debug.Log("ResourceNode depleted: " + resourcesNodeTransform.name);
                // You can add code here to make the node inactive
                // For example, disable its collider or change its appearance
            }
        }
        else
        {
            Debug.Log("Attempted to grab resources from a depleted node: " + resourcesNodeTransform.name);
        }
    }

    public void Clicked() 
    {
        Debug.Log("ResourceNode clicked: " + resourcesNodeTransform.name);
        OnResourceNodeClicked?.Invoke(this, EventArgs.Empty);
    }
}
