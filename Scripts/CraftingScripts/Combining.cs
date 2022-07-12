using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combining : MonoBehaviour
{
    Inventory inventory;
    recipesScript recipeScript;
    public GameObject[] craftingItems;
    public GameObject endObject;
    public GameObject hammer;
    public GameObject charcoal;
    public GameObject emptyBullet;
    public GameObject bullet;
    public GameObject stickBundle;
    public GameObject campfire;
    public GameObject blueHerb;
    public GameObject greenHerb;
    public GameObject yellowHerb;
    public GameObject redHerb;
    public bool craft;
    public int stickCheck;
    public int stickBundleCheck;
    public bool rockCheck;
    public bool hammerCheck;
    public bool gunpowderCheck;
    public bool lighterCheck;
    public bool charcoalCheck;
    public bool emptyBulletCheck;
    public bool bulletCheck;
    public bool metalCheck;
    public bool campfireCheck;
    public bool redFlowerCheck;
    public bool blueFlowerCheck;
    public bool greenFlowerCheck;
    public bool yellowFlowerCheck;
    public GameObject rockDesc;
    public GameObject stickDesc;
    public GameObject bulletDesc;
    public GameObject gunpowderDesc;
    public GameObject charcoalDesc;
    public GameObject bulletcasingDesc;
    public GameObject hammerDesc;
    public GameObject metalDesc;
    public GameObject lighterDesc;
    public GameObject blueFlowerDesc;
    public GameObject redFlowerDesc;
    public GameObject greenFlowerDesc;
    public GameObject bluePotionDesc;
    public GameObject redPotionDesc;
    public GameObject greenPotionDesc;
    public GameObject campfireDesc;
    public GameObject uncookedMeatDesc;
    public GameObject cookedMeatDesc;
    public GameObject stickBundleDesc;
    public bool completeCraft;
    public Transform craftSlot1;
    public Transform craftSlot2;
    public Transform endObjectTransform;
    public GameObject craftingEnd;
    public int craftItemCount;
    bool reConfirm;
    tutorialScript tutScript;
    PlayerScript playerScript;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        recipeScript = gameObject.GetComponent<recipesScript>();
        tutScript = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<tutorialScript>();
    }

    // Update is called once per frame
    void Update()
    {
        /*else if(craftingItems[0] != null && craftingItems[1] != null)
        {
            craftingItems[0] = null;
            stickCheck = false;
            rockCheck = false;
            hammerCheck = false;
        }

        if (completeCraft)
        {
            completeCraft = false;
            Destroy(craftingItems[0]);
            Destroy(craftingItems[1]);
            Destroy(craftingEnd);
            StartCoroutine("Wait", 0f);
            stickCheck = false;
            rockCheck = false;
            lighterCheck = false;
            metalCheck = false;
            gunpowderCheck = false;
            emptyBulletCheck = false;
            charcoalCheck = false;
        }*/

        if(craft) //called when player initiates the craft it checks the inventory for an empty space and spawns the craft end item into the free space 
        {
            craft = false;
            craftItemCount = 0;
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if(inventory.isFull[i] == false)
                {
                    if(!tutScript.firstCraftTutComplete)
                    {
                        tutScript.hammerCrafted = true;
                    }
                    if(endObject.tag == "Bullet")
                    {
                        playerScript.TotalBullets += 20;
                    }
                    else
                    {
                        inventory.isFull[i] = true;
                        GameObject spawnedObj = Instantiate(endObject, inventory.slots[i].transform, false);
                        spawnedObj.GetComponent<Spawn>().arrayPos = i;
                    }
                    //Debug.Log(spawnedObj.GetComponent<Spawn>().arrayPos + spawnedObj.gameObject.name);
                    endObject = null;
                    Destroy(craftingEnd);
                    craftingEnd = null;
                    break;
                }
            }
        }
    }

    public void recipeScreenActivate() //is used to add recipes to the recipe pages and is used to get the end object of each craft
    {
        recipeScript.hammerRec = false;
        recipeScript.bulletRec = false;
        recipeScript.charcoalRec = false;
        recipeScript.emptyBulletRec = false;
        recipeScript.bluePotionRec = false;
        recipeScript.redPotionRec = false;
        recipeScript.greenPotionRec = false;
        recipeScript.campfireRec = false;
        recipeScript.stickBundleRec = false;
        if (stickCheck == 1 && rockCheck)
        {
            endObject = hammer;
            recipeScript.hammerRec = true;
        }
        else if (lighterCheck && stickCheck == 1)
        {
            endObject = charcoal;
            recipeScript.charcoalRec = true;
        }
        else if (metalCheck && gunpowderCheck)
        {
            endObject = emptyBullet;
            recipeScript.emptyBulletRec = true;
        }
        else if (emptyBulletCheck && charcoalCheck)
        {
            endObject = bullet;
            recipeScript.bulletRec = true;
        }
        else if(stickCheck == 2)
        {
            endObject = stickBundle;
            recipeScript.stickBundleRec = true;
        }
        else if(stickBundleCheck == 2)
        {
            endObject = campfire;
            recipeScript.campfireRec = true;
        }
        else if(blueFlowerCheck && rockCheck)
        {
            endObject = blueHerb;
            recipeScript.bluePotionRec = true;
        }
        else if(redFlowerCheck && rockCheck)
        {
            endObject = redHerb;
            recipeScript.redPotionRec = true;
        }
        /*else if(yellowFlowerCheck && rockCheck)
        {
            endObject = yellowHerb;
        }*/
        else if(greenFlowerCheck && rockCheck)
        {
            endObject = greenHerb;
            recipeScript.greenPotionRec = true;
        }
    }

    public void Combine() //is used to check what items are being used for the craft 
    {
        if (reConfirm)
        {
            //Debug.Log("Can Confirm");
            reConfirm = false;
            craftItemCount = 0;
            stickCheck = 0;
            stickBundleCheck = 0;
            rockCheck = false;
            lighterCheck = false;
            metalCheck = false;
            gunpowderCheck = false;
            emptyBulletCheck = false;
            charcoalCheck = false;
            bulletCheck = false;
            blueFlowerCheck = false;
            redFlowerCheck = false;
            yellowFlowerCheck = false;
            greenFlowerCheck = false;
        }
        foreach (GameObject a in craftingItems) //fix issue with same endobject appearing
        {
            //Debug.Log(craftItemCount);
            if (a != null && craftItemCount < 2)
            {
                craftItemCount++;
                if (a.tag == "Stick")
                {
                    stickCheck++;
                }
                else if (a.tag == "Rock")
                {
                    rockCheck = true;
                }
                else if (a.tag == "Gunpowder")
                {
                    gunpowderCheck = true;
                }
                else if (a.tag == "Lighter")
                {
                    lighterCheck = true;
                }
                else if (a.tag == "Charcoal")
                {
                    charcoalCheck = true;
                }
                else if (a.tag == "Emptybullet")
                {
                    emptyBulletCheck = true;
                }
                else if (a.tag == "Bullet")
                {
                    bulletCheck = true;
                }
                else if (a.tag == "Metal")
                {
                    metalCheck = true;
                }
                else if (a.tag == "StickBundle")
                {
                    stickBundleCheck++;
                }
                else if (a.tag == "blueFlower")
                {
                    blueFlowerCheck = true;
                }
                else if (a.tag == "redFlower")
                {
                    redFlowerCheck = true;
                }
                else if (a.tag == "yellowFlower")
                {
                    yellowFlowerCheck = true;
                }
                else if (a.tag == "greenFlower")
                {
                    greenFlowerCheck = true;
                }
                /*if(endObject != null)
                {
                    Instantiate(endObject, endObjectTransform, false);
                }*/
                if (craftItemCount == 2)
                {
                    recipeScreenActivate();
                    if (endObject != null)
                    {
                        craftingEnd = Instantiate(endObject, endObjectTransform, false);
                    }
                    recipeScript.recipeUpdate();
                }
            }
        }
    }

    IEnumerator Wait() //a small wait used so items can be deleted then the crafted item is put in inventory
    {
        yield return new WaitForSeconds(0.1f);
        craft = true;
    }

    public void confirmCraft() //resets all values
    {
        if(craftingEnd != null)
        {
            //completeCraft = false;
            Destroy(craftingItems[0]);
            Destroy(craftingItems[1]);
            Destroy(craftingEnd);
            StartCoroutine("Wait", 0f);
            stickCheck = 0;
            stickBundleCheck = 0;
            rockCheck = false;
            lighterCheck = false;
            metalCheck = false;
            gunpowderCheck = false;
            emptyBulletCheck = false;
            charcoalCheck = false;
            blueFlowerCheck = false;
            redFlowerCheck = false;
            yellowFlowerCheck = false;
            greenFlowerCheck = false;
        }
    }

    public void cancelCraftSlot1() //resets the first crafting slot
    {
        if(craftingItems[0] != null)
        {
            reConfirm = true;
            craftingItems[0].transform.position = craftingItems[0].transform.parent.position;
            craftingItems[0] = null;
            if(craftingEnd != null)
            {
                Destroy(craftingEnd);
            }
            endObject = null;
            craftItemCount--;
            Combine();
        }
    }

    public void cancelCraftSlot2()//resets the second crafting slot
    {
        if(craftingItems[1] != null)
        {
            reConfirm = true;
            craftingItems[1].transform.position = craftingItems[1].transform.parent.position;
            craftingItems[1] = null;
            if (craftingEnd != null)
            {
                Destroy(craftingEnd);
            }
            endObject = null;
            craftItemCount--;
            Combine();
        }
    }

}

