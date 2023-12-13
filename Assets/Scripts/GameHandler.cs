using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;

    [SerializeField] private Transform woodNode1Transform;
    [SerializeField] private Transform woodNode2Transform;
    [SerializeField] private Transform woodNode3Transform;
    [SerializeField] private Transform storageTransform;
    // [SerializeField] private float stopDistance = 1f; // Distance at which the unit will stop from the target

private List<ResourceNode> resourceNodeList;

    private void Awake()
    {
        instance = this;

        resourceNodeList = new List<ResourceNode>();
        resourceNodeList.Add(new ResourceNode(woodNode1Transform));
        resourceNodeList.Add(new ResourceNode(woodNode2Transform));
        resourceNodeList.Add(new ResourceNode(woodNode3Transform));

    }

    
    private ResourceNode GetResourceNode()
{
    List<ResourceNode> tmpResourceNodeList = new List<ResourceNode>(resourceNodeList);
    for (int i = 0; i < tmpResourceNodeList.Count; i++) 
    {
        if (!tmpResourceNodeList[i].HasResources()) 
        {
            tmpResourceNodeList.RemoveAt(i);
            i--;
        }
    }
    if(tmpResourceNodeList.Count > 0 )
    {
        return resourceNodeList[UnityEngine.Random.Range(0, resourceNodeList.Count)];
    } else 
    {
        return null; 
    }   
}
    
    public static ResourceNode GetResourceNode_Static()
    {
        if (instance == null)
        {
            Debug.LogError("GameHandler instance is not initialized.");
            return null; // Handle null case appropriately
        }
        return instance.GetResourceNode();
    }

    private Transform GetStorageNode()
    {
        return storageTransform;
    }

    public static Transform GetStorageNode_Static()
    {
        return instance.GetStorageNode();
    }
    
}