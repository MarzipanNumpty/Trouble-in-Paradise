using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update() //makes object look at camera
    {
        transform.LookAt(Camera.main.transform);
    }
}