/*
 public class Combining : MonoBehaviour
{
    public Inventory inventory;
    public GameObject[] craftingItems;
    public GameObject endObject;
    public GameObject hammer;
    public GameObject charcoal;
    public GameObject emptyBullet;
    public GameObject bullet;
    public bool craft;
    public bool stickCheck;
    public bool rockCheck;
    public bool hammerCheck;
    public bool gunpowderCheck;
    public bool lighterCheck;
    public bool charcoalCheck;
    public bool emptyBulletCheck;
    public bool bulletCheck;
    public bool metalCheck;
    public GameObject rockDesc;
    public GameObject stickDesc;
    public GameObject bulletDesc;
    public GameObject gunpowderDesc;
    public GameObject charcoalDesc;
    public GameObject bulletcasingDesc;
    public GameObject hammerDesc;
    public GameObject metalDesc;
    public GameObject lighterDesc;
    public bool completeCraft;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stickCheck && rockCheck)
        {
            Destroy(craftingItems[0]);
            Destroy(craftingItems[1]);
            StartCoroutine("Wait", 0f);
            endObject = hammer;
            stickCheck = false;
            rockCheck = false;
        }
        if(lighterCheck && stickCheck)
        {
            Destroy(craftingItems[0]);
            Destroy(craftingItems[1]);
            StartCoroutine("Wait", 0f);
            endObject = charcoal;
            lighterCheck = false;
            stickCheck = false;
        }
        if(metalCheck && gunpowderCheck)
        {
            Destroy(craftingItems[0]);
            Destroy(craftingItems[1]);
            StartCoroutine("Wait", 0f);
            endObject = emptyBullet;
            metalCheck = false;
            gunpowderCheck = false;
        }
        if(emptyBulletCheck && charcoalCheck)
        {
            Destroy(craftingItems[0]);
            Destroy(craftingItems[1]);
            StartCoroutine("Wait", 0f);
            endObject = bullet;
            emptyBulletCheck = false;
            charcoalCheck = false;
        }
        /*else if(craftingItems[0] != null && craftingItems[1] != null)
        {
            craftingItems[0] = null;
            stickCheck = false;
            rockCheck = false;
            hammerCheck = false;
        }

if (completeCraft)
{
    completeCraft = false;
    Destroy(craftingItems[0]);
    Destroy(craftingItems[1]);
    stickCheck = false;
    rockCheck = false;
    lighterCheck = false;
    stickCheck = false;
}

if (craft)
{
    craft = false;
    for (int i = 0; i < inventory.slots.Length; i++)
    {
        if (inventory.isFull[i] == false)
        {
            inventory.isFull[i] = true;
            Instantiate(endObject, inventory.slots[i].transform, false);
            endObject = null;
            break;
        }
    }
}
    }

    public void Combine()
{
    foreach (GameObject a in craftingItems)
    {
        if (a.tag == "Stick")
        {
            stickCheck = true;
        }
        else if (a.tag == "Rock")
        {
            rockCheck = true;
        }
        else if (a.tag == "Gunpowder")
        {
            gunpowderCheck = true;
        }
        else if (a.tag == "Lighter")
        {
            lighterCheck = true;
        }
        else if (a.tag == "Charcoal")
        {
            charcoalCheck = true;
        }
        else if (a.tag == "Emptybullet")
        {
            emptyBulletCheck = true;
        }
        else if (a.tag == "Bullet")
        {
            bulletCheck = true;
        }
        else if (a.tag == "Metal")
        {
            metalCheck = true;
        }
    }
}

IEnumerator Wait()
{
    yield return new WaitForSeconds(0.1f);
    craft = true;
}

public void confirmCraft()
{

}

}*/
