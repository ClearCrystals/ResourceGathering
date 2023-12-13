using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererAI : MonoBehaviour
{
    private enum State
    {
        Idle,
        MovingToResourceNode,
        GatheringResources,
        MovingToStorage,
    }

    private IUnits unit;
    private State state;
    private ResourceNode resourceNode;
    private int goldInventoryAmount;
    private Transform storageTransform;

    private void Awake()
    {
        unit = gameObject.GetComponent<IUnits>(); // Ensure this is the correct interface name
        if (unit == null)
        {
            Debug.LogError("[GathererAI] No IUnits component found on the GameObject.");
        }

        state = State.Idle;
        Debug.Log("[GathererAI] State set to Idle.");
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                Debug.Log("[GathererAI] State: Idle. Finding resource node.");
                resourceNode = GameHandler.GetResourceNode_Static(); // This might return null

                if (resourceNode != null)
                {
                    state = State.MovingToResourceNode;
                    Debug.Log($"[GathererAI] Moving to resource node at position: {resourceNode.GetPosition()}");
                }
                else
                {
                    Debug.Log("[GathererAI] No resource nodes available.");
                    // Handle the situation when no resource nodes are available
                    // Maybe keep the state as Idle or implement some other logic
                }
            break;
            case State.MovingToResourceNode:
                if (unit.IsIdle())
                {
                    Debug.Log("[GathererAI] State: MovingToResourceNode. Starting move.");
                    unit.MoveTo(resourceNode.GetPosition(), 0.5f, () =>
                    {
                        Debug.Log("[GathererAI] Arrived at resource node.");
                        state = State.GatheringResources;
                    });
                }
                break;

            case State.GatheringResources:
                if (unit.IsIdle())
                {
                    if (goldInventoryAmount > 2)
                    {
                        Debug.Log("[GathererAI] State: GatheringResources. Moving to storage.");
                        storageTransform = GameHandler.GetStorageNode_Static();
                        state = State.MovingToStorage;
                    }
                    else
                    {
                        Debug.Log("[GathererAI] State: GatheringResources. Starting mining animation.");
                        unit.PlayAnimationMine(resourceNode.GetPosition(), () =>
                        {
                            resourceNode.GrabResources();
                            goldInventoryAmount++;
                            Debug.Log($"[GathererAI] Mined gold. Inventory: {goldInventoryAmount}");
                        });
                    };
                };
                break;

            case State.MovingToStorage:
                if (unit.IsIdle())
                {
                    Debug.Log("[GathererAI] State: MovingToStorage. Starting move.");
                    unit.MoveTo(storageTransform.position, 0.5f, () =>
                    {
                        GameResources.AddGoldAmount(goldInventoryAmount);
                        Debug.Log("[GathererAI] Arrived at storage. Gold deposited.");
                        goldInventoryAmount = 0;
                        state = State.Idle;
                    });
                }
                break;
        }
    }
}
