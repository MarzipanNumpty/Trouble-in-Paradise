using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
    public GameObject inventoryButtons;
    public GameObject inventoryObjects;
    MouseLook cameraScript;
    public bool[] isFull;
    public GameObject[] slots;
    public bool openChest;
    public bool openedChest;
    void Start()
    {
        inventoryObjects.SetActive(false);
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
        openedChest = true;
    }

    // Update is called once per frame
    void Update()
    {
        openChest = cameraScript.openChest;
        if(openChest && openedChest)
        {
            chestContents();
        }
        
    }

    public void closeChest() //hides chest inventory
    {
        cameraScript.openChest = false;
        openedChest = true;
        inventoryObjects.SetActive(false);
    }

    public void chestContents() //shows chest inventory
    {
        if (openChest && openedChest)
        {
            openedChest = false;
            inventoryObjects.SetActive(true);
            inventoryButtons.SetActive(false);
        }
    }
}
