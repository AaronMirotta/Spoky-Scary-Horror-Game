using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
    private List<GameObject> inventoryButtons = new List<GameObject>();

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
            if (inspectPanel.GetComponentInChildren<InspectPanel>() != null)
            {
                InspectPanel inspect = inspectPanel.GetComponentInChildren<InspectPanel>();
                inspect.SetInspect(item.ItemInspectSprite, item.ItemName, item.ItemDescription);
            }

        }
    }
    public void AddItem(Item newItem)
    {
        EventSystem.current.SetSelectedGameObject(null);

        inventoryItems.Add(newItem);

        GameObject newObject = Instantiate(inventoryItemPrefab, inventoryContent.transform);

        if (newObject.TryGetComponent<Button>(out Button button))
        {
            inventoryButtons.Add(newObject);

            //setup button in UI
            button.onClick.AddListener(() => InspectItem(newItem));

            button.image.sprite = newItem.ItemInspectSprite;
        }
    }
    public void Open()
    {
        Debug.Log(inventoryItems.Count);
        if(inventoryItems.Count > 0)
        {
            EventSystem.current.SetSelectedGameObject(inventoryButtons[0]);
            Debug.Log("Selecting: " + EventSystem.current.currentSelectedGameObject.name);
        }
    }
}
