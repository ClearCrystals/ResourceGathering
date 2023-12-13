using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Window_GameResources : MonoBehaviour
{
    public TextMeshProUGUI goldAmountText; // Assign this in the Unity Editor
    public static event EventHandler OnGoldAmountChanged; // Define the event

    private static int goldAmount; // Example static field to hold the gold amount

    private void Awake() 
    {
        GameResources.OnGoldAmountChanged += HandleGoldAmountChanged;
        UpdateResourcesTextObject();
    }

    private void OnDestroy()
    {
        GameResources.OnGoldAmountChanged -= HandleGoldAmountChanged;
    }

    private void HandleGoldAmountChanged(object sender, EventArgs e) 
    {
        UpdateResourcesTextObject();
    }

    private void UpdateResourcesTextObject()
    {
        if (goldAmountText != null)
        {
            goldAmountText.text = "Gold: " + GameResources.GetGoldAmount();
        }
        else
        {
            Debug.LogError("Gold amount TextMeshPro component not set.");
        }
    }

}
