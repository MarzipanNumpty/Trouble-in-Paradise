using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AnimalMovementScript : MonoBehaviour
{
    Transform playerTransform;
    public bool chase; //has it saw player
    public float timeRunning; //how long it has been running
    public float timeToRun; //how long it will run
    NavMeshAgent agent; 
    public float baseSpeed;  //maximum speed of iguana
    public float actualSpeed; //the real speed of iguana
    public bool alert; //look out for player
    public bool cancelAlert; //stop looking for player
    public int health; 
    public GameObject meat;
    public RectTransform healthbar;
    public GameObject exclamationMark;
    public GameObject questionMark;
    public float exclamationTimer;
    Animator anim;
    public int meleeDamage;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        actualSpeed = baseSpeed;
        exclamationMark.SetActive(false);
        questionMark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.sizeDelta = new Vector2(health * 5, healthbar.sizeDelta.y); 
        agent.speed = actualSpeed;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = playerTransform.position - transform.position;
        if (Vector3.Distance(transform.position, playerTransform.position) < 25 && alert == false)  //if the iguana hasnt interacted with player and the player is within a certain distance of iguana
        {
            cancelAlert = false;
            if(Vector3.Dot(forward, toOther) > 0 && chase == false) // if player is in front of iguana run away
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + Random.Range(-20.0f, 20.0f), transform.rotation.z);
                alert = true;
                chase = true;
                timeRunning = timeToRun;
                actualSpeed = baseSpeed;
                anim.SetBool("Run", true);
            }
        }
        else if (alert && Vector3.Distance(transform.position, playerTransform.position) < 8) //if iguana has previously interacted with player and player is within a certain distance run away
        {
            cancelAlert = false;
            chase = true;
            timeRunning = timeToRun;
            actualSpeed = baseSpeed;
        }
        else if(alert && !cancelAlert && !chase) //if player hasnt interacted with iguana begin alert cooldown
        {
            cancelAlert = true;
            StartCoroutine("cancellingAlertMode", 0.0f);
        }
        if(timeRunning > 0) //if iguana is running show exclamation mark
        {
            questionMark.SetActive(false);
            exclamationMark.SetActive(true);
            timeRunning -= Time.deltaTime;
        }
        else if (timeRunning <= 0 && actualSpeed <= 1) //hide exclamation mark when speed slows down
        {
            exclamationMark.SetActive(false);
            chase = false;
        }
        if (timeRunning > timeToRun / 2) //decrease speed
        {
            actualSpeed -= Time.deltaTime;
        }
        else if(chase) //decrease speed
        {
            actualSpeed -= Time.deltaTime * 2;
        }
        if (chase) //moves iguana
        {
            agent.isStopped = false;
            //agent.SetDestination(transform.forward);
            agent.destination = transform.position + transform.forward;
            /*var localDirection = transform.rotation * transform.forward;
            localDirection = transform.InverseTransformDirection(transform.forward);
            agent.SetDestination(localDirection);*/
        }
        else //stops iguana from moving
        {
            agent.isStopped = true;
        }
        if(health <= 0) //destroys iguana and leaves a peice of meat for player to pick up
        {
            Instantiate(meat, transform.position, Quaternion.identity);
            Destroy(gameObject);
            //StartCoroutine("death", 0.0f);
        }
    }

    /*private void FixedUpdate()
    {
        if(chase)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.x + Random.Range(-45.0f, 45.0f), transform.rotation.z);
        }
    }*/

    IEnumerator cancellingAlertMode() //puts iguana into alert state
    {
        anim.SetBool("Run", false);
        questionMark.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        if(cancelAlert)
        {
            questionMark.SetActive(false);
            alert = false;
        }
    }

    private void OnTriggerEnter(Collider other) //take damage 
    {
        if (other.gameObject.tag == "Weapon")
        {
            health -= meleeDamage;
        }
        /*if (health <= 0)
        {
            Instantiate(meat, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }*/
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + Random.Range(-180.0f, 180.0f), transform.rotation.z);
        alert = true;
        chase = true;
        timeRunning = timeToRun;
        actualSpeed = baseSpeed;
    }

     IEnumerator death()
     {
         yield return new WaitForSeconds(5.0f);
         Destroy(gameObject);
     }

}
