using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    Inventory inventory;
    ChestScript chestInv;
    Combining combining;
    MouseLook camScript;
    Hotbar hotbarScript;
    public int i;
    public bool isChest;
    public GameObject chest;
    public bool craftSlot1;
    public bool craftSlot2;
    tutorialScript tutScript;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        combining = GameObject.FindGameObjectWithTag("recipe").GetComponent<Combining>();
        camScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
        hotbarScript = GameObject.FindGameObjectWithTag("hotbar").GetComponent<Hotbar>();
        tutScript = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<tutorialScript>();
        if (isChest)
        {
            chestInv = chest.GetComponent<ChestScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isChest)
        {
            if (transform.childCount <= 0)
            {
                chestInv.isFull[i] = false;
            }
        }
        else
        {
            if (transform.childCount <= 0)
            {
                inventory.isFull[i] = false;
            }
        }
        if (camScript.pauseGame == false)
        {
            hotbarScript.currentButton = null;
            hotbarScript.itemToBeHeld = null;
            descActive();
        }
        if (combining.craftingItems[0] == null)
        {
            craftSlot1 = false;
        }
        if (combining.craftingItems[1] == null)
        {
            craftSlot2 = false;
        }
    }

    public void dropItem()
    {
        craftSlot1 = false;
        craftSlot2 = false;
        foreach(Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnDroppedItems();
            Destroy(child.gameObject);
        }
    }

    public void Combine()
    {
        if(craftSlot1 == false && craftSlot2 == false && tutScript.enterInvTutComplete)
        {
            descActive();
            foreach (Transform child in transform)
            {
                if (combining.craftingItems[0] == null)
                {
                    craftSlot1 = true;
                    combining.craftingItems[0] = child.gameObject;
                    child.gameObject.transform.position = combining.craftSlot1.position;
                }
                else if(combining.craftingItems[1] == null)
                {
                    craftSlot2 = true;
                    combining.craftingItems[1] = child.gameObject;
                    child.gameObject.transform.position = combining.craftSlot2.position;
                }
            }

            if (combining.craftingItems[0] != null && combining.craftingItems[1] != null)
            {
                combining.Combine();
            }
        }
    }

    public void reaccessItems1()
    {
        craftSlot1 = false;
    }

    public void reaccessItems2()
    {
        craftSlot2 = false;
    }

    public void itemDesc()
    {
        hotbarScript.currentButton = gameObject;
        if (transform.childCount > 0)
        {
            hotbarScript.itemToBeHeld = gameObject.transform.GetChild(0).gameObject;
        }
        //Debug.Log(hotbarScript.currentButton + " " + hotbarScript.itemToBeHeld);
        descActive();
        foreach (Transform child in transform)
        {
            if (child.tag == "Stick")
            {
                combining.stickDesc.SetActive(true);
            }
            else if (child.tag == "Rock")
            {
                combining.rockDesc.SetActive(true);
            }
            else if (child.tag == "Gunpowder")
            {
                combining.gunpowderDesc.SetActive(true);
            }
            else if (child.tag == "Lighter")
            {
                combining.lighterDesc.SetActive(true);
            }
            else if (child.tag == "Charcoal")
            {
                combining.charcoalDesc.SetActive(true);
            }
            else if (child.tag == "Emptybullet")
            {
                combining.bulletcasingDesc.SetActive(true);
            }
            else if (child.tag == "Bullet")
            {
                combining.bulletDesc.SetActive(true);
            }
            else if (child.tag == "Metal")
            {
                combining.metalDesc.SetActive(true);
            }
            else if (child.tag == "Hammer")
            {
                combining.hammerDesc.SetActive(true);
            }
            else if (child.tag == "blueFlower")
            {
                combining.blueFlowerDesc.SetActive(true);
            }
            else if (child.tag == "redFlower")
            {
                combining.redFlowerDesc.SetActive(true);
            }
            else if (child.tag == "greenFlower")
            {
                combining.greenFlowerDesc.SetActive(true);
            }
            else if (child.tag == "blueHerb")
            {
                combining.bluePotionDesc.SetActive(true);
            }
            else if (child.tag == "redHerb")
            {
                combining.redPotionDesc.SetActive(true);
            }
            else if (child.tag == "greenHerb")
            {
                combining.greenPotionDesc.SetActive(true);
            }
            else if (child.tag == "campfire")
            {
                combining.campfireDesc.SetActive(true);
            }
            else if (child.tag == "uncookedMeat")
            {
                combining.uncookedMeatDesc.SetActive(true);
            }
            else if (child.tag == "Meat")
            {
                combining.cookedMeatDesc.SetActive(true);
            }
            else if (child.tag == "StickBundle")
            {
                combining.stickBundleDesc.SetActive(true);
            }
        }
    }

    public void descActive()
    {
        combining.rockDesc.SetActive(false);
        combining.stickDesc.SetActive(false);
        combining.bulletDesc.SetActive(false);
        combining.gunpowderDesc.SetActive(false);
        combining.charcoalDesc.SetActive(false);
        combining.bulletcasingDesc.SetActive(false);
        combining.hammerDesc.SetActive(false);
        combining.metalDesc.SetActive(false);
        combining.lighterDesc.SetActive(false);
        combining.blueFlowerDesc.SetActive(false);
        combining.redFlowerDesc.SetActive(false);
        combining.greenFlowerDesc.SetActive(false);
        combining.bluePotionDesc.SetActive(false);
        combining.redPotionDesc.SetActive(false);
        combining.greenPotionDesc.SetActive(false);
        combining.campfireDesc.SetActive(false);
        combining.uncookedMeatDesc.SetActive(false);
        combining.cookedMeatDesc.SetActive(false);
        combining.stickBundleDesc.SetActive(false);
    }
}
