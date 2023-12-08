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
    [SerializeField] private float stopDistance = 1f; // Distance at which the unit will stop from the target

    private void Awake()
    {
        instance = this;
    }

    
    private Transform GetResourceNode()
    {
        List<Transform> resourceNodeList = new List<Transform>() { woodNode1Transform, woodNode2Transform, woodNode3Transform };
        return resourceNodeList[UnityEngine.Random.Range(0, resourceNodeList.Count)];
    }

    

    public static Transform GetResourceNode_Static()
    {
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