using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public float mouseSennsitivity;
    public Transform playerBody;
    private float xRotation = 0f;
    public bool pauseGame;  // this is to use inventory
    public Camera cam;
    Pickup pickupScript;
    public GameObject cursor;
    public GameObject inventoryObject;
    public GameObject inventoryButtons;
    public GameObject craftingRecipes;
    public GameObject craftController;
    public bool openChest;
    public bool cookMenu;
    public bool saveFriend;
    bool canEndGame;
    public GameObject engineUI;
    public GameObject endGameUI;
    bool endGame;
    bool actualPauseGame;   // apologies I used poor naming conventions previously this is used to pause game
    public GameObject pauseMenu;
    public bool hasDied;
    public GameObject journalObject;
    public GameObject strikeThroughJournal;
    bool journalOpen;
    PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        inventoryObject.SetActive(false);
        openChest = false;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasDied) 
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && !actualPauseGame) //used to activate hand icon when camera is hovering over items that can be interacted with
            {
                Transform objectHit = hit.transform;
                float distanceToObject = Vector3.Distance(hit.transform.position, transform.position);
                if (distanceToObject <= 5 && (hit.transform.gameObject.layer == LayerMask.NameToLayer("Pickup") || hit.transform.gameObject.layer == LayerMask.NameToLayer("Chest")) || hit.transform.gameObject.layer == LayerMask.NameToLayer("boat"))
                {
                    cursor.SetActive(true);
                }
                else
                {
                    cursor.SetActive(false);
                }
                if (objectHit.GetComponent<Pickup>() != null && Input.GetButtonDown("Interact") && distanceToObject <= 5)
                {
                    pickupScript = objectHit.GetComponent<Pickup>();
                    pickupScript.pickedUp = true;
                    //Debug.Log(objectHit.name.ToString());
                }
                if (objectHit.gameObject.tag == "campfire" && Input.GetButtonDown("Interact") && distanceToObject <= 10)
                {
                    craftController.GetComponent<cookingScript>().canCook();
                    cookMenu = true;
                    Debug.Log(objectHit.name.ToString());
                }
                if (objectHit.GetComponent<ChestScript>() != null && Input.GetButtonDown("Interact") && distanceToObject <= 10)
                {
                    openChest = true;
                }
                if (objectHit.gameObject.tag == "capturedFriend" && Input.GetButtonDown("Interact") && distanceToObject <= 10)
                {
                    saveFriend = true;
                }
                if (objectHit.gameObject.tag == "Engine" && Input.GetButtonDown("Interact") && distanceToObject <= 30)
                {
                    canEndGame = true;
                    strikeThroughJournal.SetActive(true);
                    Destroy(objectHit.gameObject);
                    engineUI.GetComponent<Animator>().SetBool("acquired", true);
                }
                if (objectHit.gameObject.tag == "boat" && Input.GetButtonDown("Interact") && distanceToObject <= 40 && canEndGame)
                {
                    endGame = true;
                    endGameUI.SetActive(true);
                    //Debug.Log("End Game");
                }
                if(Input.GetButtonDown("Journal")) //shows inventory
                {
                    journalOpen = !journalOpen;
                    if(journalOpen)
                    {
                        inventoryObject.SetActive(false);
                        inventoryButtons.SetActive(false);
                        journalObject.SetActive(true);
                    }
                    else
                    {
                        journalObject.SetActive(false);
                    }
                }
            }


            if (pauseGame || openChest || cookMenu || endGame || actualPauseGame) //unlocks mouse
            {
                Cursor.lockState = CursorLockMode.None;
                playerScript.GameNotPaused = true;
            }
            else //locks mouse
            {
                playerScript.GameNotPaused = false;
                Cursor.lockState = CursorLockMode.Locked;
                float mouseX = Input.GetAxis("Mouse X") * mouseSennsitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSennsitivity * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);
            }

            if (Input.GetButtonDown("Pause")) //used to show inventory
            {
                pauseGame = !pauseGame;
                if (pauseGame)
                {
                    inventoryObject.SetActive(true);
                    inventoryButtons.SetActive(false);
                    journalObject.SetActive(false);
                }
                else
                {
                    StartCoroutine("unpauseCooldown", 0.0f);
                }
            }

            if (Input.GetButtonDown("PauseGame")) //pause game
            {
                actualPauseGame = !actualPauseGame;

                if (actualPauseGame)
                {
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0.0f;
                }
                else
                {
                    pauseMenu.SetActive(false);
                    Time.timeScale = 1.0f;
                }
            }

            if (Input.GetButtonDown("Interact")) //pickup items
            {
                /* RaycastHit newhit;
                 Ray newray = cam.ScreenPointToRay(Input.mousePosition);*/

                if (Physics.Raycast(ray, out hit))
                {
                    Transform objectHit = hit.transform;
                    if (objectHit.GetComponent<Pickup>() != null)
                    {
                        pickupScript = objectHit.GetComponent<Pickup>();
                        pickupScript.pickedUp = true;
                        //Debug.Log(objectHit.name.ToString());
                    }
                }
            }
        }
    }

    public void UnPauseGame()
    {
        actualPauseGame = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    IEnumerator unpauseCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        inventoryObject.SetActive(false);
        craftingRecipes.SetActive(false);
    }


}
