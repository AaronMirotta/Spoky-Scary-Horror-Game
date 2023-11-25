using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Manage in game UI (inventory, inspect, pause menu)
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    //UI components
    [SerializeField]
    private GameObject inventoryUI;

    [SerializeField]
    private InventoryUI inventory;

    public bool isInventoryOpen;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else { Destroy(this.gameObject); }

        DontDestroyOnLoad(this.gameObject);

        GameStart();
    }
    private void GameStart()
    {
        CloseInventory();
    }
    public void OpenInventory()
    {
        //set inventory active and select the first item in the inventory
        inventoryUI.SetActive(true);
        isInventoryOpen = true;

        inventory.Open();

    }
    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
        isInventoryOpen = false;
    }
}
