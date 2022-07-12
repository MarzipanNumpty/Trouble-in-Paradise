using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialScript : MonoBehaviour  
{
    public bool wPressed;
    public bool aPressed;
    public bool sPressed;
    public bool dPressed;
    public bool spacePressed;
    public float healthUITimer = 10.0f;
    public bool rockPickedUp;
    public bool stickPickedUp;
    public bool hammerCrafted;
    public bool enteredRecipePage;
    public bool wentBackToInv;
    public bool hammerHotKeyed;
    public bool hammerEquipped;
    public int journalOpenCount;
    public float saveFriendCounter = 15.0f;
    public float endOfTutTimer = 15.0f;
    public bool movementTutComplete;
    public bool healthUITutComplete;
    public bool pickupTutComplete;
    public bool enterInvTutComplete;
    public bool firstCraftTutComplete;
    public bool recipeTutComplete;
    public bool backToInvTutComplete;
    public bool hotbarTutComplete;
    public bool equipHammerTutComplete;
    public bool journalTutComplete;
    public bool saveFriendTutComplete;
    public bool tutorialEndScreen;
    public bool tutorialFinished;
    public GameObject movementTut;
    public GameObject healthUITut;
    public GameObject pickupTut;
    public GameObject enterInvTut;
    public GameObject firstCraftTut;
    public GameObject recipeTut;
    public GameObject backToInvTut;
    public GameObject hotbarTut;
    public GameObject equipHammerTut;
    public GameObject journalTut;
    public GameObject saveFriendTut;
    public GameObject endOfTut;
    public GameObject islandBlocker;
    public GameObject villageBlocker;
    // Start is called before the first frame update
    void Start()
    {
        movementTut.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(!tutorialFinished)
        {
            if (!movementTutComplete)
            {
                if (wPressed && aPressed && sPressed && dPressed && spacePressed)
                {
                    movementTutComplete = true;
                    movementTut.SetActive(false);
                    healthUITut.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    wPressed = true;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    aPressed = true;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    sPressed = true;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    dPressed = true;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    spacePressed = true;
                }
            }
            else if (!healthUITutComplete)
            {
                healthUITimer -= Time.deltaTime;
                if (healthUITimer <= 0)
                {
                    healthUITutComplete = true;
                    healthUITut.SetActive(false);
                    pickupTut.SetActive(true);
                }
            }
            else if (!pickupTutComplete)
            {
                if (rockPickedUp && stickPickedUp)
                {
                    pickupTutComplete = true;
                    pickupTut.SetActive(false);
                    enterInvTut.SetActive(true);
                }
            }
            else if (!enterInvTutComplete)
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    enterInvTutComplete = true;
                    enterInvTut.SetActive(false);
                    firstCraftTut.SetActive(true);
                }
            }
            else if (!firstCraftTutComplete)
            {
                if (hammerCrafted)
                {
                    firstCraftTutComplete = true;
                    firstCraftTut.SetActive(false);
                    recipeTut.SetActive(true);
                }
            }
            else if (!recipeTutComplete)
            {
                if (enteredRecipePage)
                {
                    recipeTutComplete = true;
                    recipeTut.SetActive(false);
                    backToInvTut.SetActive(true);
                }
            }
            else if (!backToInvTutComplete)
            {
                if (wentBackToInv)
                {
                    backToInvTutComplete = true;
                    backToInvTut.SetActive(false);
                    hotbarTut.SetActive(true);
                }
            }
            else if (!hotbarTutComplete)
            {
                if (hammerHotKeyed)
                {
                    hotbarTutComplete = true;
                    hotbarTut.SetActive(false);
                    equipHammerTut.SetActive(true);
                }
            }
            else if (!equipHammerTutComplete)
            {
                if (hammerEquipped)
                {
                    equipHammerTutComplete = true;
                    equipHammerTut.SetActive(false);
                    journalTut.SetActive(true);
                }
            }
            else if (!journalTutComplete)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    journalOpenCount++;
                    journalTut.SetActive(false);
                }
                if (journalOpenCount >= 2)
                {
                    journalTutComplete = true;
                    saveFriendTut.SetActive(true);
                    Destroy(villageBlocker);
                }
            }
            else if (!saveFriendTutComplete)
            {
                saveFriendCounter -= Time.deltaTime;
                if (saveFriendCounter <= 0)
                {
                    saveFriendTutComplete = true;
                    saveFriendTut.SetActive(false);
                }
            }
            else if (tutorialEndScreen)
            {
                endOfTut.SetActive(true);
                endOfTutTimer -= Time.deltaTime;
                if (endOfTutTimer <= 0)
                {
                    endOfTut.SetActive(false);
                    Destroy(islandBlocker);
                    tutorialFinished = true;
                }
            }
        }
    }


    public void enterRecipe()
    {
        if(firstCraftTutComplete)
        {
            enteredRecipePage = true;
        }
    }

    public void exitRecipe()
    {
        if(recipeTutComplete)
        {
            wentBackToInv = true;
        }
    }
}
