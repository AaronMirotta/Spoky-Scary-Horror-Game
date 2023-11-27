using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item : MonoBehaviour, IInteractable
{
    //holds all information for the inventory and inspection systems
    [SerializeField]
    private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField]
    private string itemDescription;
    public string ItemDescription { get { return itemDescription; } }

    [SerializeField]
    private Sprite itemSprite;
    public Sprite ItemSprite { get { return itemSprite; } }

    public void Interact()
    {
        Pickup();
    }

    private void Pickup()
    {
        //add item to the players inventory
        PlayerController.Instance.AddToInventory(this);

        this.gameObject.SetActive(false);
    }
}
