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

    [SerializeField]
    private RectTransform scrollRect;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private Scrollbar inventorySlider;

    [SerializeField]
    private float sliderStep;

    private RectTransform selectedRect;
    private GameObject lastSelected;

    //animation stuff
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool("IsOpen", UIManager.Instance.isInventoryOpen);

        //get the top and bottom positions of the selected item
        //if the top of the selected item is above the content panel Anchor max Y
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        if(selected == null) { return; }
        if(selected.transform.parent != contentPanel.transform) { return; }
        if (selected == lastSelected) { return; }

        selectedRect = selected.GetComponent<RectTransform>();

        float selectedPos = Mathf.Abs(selectedRect.anchoredPosition.y) + selectedRect.rect.height;
        float scrollMinY = contentPanel.anchoredPosition.y;
        float scrollMaxY = contentPanel.anchoredPosition.y + scrollRect.rect.height;

        if(selectedPos > scrollMaxY)
        {
            if(inventorySlider.value > 0 )
            {
                inventorySlider.value -= sliderStep * Time.deltaTime;

            }
        }
        if(Mathf.Abs(selectedRect.anchoredPosition.y) - selectedRect.rect.height < scrollMinY)
        {
            if(inventorySlider.value < 1)
            {

                inventorySlider.value += sliderStep * Time.deltaTime;
            }
        }
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

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
