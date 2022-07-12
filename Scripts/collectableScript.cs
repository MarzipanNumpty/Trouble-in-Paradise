using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectableScript : MonoBehaviour
{
    int rotationSpeed = 40;
    public int bobbingPos = 40;

    void Update() //rotates object on the spot
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

}
