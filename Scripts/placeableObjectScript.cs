using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeableObjectScript : MonoBehaviour
{
    Collider circleCol;
    Collider cubeCol;
    public bool canBePlaced;
    Color baseColor;
    fireParticleController fireScript;
    public bool fireNearby;
    void Start()
    {
        circleCol = gameObject.GetComponent<CapsuleCollider>();
        cubeCol = gameObject.GetComponent<BoxCollider>();
        cubeCol.enabled = false;
        baseColor = gameObject.GetComponent<Renderer>().material.color;
        canBePlaced = true;
        fireScript = gameObject.GetComponent<fireParticleController>();
    }

    public void Placed() //places campfire
    {
        fireScript.placed = true;
        circleCol.enabled = false;
        cubeCol.enabled = true;
        gameObject.GetComponent<Renderer>().material.color = baseColor;
        gameObject.layer = 10;
        Destroy(this);
    }

    /* private void OnTriggerEnter(Collider other)
     {
         Debug.Log(other.gameObject.name + "Enter");
         float dist = Vector3.Distance(transform.position, other.gameObject.transform.position);
         if (other.gameObject.layer == 0 && dist > 5)
         {
             canBePlaced = false;
         }
     }

     private void OnTriggerExit(Collider other)
     {
         Debug.Log(other.gameObject.name + "Exit");
         canBePlaced = true;
     }*/

    private void OnTriggerStay(Collider other) //used to make sure campfire is on placeable surface
    {
        if(!fireNearby)
        {
            if (other.gameObject.layer == 0)
            {

                float dist = Vector3.Distance(transform.position, other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position));
                //Debug.Log(dist + other.gameObject.name);
                if (dist < 5)
                {
                    canBePlaced = false;
                    gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
                else
                {
                    canBePlaced = true;
                    gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
            }
            else
            {
                canBePlaced = true;
                gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else
        {
            canBePlaced = false;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        /* else if (other.gameObject.layer == 13)
 {
     RaycastHit hit;
     if(Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit))
     {
         Debug.DrawRay(transform.position, new Vector3(0, -1,0) * hit.distance, Color.red);
         if(Vector3.Distance(transform.position, hit.transform.position) > 5 )
         {
             if (other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position).y < transform.position.y)
             {
                 canBePlaced = true;
                 gameObject.GetComponent<Renderer>().material.color = Color.green;
             }
             else
             {
                 canBePlaced = false;
                 gameObject.GetComponent<Renderer>().material.color = Color.red;
             }
         }
         else
         {
             canBePlaced = false;
             gameObject.GetComponent<Renderer>().material.color = Color.red;
         }
     }
 }*/
    }

}
