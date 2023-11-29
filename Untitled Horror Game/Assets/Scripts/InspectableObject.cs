using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectableObject : MonoBehaviour, IInteractable
{
    //objects in the scene that can be inspected but not picked up
    private InspectPanel inspectPanel;

    [SerializeField]
    private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField]
    private string itemDescription;
    public string ItemDescription { get { return itemDescription; } }

    [SerializeField]
    private Sprite itemSprite;
    public Sprite ItemSprite { get { return itemSprite; } }

    //add flip option

    public void Interact()
    {
        inspectPanel = FindObjectOfType<InspectPanel>();
        //open inspect panel
        inspectPanel.Open();

        //set inspect variables
        inspectPanel.SetInspect(itemSprite, itemName, itemDescription);
    }
    public void Use(Item equippedItem)
    {
        //if the player has an item equipped then check if it is the right item
    }
}
