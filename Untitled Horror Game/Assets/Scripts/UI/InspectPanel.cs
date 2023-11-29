using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InspectPanel : MonoBehaviour
{
    //set the values for inspect panel

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Image itemSprite;

    [SerializeField]
    private TextMeshProUGUI itemName;

    [SerializeField]
    private TextMeshProUGUI itemDescription;

    //add flip button

    public void Open()
    {
        animator.SetBool("IsOpen", true);
    }
    public void Close()
    {
        animator.SetBool("IsOpen", false);
    }

    public void SetInspect(Sprite sprite, string name, string description)
    {
        Debug.Log("Inspecting Item");

        itemSprite.sprite = sprite;
        itemName.text = name;
        itemDescription.text = description;
    }
}
