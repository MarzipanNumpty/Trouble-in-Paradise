using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    public GameObject currentButton;
    public GameObject itemToBeHeld;
    public GameObject spawnedItem;
    public GameObject itemDeleted;
    public int buttonAccepts;
    public bool firePlaced;
    public bool fireNearby;
    public int buttonNum;
    private void Awake()
    {
        foreach(var button in GetComponentsInChildren<HotbarButton>())
        {
            button.OnButtonClicked += ButtonOnOnButtonClicked;
        }

    }

    private void ButtonOnOnButtonClicked(int buttonNumber)
    {
        //Debug.Log($"Button {buttonNumber} clicked!");
    }
}
