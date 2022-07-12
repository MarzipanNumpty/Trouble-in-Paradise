using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemsScript : MonoBehaviour
{
    public GameObject[] buttons;
    public string[] itemTags;
    public int buttonInUse;
    PlayerScript playerController;
    public bool useItem;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && useItem)
        {
            useItem = false;
            if(itemTags[buttonInUse - 1] == "uncookedMeat")
            {
                HealPlayer(20);
            }
            else if (itemTags[buttonInUse - 1] == "Meat")
            {
                HealPlayer(50);
            }
            else if (itemTags[buttonInUse - 1] == "greenHerb")
            {
                HealPlayer(10);
            }
            else if (itemTags[buttonInUse - 1] == "blueHerb")
            {
                thirst(50);
            }
            else if (itemTags[buttonInUse - 1] == "redHerb")
            {
                hunger(60);
            }
        }
    }

    void HealPlayer(int health)
    {
        if (playerController.CurrentHealth != playerController.MaxHealth)
        {
            playerController.consumableHealth(health);
            buttons[buttonInUse - 1].GetComponent<HotbarButton>().deleteItem = true;
        }
        else
        {
            //Debug.Log("No food today Dr Jones");
        }
    }

    void hunger(int food)
    {
        if(playerController.CurrentHunger < playerController.MaxHunger)
        {
            playerController.ChangeHunger(food);
            buttons[buttonInUse - 1].GetComponent<HotbarButton>().deleteItem = true;
        }
        else
        {
            //Debug.Log("No hunger to cure Dr Jones");
        }
    }

    void thirst(int drink)
    {
        if (playerController.CurrentThirst < playerController.MaxThirst)
        {
            playerController.ChangeThirst(drink);
            buttons[buttonInUse - 1].GetComponent<HotbarButton>().deleteItem = true;
        }
        else
        {
            //Debug.Log("Stop drinking Dr Jones");
        }
    }
}
