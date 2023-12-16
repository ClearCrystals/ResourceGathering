using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;
    [SerializeField] private GathererAI gathererAI;

    [SerializeField] private Transform woodNode1Transform;
    [SerializeField] private Transform woodNode2Transform;
    [SerializeField] private Transform woodNode3Transform;
    [SerializeField] private Transform woodNode4Transform;
    [SerializeField] private Transform woodNode5Transform;
    [SerializeField] private Transform woodNode6Transform;
    [SerializeField] private Transform woodNode7Transform;
    [SerializeField] private Transform woodNode8Transform;
    [SerializeField] private Transform woodNode9Transform;
    [SerializeField] private Transform storageTransform;
    // [SerializeField] private float stopDistance = 1f; // Distance at which the unit will stop from the target

private List<ResourceNode> resourceNodeList;

    private void Awake()
    {
        instance = this;

        resourceNodeList = new List<ResourceNode>();
        AddResourceNode(woodNode1Transform);
        AddResourceNode(woodNode2Transform);
        AddResourceNode(woodNode3Transform);
        AddResourceNode(woodNode4Transform);
        AddResourceNode(woodNode5Transform);
        AddResourceNode(woodNode6Transform);
        AddResourceNode(woodNode7Transform);
        AddResourceNode(woodNode8Transform);
        AddResourceNode(woodNode9Transform);

    }

    private void AddResourceNode(Transform nodeTransform)
    {
        if (nodeTransform == null)
        {
            Debug.LogError("Attempted to add a ResourceNode with a null Transform.");
            return;
        }

        ResourceNode node = new ResourceNode(nodeTransform);
        node.OnResourceNodeClicked += ResourceNode_OnResourceNodeClicked; 
        resourceNodeList.Add(node);
    }
    
    private void ResourceNode_OnResourceNodeClicked(object sender, EventArgs e) 
    {
            ResourceNode resourceNode = sender as ResourceNode;
        if (resourceNode == null)
        {
            Debug.LogError("ResourceNode_OnResourceNodeClicked: Sender is not a ResourceNode.");
            return;
        }

        if (gathererAI == null)
        {
            Debug.LogError("GathererAI reference is not set in GameHandler.");
            return;
        }

        gathererAI.SetResourceNode(resourceNode);
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
            return tmpResourceNodeList[UnityEngine.Random.Range(0, tmpResourceNodeList.Count)];
        } 
        else 
        {
            return null; // No resources available
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