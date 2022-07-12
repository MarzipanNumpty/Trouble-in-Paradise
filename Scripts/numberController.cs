using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class numberController : MonoBehaviour
{
    //this script is used for friends saved count plan to use for showcasing days passed
    //and the resulting ui that will be used to compliment these values
    //this script can also be used to hold any numbers that you dont want to be destroyed
    public int savedFriends;
    public Transform[] savedTransforms;
    int friendsToBeSaved = 4;
    public GameObject savedFriendScoreUI;
    public Text savedFriendTotalText;
    public GameObject finalDoors;
    public bool meleeWeapon;
    public GameObject strikeThroughJournal;
    public GameObject tutorialController;
    tutorialScript tutScript;

    public void changeText() //used to enable ui elements
    {
        if(savedFriends == 1)
        {
            tutScript = tutorialController.GetComponent<tutorialScript>();
            tutScript.tutorialEndScreen = true;
        }
        savedFriendTotalText.text = savedFriends.ToString();
        savedFriendScoreUI.GetComponent<Animator>().SetBool("Saved", false);
        if(savedFriends == friendsToBeSaved)
        {
            strikeThroughJournal.SetActive(true);
            Destroy(finalDoors);
        }
    }

}
