using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InspectPanel : MonoBehaviour
{
    //set the values for inspect panel

    [SerializeField]
    private Image itemSprite;

    [SerializeField]
    private TextMeshProUGUI itemName;

    [SerializeField]
    private TextMeshProUGUI itemDescription;

    public void SetInspect(Sprite sprite, string name, string description)
    {
        Debug.Log("Inspecting Item");

        itemSprite.sprite = sprite;
        itemName.text = name;
        itemDescription.text = description;
    }
}
