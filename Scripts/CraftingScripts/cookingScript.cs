using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookingScript : MonoBehaviour
{
    Inventory inventory;
    MouseLook cameraScript;
    public GameObject cookMenu;
    public GameObject cookedMeat;
    public GameObject completedCook;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
        cookMenu.SetActive(false);
        completedCook.SetActive(false);
    }

    // Update is called once per frame
    public void canCook() //shows cooking menu
    {
        cookMenu.SetActive(true);
    }

    public void cantCook() //shows the next cooking menu
    {
        completedCook.SetActive(false);
        cookMenu.SetActive(false);
        cameraScript.cookMenu = false;
    }

    public void willCook() //checks the inventory for uncooked meat and changes it to cooked meat
    {
        foreach (GameObject a in inventory.slots)
        {
            if(a.transform.childCount > 0)
            {
                if (a.transform.GetChild(0).gameObject.tag == "uncookedMeat")
                {
                    GameObject item = Instantiate(cookedMeat, a.transform, false);
                    item.GetComponent<Spawn>().arrayPos = a.transform.gameObject.GetComponent<Slot>().i;
                    Destroy(a.transform.GetChild(0).gameObject);
                }
            }
        }
        cookMenu.SetActive(false);
        completedCook.SetActive(true);
    }
}

