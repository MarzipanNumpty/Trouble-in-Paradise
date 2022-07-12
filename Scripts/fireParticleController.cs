using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fireParticleController : MonoBehaviour
{
    public ParticleSystem fire;
    Transform playerTransform;
    GameObject player;
    PlayerScript playerController;
    public bool placed;
    public GameObject fireLight;
    public bool playing;
    public RectTransform healthbar;
    public float fireTimeLeft = 50;
    Hotbar hotbarScript;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        playerController = player.GetComponent<PlayerScript>();
        hotbarScript = GameObject.FindGameObjectWithTag("hotbar").GetComponent<Hotbar>();
        fireLight.SetActive(false);
        fire.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (placed && fireTimeLeft > 0) //if the campfire is placed and it is still burning
        {
            fireTimeLeft -= Time.deltaTime;
            healthbar.sizeDelta = new Vector2(fireTimeLeft * 4, healthbar.sizeDelta.y);
            if (Vector3.Distance(transform.position, playerTransform.position) < 50 && !playing) //if player is close enough play fire particle
            {
                playing = true;
                fire.Play();
                fireLight.SetActive(true);
                StartCoroutine("fireHealth", 0.0f);
            }
            else if (Vector3.Distance(transform.position, playerTransform.position) > 50) //if player is too far away stop fire particle 
            {
                playing = false;
                fire.Stop();
                fireLight.SetActive(false);
            }
            if (Vector3.Distance(transform.position, playerTransform.position) < 100) //used so fires cant be placed nearby
            {
                hotbarScript.fireNearby = true;
            }
            else if (Vector3.Distance(transform.position, playerTransform.position) > 100)
            {
                hotbarScript.fireNearby = false;
            }
        }
        else //dont play fire particle
        {
            playing = false;
            fire.Stop();
            fireLight.SetActive(false);
        }
    }

    IEnumerator fireHealth() //if player is nearby heal them slowly
    {
        if(playing)
        {
            if(playerController.CurrentHealth < playerController.MaxHealth)
            {
                playerController.CurrentHealth += playerController.fireHealing;
            }
            else if (playerController.CurrentHealth > playerController.MaxHealth)
            {
                playerController.CurrentHealth = playerController.MaxHealth;
            }
            yield return new WaitForSeconds(1.0f);
            StartCoroutine("fireHealth", 0.0f);
        }
    }
}
