using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementScript : MonoBehaviour
{
    [SerializeField]
    private GameObject placeableObject;


    //public KeyCode objectHotKey;

    public int buttonToPress;

    private GameObject currentPlaceableObject;
    private float mouseWheelRotation;
    public bool canMove;
    public int num;
    placeableObjectScript objectScript;
    public bool placeObject;
    Hotbar hotbarScript;
    bool haveItem;

    private void Start()
    {
        hotbarScript = GameObject.FindGameObjectWithTag("hotbar").GetComponent<Hotbar>();
    }

    // Update is called once per frame
    void Update()
    {
        if(placeObject) //is used to move campfire and rotate campfire
        {
            HandleObjectHotKey();

            if (currentPlaceableObject != null)
            {
                MoveObjectToMouse();
                RotateObject();
                ReleaseObject();
            }
            if(haveItem)
            {
                if (hotbarScript.fireNearby)
                {
                    objectScript.fireNearby = true;
                }
                else
                {
                    objectScript.fireNearby = false;
                }
            }
        }
        else if(currentPlaceableObject != null)
        {
            Destroy(currentPlaceableObject);
        }
    }

    private void RotateObject() //rotates campfire
    {
        if(canMove)
        {
            mouseWheelRotation += Input.mouseScrollDelta.y;
            currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
        }
    }

    private void ReleaseObject() //places campfire
    {
        if(Input.GetMouseButtonDown(1) && objectScript.canBePlaced)
        {
            currentPlaceableObject = null;
            objectScript.Placed();
            hotbarScript.firePlaced = true;
            haveItem = false;
        }
    }

    private void MoveObjectToMouse() //moves campfire to where mouse position is
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo) )
        {
            if(hitInfo.collider.gameObject.layer != 13)
            {
                float dist = Vector3.Distance(Camera.main.transform.position, hitInfo.point);
                if (dist < num)
                {
                    canMove = true;
                }
                else
                {
                    canMove = false;
                }
                if (canMove)
                {
                    currentPlaceableObject.transform.position = hitInfo.point;
                    currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                }
            }
        }
    }

    private void HandleObjectHotKey() //places fire
    {
        if(Input.GetKeyDown(buttonToPress.ToString()))
        {
            if(currentPlaceableObject == null)
            {
                currentPlaceableObject = Instantiate(placeableObject);
                objectScript = currentPlaceableObject.GetComponent<placeableObjectScript>();
                haveItem = true;
            }
            else
            {
                haveItem = false;
                Destroy(currentPlaceableObject);
            }
        }
    }
}
