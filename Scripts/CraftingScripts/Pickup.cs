using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;
    public bool pickedUp;
    tutorialScript tutScript;
    bool outOfTutorial;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        tutScript = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<tutorialScript>();
    }

    private void Update()
    {
        if(pickedUp) //pickup items and add them to inventory
        {
            //Debug.Log("hit");
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    if (!tutScript.pickupTutComplete)
                    {
                        if (tutScript.stickPickedUp)
                        {
                            tutScript.rockPickedUp = true;
                        }
                        else
                        {
                            tutScript.stickPickedUp = true;
                        }
                    }
                    inventory.isFull[i] = true;
                    GameObject button = Instantiate(itemButton, inventory.slots[i].transform, false);
                    button.GetComponent<Spawn>().arrayPos = i;
                    Destroy(gameObject);
                    break;
                }
            }
            pickedUp = false;
        }
    }
}
