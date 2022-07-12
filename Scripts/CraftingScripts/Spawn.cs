using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    public GameObject itemImage;
    Transform player;
    PlayerMovement playerScript;
    private ChestScript inventory;
    Inventory playerInventory;
    public bool putInChest;
    public int arrayPos;
    public int hotbarArrayPos;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    
    public void SpawnDroppedItems()
    {
        Debug.Log(gameObject.name.ToString());
        Debug.Log(playerScript.inChestArea + "playerscript");
        Debug.Log(putInChest + "chest");
        if(playerScript.inChestArea)
        {
            inventory = playerScript.chest.GetComponent<ChestScript>();
            if (inventory.openChest)
            {
                //Debug.Log("start");
                for (int a = 0; a < playerInventory.slots.Length; a++)
                {
                    if (playerInventory.isFull[a] == false)
                    {
                        playerInventory.isFull[a] = true;
                        Instantiate(itemImage, playerInventory.slots[a].transform, false);
                        //putInChest = false;
                        Destroy(gameObject);
                        break;
                    }
                }
            }
            else
            {
                //Debug.Log("start");
                inventory = playerScript.chest.GetComponent<ChestScript>();
                for (int i = 0; i < inventory.slots.Length; i++)
                {
                    if (inventory.isFull[i] == false)
                    {
                        inventory.isFull[i] = true;
                        Instantiate(itemImage, inventory.slots[i].transform, false);
                        Destroy(gameObject);
                        break;
                    }
                    else if(i == inventory.slots.Length)
                    {
                        break;
                    }
                }
            }
        }
        else
        {
            Vector3 playerpos = new Vector3(player.position.x + 1, player.position.y, player.position.z);
            Instantiate(item, playerpos, Quaternion.identity);
        }
    }
}
