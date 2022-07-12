using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayCycle : MonoBehaviour
{

    // Update is called once per frame
    void Update() //rotates object around center of map
    {
        transform.RotateAround(Vector3.zero, Vector3.right, 1.0f * Time.deltaTime);
        transform.LookAt(Vector3.zero);       
    }
}
