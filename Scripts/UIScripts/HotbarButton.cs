using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class HotbarButton : MonoBehaviour
{
    public event Action<int> OnButtonClicked;
    private KeyCode keyCode;
    private TMP_Text text;
    public int keyNum;
    Hotbar hotbarScript;
    MouseLook camScript;
    GameObject hotbarItem;
    Transform itemsSpawnPoint;
    public GameObject itemToCompare;
    public bool takenArguement;
    public string itemTag;
    PlacementScript objectScript;
    Inventory inventory;
    bool placeable;
    public bool deleteItem;
    UseItemsScript itemUsageScript;
    numberController numScript;
    tutorialScript tutScript;
    PlayerScript playerScript;
    string[] healingItemTags = { "uncookedMeat", "Meat", "greenHerb", "blueHerb", "redHerb" };


   /* private void OnValidate()
    {
        keyNum = transform.GetSiblingIndex() + 1;
        keyCode = KeyCode.Alpha0 + keyNum;

        if(text == null)
        {
            text = GetComponentInChildren<TMP_Text>();
        }

        text.SetText(keyNum.ToString());
        gameObject.name = "Hotbar Button " + keyNum;
    }*/

    private void Awake()
    {
        hotbarScript = GameObject.FindGameObjectWithTag("hotbar").GetComponent<Hotbar>();
        camScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
        itemsSpawnPoint = GameObject.FindGameObjectWithTag("handSpawn").GetComponent<Transform>();
        objectScript = GameObject.FindGameObjectWithTag("Placement").GetComponent<PlacementScript>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        itemUsageScript = GameObject.FindGameObjectWithTag("ItemUsage").GetComponent<UseItemsScript>();
        numScript = GameObject.FindGameObjectWithTag("Numbers").GetComponent<numberController>();
        tutScript = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<tutorialScript>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        //itemUsageScript.buttons[keyNum - 1] = gameObject;
        GetComponent<Button>().onClick.AddListener(HandleClick);
        keyCode = KeyCode.Alpha0 + keyNum;
        if (text == null)
        {
            text = GetComponentInChildren<TMP_Text>();
        }
        text.SetText(keyNum.ToString());
    }

    private void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
            HandleClick();
        }
        if(hotbarScript.firePlaced && placeable && hotbarScript.buttonNum == keyNum) //used to destroy campfire items and ui elements
        {
            takenArguement = false;
            hotbarScript.firePlaced = false;
            objectScript.placeObject = false;
            Destroy(inventory.slots[hotbarItem.GetComponent<Spawn>().arrayPos].transform.GetChild(0).gameObject);
            Destroy(hotbarScript.spawnedItem);
            destroyItem();
        }

        if(hotbarItem != null) //used to destroy items and ui elements when used
        {

            if(inventory.slots[hotbarItem.GetComponent<Spawn>().arrayPos].transform.GetChild(0).gameObject.GetComponent<Spawn>().hotbarArrayPos != keyNum || deleteItem)
            {
                if (hotbarScript.spawnedItem != null)
                {
                    if (itemTag == hotbarScript.spawnedItem.tag)
                    {
                        Destroy(hotbarScript.spawnedItem);
                    }
                }
                if (deleteItem)
                {
                    deleteItem = false;
                    Destroy(inventory.slots[hotbarItem.GetComponent<Spawn>().arrayPos].transform.GetChild(0).gameObject);
                }
                destroyItem();
            }

            
        }
    }

    public void destroyItem() //destroy item
    {
        numScript.meleeWeapon = false;
        itemUsageScript.itemTags[keyNum - 1] = null;
        takenArguement = false;
        Destroy(hotbarItem);
    }

    private void HandleClick()
    {
        OnButtonClicked?.Invoke(keyNum);
        hotbarScript.buttonNum = keyNum;
        if(camScript.pauseGame) //when item is being addded to hotbar makes sure it is empty and then put it in hotbar
        {
            //Debug.Log("cam");
            if (hotbarScript.itemToBeHeld != null)
            {
                if (hotbarItem == null)
                {
                    //Debug.Log("cam1");
                    itemToCompare = hotbarScript.itemToBeHeld;
                    takenArguement = true;
                    hotbarItem = Instantiate(hotbarScript.itemToBeHeld, transform, false);
                }
                else
                {
                    //Debug.Log("cam2");
                    itemToCompare = hotbarScript.itemToBeHeld;
                    Destroy(hotbarItem);
                    hotbarItem = Instantiate(hotbarScript.itemToBeHeld, transform, false);
                }
                itemTag = itemToCompare.GetComponent<Spawn>().item.tag;
                inventory.slots[hotbarItem.GetComponent<Spawn>().arrayPos].transform.GetChild(0).gameObject.GetComponent<Spawn>().hotbarArrayPos = keyNum;
                itemUsageScript.itemTags[keyNum - 1] = hotbarItem.tag;
            }
            else
            {
                Destroy(hotbarItem);
            }

            if (hotbarItem.tag == "campfire")
            {
                objectScript.buttonToPress = keyNum;
            }

            if(hotbarItem.tag == "Hammer" && tutScript.backToInvTutComplete && !tutScript.hotbarTutComplete)
            {
                tutScript.hammerHotKeyed = true;
            }

        }
        else //if hotbar button is pressed equip item 
        {
            playerScript.PistolEquipped = false;
            itemUsageScript.useItem = false;
            itemUsageScript.buttonInUse = keyNum;
            if (transform.childCount > 1)
            {
                objectScript.placeObject = false;
                numScript.meleeWeapon = false;
                if (hotbarScript.spawnedItem == null)
                {
                    hotbarScript.spawnedItem = Instantiate(gameObject.transform.GetChild(1).gameObject.GetComponent<Spawn>().item, itemsSpawnPoint, false);
                    if(hotbarItem.tag == "Gun")
                    {
                        playerScript.UpdateCurrentGun(hotbarScript.spawnedItem);
                        playerScript.PistolEquipped = true;
                    }
                }
                else
                {
                    Destroy(hotbarScript.spawnedItem);
                    hotbarScript.spawnedItem = Instantiate(gameObject.transform.GetChild(1).gameObject.GetComponent<Spawn>().item, itemsSpawnPoint, false);
                    if (hotbarItem.tag == "Gun")
                    {
                        playerScript.UpdateCurrentGun(hotbarScript.spawnedItem);
                        playerScript.PistolEquipped = true;
                    }
                }
                if (hotbarScript.spawnedItem.tag == "campfire")
                {
                    //Debug.Log("like the fastest boii in the west I have came and gone");
                    objectScript.placeObject = true;
                    placeable = true;
                }
                else
                {
                    objectScript.placeObject = false;
                    placeable = false;
                }
                if(hotbarScript.spawnedItem.tag == "Hammer")
                {
                    numScript.meleeWeapon = true;

                    if(tutScript.hotbarTutComplete && !tutScript.equipHammerTutComplete)
                    {
                        tutScript.hammerEquipped = true;
                    }
                }
                if (healingItemTags.Contains(hotbarItem.tag))
                {
                    itemUsageScript.useItem = true;
                }
            }
            else
            {
                Destroy(hotbarScript.spawnedItem);
            }
        }
    }
}
