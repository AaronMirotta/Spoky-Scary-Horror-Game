using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    //UI for the player inventory
    //Actions: Inspect, adjust scroll bar

    [SerializeField]
    private GameObject inventoryContent;

    [SerializeField]
    private GameObject inspectPanel;

    [SerializeField]
    private GameObject inventoryItemPrefab;

    [SerializeField]
    private List<Item> inventoryItems = new List<Item>();

    [SerializeField]
    private List<Button> inventoryButtons = new List<Button>();

    private void ScrollInventory()
    {

    }    
    public void InspectItem(Item item)
    {
        if (inspectPanel.activeSelf)
        {
            inspectPanel.SetActive(false);
        }
        else
        {
            inspectPanel.SetActive(true);
            if(inspectPanel.GetComponentInChildren<InspectPanel>() != null)
            {
                InspectPanel inspect = inspectPanel.GetComponentInChildren<InspectPanel>();
                inspect.SetInspect(item.ItemInspectSprite, item.ItemName, item.ItemDescription);
            }

        }
    }
    public void Open()
    {
        inventoryItems = PlayerController.Instance.Inventory;

        foreach(Item item in inventoryItems)
        {
            GameObject newObject = Instantiate(inventoryItemPrefab, inventoryContent.transform);

            if(newObject.TryGetComponent<Button>(out Button button))
            {
                inventoryButtons.Add(button);

                //setup button in UI
                button.onClick.AddListener(() => InspectItem(item));

                button.image.sprite = item.ItemInspectSprite;
            }
        }
    }
    
}
