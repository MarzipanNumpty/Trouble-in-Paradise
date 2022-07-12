using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friendsSCoreUIScript : MonoBehaviour
{
    public GameObject numObject;

    public void changenum() //changes ui text number
    {
        numObject.GetComponent<numberController>().changeText();
    }
}
