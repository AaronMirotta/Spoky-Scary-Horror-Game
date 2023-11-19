using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    //holds all information for the inventory and inspection systems
    [SerializeField]
    string ItemName;

    [SerializeField]
    string ItemDescription;

    [SerializeField]
    Sprite itemInspectSprite;

    public void Interact()
    {
        Pickup();
    }

    private void Pickup()
    {

    }
}
