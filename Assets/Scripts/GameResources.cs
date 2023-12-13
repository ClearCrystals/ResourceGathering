using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameResources
{
    public static event EventHandler OnGoldAmountChanged;
    private static int goldAmount;

    public static void AddGoldAmount(int amount)
    {
        goldAmount += amount;
        Debug.Log("Gold amount updated in GameResources: " + goldAmount);
        OnGoldAmountChanged?.Invoke(null, EventArgs.Empty);
    }


    public static int GetGoldAmount()
    {
        return goldAmount;
    }
}