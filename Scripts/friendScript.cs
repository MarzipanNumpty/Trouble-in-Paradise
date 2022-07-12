using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friendScript : MonoBehaviour
{
    MouseLook cameraScript;
    numberController numScript;
    bool saved;
    public Transform playerTranform;
    public GameObject sitting;
    public GameObject standing;
    void Start()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
        numScript = GameObject.FindGameObjectWithTag("Numbers").GetComponent<numberController>();
        playerTranform = cameraScript.playerBody;
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraScript.saveFriend && !saved && Vector3.Distance(transform.position, playerTranform.position) < 10) //if player is close enough and saves friends moves friend to boat and activates ui elements
        {
            saved = true;
            //Debug.Log("saved hooray");
            numScript.savedFriends++;
            numScript.savedFriendScoreUI.GetComponent<Animator>().SetBool("Saved", true);
            cameraScript.saveFriend = false;
            transform.position = numScript.savedTransforms[numScript.savedFriends - 1].position;
            sitting.SetActive(false);
            standing.SetActive(true);
        }
    }
}
